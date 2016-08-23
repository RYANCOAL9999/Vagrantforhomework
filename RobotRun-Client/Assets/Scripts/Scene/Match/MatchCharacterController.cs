using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Cameras;
using Spine.Unity;
using System;
using SimpleJSON;
using DG.Tweening;

public class MatchCharacterController : Photon.MonoBehaviour {

	const string kMapSceneName = "BackgroundTest";

	public class SpeedMultipler{
		public enum Type{
			Ground,
			Skill
		}

		public enum ApplyType{
			Add,
			Multiple
		}

		public float value;
		public float duration;
		public Type type;
		public ApplyType applyType;

		public SpeedMultipler(SpeedMultipler.Type type, SpeedMultipler.ApplyType applyType, float multipler, float duration){
			this.type = type;
			this.applyType = applyType;
			this.value = multipler;
			this.duration = duration;
		}
	}

	public struct RaycastOrigins{
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

	public Action<SkillData> callbackDrawSkill = null;
	public Action<int> callbackGetMilkBottle = null;
#if UNITY_EDITOR
	public float normalRunSpeed = 5;
	public float normalPetRunSpeed = 10;
	public float jumpHeight = 1;
	public float jumpDoubleHeight = 1;
	public bool useEditorValue = false;
#endif
	public const float skinWidth = 0.05f;
	public int verticalRayCount = 4;
	protected float _verticalRaySpacing;
	protected RaycastOrigins _raycastOrigins;

	protected float _milkCollected;
	protected float _petLife = 0;
	protected float _currRunSpeed = 0;
	protected int _jumpState = 0;
	protected float _jumpForce = 0;
	protected float _jumpDoubleForce = 0;
	protected List<SpeedMultipler> _listSpeedMultiper = new List<SpeedMultipler>();
	protected Rigidbody2D _rigidBody2D;
	protected PolygonCollider2D _collier;
	protected bool _readyForJump2 = false;
	protected int _hurtID = 0;
	protected bool _isGround = false;
	protected float _ignoreHurtTime = 0;
	protected Vector2 _originCharacterPosition;
	protected Vector3 _prevPos;
	protected Vector3 _realVelocity;
	protected bool _arriveGoal = false;

	protected SK052 _sk052 = null;
	protected Sequence _skillSequence = null;

	protected PhotonTransformView _photonTransformView;
	protected Match _match;
	protected MatchCharacter _matchCharacter;
	public MatchCharacter matchCharacter{
		get{
			return _matchCharacter;
		}
	}

	public bool canHurt{
		get{
			return _hurtID == 0;
		}
	}

	public bool canRide{
		get{
			if(_matchCharacter.characterData.equipedPetData == null)return false;
			return _milkCollected >= _matchCharacter.characterData.equipedPetData.requiredMilk;
		}
	}


	protected virtual void Awake () {
		_rigidBody2D = this.GetComponent<Rigidbody2D>();
		_photonTransformView = this.GetComponent<PhotonTransformView>();
		_matchCharacter = this.GetComponent<MatchCharacter>();
		_match = GameObject.FindObjectOfType<Match>();
	}

	protected virtual void Start () {
		_readyForJump2 = false;
		_jumpState = 0;
		_hurtID = 0;
		_arriveGoal = false;
		_milkCollected = 0;

		if (photonView.isMine) {
//			_character.skeletonAnimationPet.zSpacing = 0;
//			_character.skeletonAnimationBody.zSpacing = 0;
//			_skeletonAnimationHead.zSpacing = 0;
		}else{
//			_character.skeletonAnimationPet.zSpacing = 5;
//			_character.skeletonAnimationBody.zSpacing = 5;
//			_skeletonAnimationHead.zSpacing = 5;
		}
		_currRunSpeed = 0;
		CalculateJumpForce();

		CalulateRaySpacing();

		_prevPos = this.transform.position;

		_match.AddMatchCharacterController(this);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (photonView.isMine) {
#if UNITY_EDITOR
			if(useEditorValue){
				 CalculateJumpForce();
			}
#endif
			UpdateRaycastOrigins();
			_isGround = IsGrounded ();
				
			UpdateSpeedMultipler(Time.deltaTime);
			if(_ignoreHurtTime > 0){
				_ignoreHurtTime = Mathf.Max(0, _ignoreHurtTime - Time.deltaTime);
			}

			if(_petLife > 0){
				_petLife -= Time.deltaTime;
				if(_matchCharacter.isRiding){
					if(_petLife < 0){
						_petLife = 0;
						this.photonView.RPC ("OnUnRide", PhotonTargets.AllBufferedViaServer, false);
					}
					if(_petLife < _matchCharacter.characterData.equipedPetData.duration - _matchCharacter.characterData.equipedPetData.attackDuration){
						_matchCharacter.DisablePetTrigger();
					}
				}
			}

			if(this.GetType() == typeof(MatchCharacterController)){
				if(_hurtID == 0){
					HandleInput();
				}
			}

			if(_hurtID != 0){
				_rigidBody2D.velocity = Vector2.zero;
			}else{
				_rigidBody2D.velocity = new Vector2(_currRunSpeed, _rigidBody2D.velocity.y);
			}
		}
	}

	protected virtual void LateUpdate(){
	}

	protected virtual void FixedUpdate ()
	{
		if (photonView.isMine) {
			_realVelocity = (this.transform.position - _prevPos)/Time.fixedDeltaTime;
			_photonTransformView.SetSynchronizedValues(_realVelocity, 0);
			UpdateCharacterAnimation();
			/*if(_jumpState == 1 && _isGround){
				_jumpState = 0;
			}*/
			_prevPos = this.transform.position;
		}
//		Debug.Log("FixedUpdate: "+_rigidBody2D.velocity);
	}

	//Photon Callbacks
	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext ((int)_matchCharacter.currentCharacterState);
			stream.SendNext ((int)_matchCharacter.currentPetState);
		} else {
			_matchCharacter.SetCharacterState ((Character.CharacterState)stream.ReceiveNext ());
			_matchCharacter.SetPetState ((Character.PetState)stream.ReceiveNext ());
		}
	}

	protected virtual void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate");

		GameDataManager.Instance.dictOwnerIDMatchCharacterController.Add(this.photonView.ownerId, this);

		_matchCharacter.characterData = new CharacterData(JSON.Parse(this.photonView.instantiationData[0].ToString()));
		#if UNITY_EDITOR
		if(!GameDataManager.Instance.dictPlayerIDSlot.ContainsKey(info.sender.ID)){
			_match.matchProgressBar.listMatchProgressBarIcon[0].matchCharacterController = this;

		}else{
		#endif
			int playerIndex = GameDataManager.Instance.dictPlayerIDSlot[info.sender.ID];
			_match.matchProgressBar.listMatchProgressBarIcon[playerIndex].matchCharacterController = this;
		#if UNITY_EDITOR
		}
		#endif
		// e.g. store this gameobject as this player's charater in PhotonPlayer.TagObject
//		info.sender.TagObject = this.GameObject;
	}

//	[PunRPC]
//	void SetPosition(string x, string y, PhotonMessageInfo msgInfo){
//		this.transform.position = new Vector3(float.Parse(x), float.Parse(y));
//	}

	[PunRPC]
	protected void OnThrow (PhotonMessageInfo msgInfo)
	{
		if(_matchCharacter.holdingSkillData != null){
			if (photonView.isMine) { 
				if(_matchCharacter.holdingSkillData.id == 32){
					_matchCharacter.UseSkill();
					return;
				}else if(_matchCharacter.holdingSkillData.id == 42){
					_matchCharacter.ShowGun();
					_skillSequence = DOTween.Sequence();
					_skillSequence.AppendCallback(new TweenCallback(FireBullet));
					_skillSequence.AppendInterval(0.3f);
					_skillSequence.SetLoops(10);
					_skillSequence.OnComplete(OnFireBulletFinish);
					_skillSequence.Play();
				}else if(_matchCharacter.holdingSkillData.id == 51){
					AddSpeedMultipler(SpeedMultipler.Type.Skill, SpeedMultipler.ApplyType.Multiple, 2, 3);
					_matchCharacter.UseSkill();
				}else if(_matchCharacter.holdingSkillData.id == 52){
					if(_sk052 == null){
						this.photonView.RPC ("AddSkillEffect", PhotonTargets.AllBufferedViaServer, _matchCharacter.holdingSkillData.prefabPath);
					}
					_matchCharacter.UseSkill();
				}else{
					object[] data = new object[3];
					data[0] = this.photonView.ownerId;
					data[1] = JsonUtility.ToJson(this.transform.localScale);
					data[2] = JsonUtility.ToJson(_realVelocity);

					Spine.Bone boneItem = _matchCharacter.skeletonAnimationBody.Skeleton.FindBone("item");
					PhotonNetwork.Instantiate(_matchCharacter.holdingSkillData.prefabPath, boneItem.GetWorldPosition(_matchCharacter.skeletonAnimationBody.transform), Quaternion.identity, 0, data);

					_matchCharacter.ThrowWithAnimation();
				}
			}
		}
	}

	void CalculateJumpForce(){
		float g = _rigidBody2D.gravityScale * Physics2D.gravity.magnitude;
#if UNITY_EDITOR
		if(useEditorValue){
			_jumpForce = (Mathf.Sqrt(2 * g * jumpHeight) + 0.1f) * _rigidBody2D.mass;
			_jumpDoubleForce = (Mathf.Sqrt(2 * g * jumpDoubleHeight) + 0.1f) * _rigidBody2D.mass;
		}else{
#endif
			_jumpForce = (Mathf.Sqrt(2 * g * _matchCharacter.characterData.matchJump) + 0.1f) * _rigidBody2D.mass;
			_jumpDoubleForce = (Mathf.Sqrt(2 * g * _matchCharacter.characterData.matchDoubleJump) + 0.1f) * _rigidBody2D.mass;
#if UNITY_EDITOR
		}
#endif
	}

	void FireBullet(){
		if(_matchCharacter.holdingSkillData != null){
			object[] data = new object[3];
			data[0] = this.photonView.ownerId;
			data[1] = JsonUtility.ToJson(this.transform.localScale);
			data[2] = JsonUtility.ToJson(_realVelocity);

			Spine.Bone boneItem = _matchCharacter.skeletonAnimationBody.Skeleton.FindBone("Gun");
			PhotonNetwork.Instantiate(_matchCharacter.holdingSkillData.prefabPath, boneItem.GetWorldPosition(_matchCharacter.skeletonAnimationBody.transform), Quaternion.identity, 0, data);
		}
	}

	void OnFireBulletFinish(){
		_matchCharacter.HideGun();
		_matchCharacter.UseSkill();
		_skillSequence = null;
	}

//	[PunRPC]
//	void OnHold (PhotonMessageInfo msgInfo)
//	{
//		if(_character.holdingItem == null){
//			Item item = _character.Hold("Prefabs/Match/Item");
//			if(item != null){
//				item.fromCharacter = this;
//			}
//		}
//	}

	[PunRPC]
	protected void OnHurt (int skillID, PhotonMessageInfo msgInfo)
	{
		HandleHurt(skillID, this.transform.position);
	}

	protected void HandleHurt(int skillID, Vector3 rebornPos){
		if(skillID == 23){
			AddSpeedMultipler(SpeedMultipler.Type.Skill, SpeedMultipler.ApplyType.Multiple, 0.2f, 5);
			return;
		}
		if(_sk052 != null){
			Destroy(_sk052.gameObject);
			_sk052 = null;
			_ignoreHurtTime = 1.0f;
			_rigidBody2D.AddForce (new Vector2 (0, _jumpForce - _rigidBody2D.velocity.y * _rigidBody2D.mass), ForceMode2D.Impulse);
			return;
		}
		if(_matchCharacter.isRiding && _ignoreHurtTime == 0){
			_ignoreHurtTime = 1.0f;
			this.photonView.RPC ("OnUnRide", PhotonTargets.AllBufferedViaServer, true);
		}else if(_ignoreHurtTime == 0){
			if(_ignoreHurtTime == 0){
				switch(skillID){
				case 1:
				case 2:
				case 3:
				case 4:
				case 7:
				case 42:
					_hurtID = 2;
					break;
				case 31:
					_hurtID = 3;
					break;
				default:
					_hurtID = 1;
					break;
				}
			}
			if(_skillSequence != null){
				_skillSequence.Complete();
			}
			StartCoroutine(SetRebornPositionAt(rebornPos));
		}
		ClearSpeedMultipler();
	}

	[PunRPC]
	public void OnRide(PhotonMessageInfo msgInfo){
		_milkCollected = 1000;
		if(_milkCollected >= _matchCharacter.characterData.equipedPetData.requiredMilk){
			_milkCollected = 0;
			_matchCharacter.Ride();
			_matchCharacter.EnablePetTrigger();
	#if UNITY_EDITOR
			if(useEditorValue){
				_currRunSpeed = normalPetRunSpeed;
			}else{
				_currRunSpeed = _matchCharacter.characterData.matchPetSpeed;
			}
	#else
			_currRunSpeed = _character.characterData.matchPetSpeed;
	#endif
			ApplySpeedModifier();
			CalulateRaySpacing();
			_petLife = _matchCharacter.characterData.equipedPetData.duration;
		}
	}

	[PunRPC]
	public void OnUnRide(bool jump, PhotonMessageInfo msgInfo){
		_matchCharacter.UnRide();
#if UNITY_EDITOR
		if(useEditorValue){
			_currRunSpeed = normalRunSpeed;
		}else{
			_currRunSpeed = _matchCharacter.characterData.matchSpeed;
		}
#else
		_currRunSpeed = _character.characterData.matchSpeed;
#endif
		ApplySpeedModifier();
		CalulateRaySpacing();
		if(jump){
			_rigidBody2D.AddForce (new Vector2 (0, _jumpForce - _rigidBody2D.velocity.y * _rigidBody2D.mass), ForceMode2D.Impulse);
		}
	}

	[PunRPC]
	public void AddSkillEffect(string prefabPath, PhotonMessageInfo msgInfo){
		GameObject go = Instantiate(Resources.Load<GameObject>(prefabPath));
		go.transform.SetParent(this.transform, false);
		_sk052 = go.GetComponent<SK052>();
	}

	[PunRPC]
	public void OnPlayerArriveGoal(PhotonMessageInfo msgInfo){
		_match.OnPhotonPlayerArriveGoal(msgInfo.sender);
	}

	//Collider Callbacks
	public virtual void OnTriggerEnter2D(Collider2D collider){
//		Debug.Log("OnTriggerEnter: "+collider.name);
		if(this.photonView.isMine){
			if(collider.name == "end" && !_arriveGoal){
				_arriveGoal = true;
				this.photonView.RPC ("OnPlayerArriveGoal", PhotonTargets.AllBufferedViaServer);
				return;
			}
			if(collider.name == "TresaureBox"){
				DestroyMapItem(collider.gameObject);
	//			this.photonView.RPC ("OnHold", PhotonTargets.AllBufferedViaServer);
				DrawThrowItem();
				return;
			}
			if(collider.name == "milkbottle"){
				DestroyMapItem(collider.gameObject);
				if(callbackGetMilkBottle != null){
					_milkCollected++;
					callbackGetMilkBottle(1);
				}
				return;
			}
			if(collider.name == "MilkBottleLarge"){
				DestroyMapItem(collider.gameObject);
				if(callbackGetMilkBottle != null){
					_milkCollected += 5;
					callbackGetMilkBottle(5);
				}
				return;
			}
			if(collider.name == "ColliderTrigger"){
				if(this.transform.position.x > collider.gameObject.transform.position.x){
					HandleHurt(0, this.transform.position);
				}
				return;
			}
			DropArea dropArea = collider.gameObject.GetComponent<DropArea>();
			if(dropArea != null){
				Vector2 rebornPos = dropArea.transform.TransformPoint(dropArea.rebornPosition);
				StartCoroutine(SetRebornPositionAt(rebornPos));
				_match.OnMatchCharacterControllerTriggerDropArea(this);
				return;
			}
			AcceleratePlatform acceleratePlatform = collider.gameObject.GetComponent<AcceleratePlatform>();
			if(acceleratePlatform != null){
				AddSpeedMultipler(SpeedMultipler.Type.Ground, acceleratePlatform.applyType, acceleratePlatform.speedMultipler, acceleratePlatform.multiplerDurationSec);
			}
		}

	}

	public virtual void OnTriggerStay2D(Collider2D collider){
		if(collider.gameObject.transform.parent != null){
			HurtableObject hurtableObject = collider.gameObject.transform.parent.GetComponent<HurtableObject>();
			if(hurtableObject != null && _hurtID == 0){
				//				this.transform.position = hurtableObject.transformReborn.position;
				Vector2 rebornPos = hurtableObject.transform.TransformPoint(hurtableObject.rebornPosition);
				int collisionMask = MatchConfig.layerMaskGround | MatchConfig.layerMaskObstacle;
				RaycastHit2D hit = Physics2D.Raycast(rebornPos, Vector2.down, float.MaxValue, collisionMask);
				if(hit){
					rebornPos = hit.point;
				}
				HandleHurt(0, rebornPos);
			}
		}
	}

	public virtual void OnTriggerExit2D(Collider2D collider){
		Debug.Log("OnTriggerExit: "+collider.name);
	}

	public void OnHitByItem(ThrowItem item){
		if(_hurtID == 0){
			this.photonView.RPC ("OnHurt", PhotonTargets.AllBufferedViaServer, item.skillDataID);
		}
	}

	protected virtual void DestroyMapItem(GameObject gameObject){
		Destroy(gameObject);
	}

	//Public Functions
	public void StartMatch(){
#if UNITY_EDITOR
		if(useEditorValue){
			_currRunSpeed = normalRunSpeed;
		}else{
			_currRunSpeed = _matchCharacter.characterData.matchSpeed;
		}
#else
		_currRunSpeed = _character.characterData.matchSpeed;
#endif
		_photonTransformView.SetSynchronizedValues(_realVelocity, 0);
	}

	public virtual void Jump(){
		if (_isGround) {
//			if (_character.isRiding) {
//				_character.SetPetState (Character.PetState.Jump);
//			}else{
//				_character.SetCharacterState (Character.CharacterState.Jump);
//			}
			_rigidBody2D.AddForce (new Vector2 (0, _jumpForce - _rigidBody2D.velocity.y * _rigidBody2D.mass), ForceMode2D.Impulse);
			_readyForJump2 = true;
			_jumpState = 1;
			//					return;
		} else if (_jumpState != 2) {
			_jumpState = 2;
			_readyForJump2 = false;
//			if (_character.isRiding) {
////				_character.SetCharacterState (Character.CharacterState.Ride);
//				_character.SetPetState (Character.PetState.Jump);
//			} else {
//				_character.SetCharacterState (Character.CharacterState.Jump2);
//			}
			_rigidBody2D.AddForce (new Vector2 (0, _jumpDoubleForce - _rigidBody2D.velocity.y * _rigidBody2D.mass), ForceMode2D.Impulse);
			//					return;
		}
	}

	public void Throw(){
		if(_matchCharacter.holdingSkillData != null){
			this.photonView.RPC ("OnThrow", PhotonTargets.AllBufferedViaServer);
		}
	}

	public void Ride(){
		if(!_matchCharacter.isRiding){
			this.photonView.RPC ("OnRide", PhotonTargets.AllBufferedViaServer);
		}
//		else{
//			this.photonView.RPC ("OnUnRide", PhotonTargets.AllBufferedViaServer, false);
//		}
	}

	public void DrawThrowItem(){
		int rank = _match.GetCurrentRank(this);
		SkillData drawSkillData = _matchCharacter.DrawSkill(rank);
		if(drawSkillData != null && callbackDrawSkill != null){
			callbackDrawSkill(drawSkillData);
		}
	}

	//Private Functions
	bool IsGrounded ()
	{
		float rayLength = skinWidth;
		int collisionMask = MatchConfig.layerMaskGround | MatchConfig.layerMaskObstacle;
		for(int i = 0; i < verticalRayCount; i++){
			Vector2 rayOrigin = _raycastOrigins.bottomLeft;
			rayOrigin += Vector2.right * (_verticalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.down * rayLength, Color.red);
			if(hit){
				return true;
			}
		}
		return false;
	}

	bool HasSkillSpeedModifer(){
		foreach(SpeedMultipler sm in _listSpeedMultiper){
			if(sm.type == SpeedMultipler.Type.Skill){
				return true;
			}
		}
		return false;
	}

	void UpdateCharacterAnimation(){
		if (_matchCharacter.isRiding) {
			_matchCharacter.SetCharacterState (Character.CharacterState.Ride);
			if (_isGround) {
				if (_realVelocity.x > 0.001f) {

//					if(_petLife >= _character.characterData.equipedPetData.attackDuration){
//						_character.SetPetState (Character.PetState.Attack);
//					}else{
						_matchCharacter.SetPetState (Character.PetState.Run);
//					}
				} else {
					_matchCharacter.SetPetState (Character.PetState.Idle);
				}

			} else {
//				if (_realVelocity.y > 0.001f) {
//					if (_character.currentCharacterState != Character.CharacterState.Jump2) {
//
//						_character.SetPetState (Character.PetState.Jump);
//					}
//				} else 

				if (_realVelocity.y < 0.001f) {
					_matchCharacter.SetPetState (Character.PetState.Fall);
				}else if(_jumpState == 1){
					_matchCharacter.SetPetState (Character.PetState.Jump);
				}else if(_jumpState == 2){
					_matchCharacter.SetPetState (Character.PetState.Jump);
				} 
			}
		}else if(_hurtID == 1){
			_matchCharacter.SetCharacterState (Character.CharacterState.Hurt1);
		}else if(_hurtID == 2){
			_matchCharacter.SetCharacterState (Character.CharacterState.Hurt2);
		}else if(_hurtID == 3){
			_matchCharacter.SetCharacterState (Character.CharacterState.Hurt3);
		}else if (_isGround) {
  			if (_realVelocity.x > _matchCharacter.characterData.matchSpeed+0.001f) {
				if(HasSkillSpeedModifer()){
					_matchCharacter.SetCharacterState (Character.CharacterState.Run3);
				}else{
					_matchCharacter.SetCharacterState (Character.CharacterState.Run2);
				}
			} else if (_realVelocity.x > 0.001f) {

				_matchCharacter.SetCharacterState (Character.CharacterState.Run1);
			} else {

				_matchCharacter.SetCharacterState (Character.CharacterState.Idle);
			}

		} else {
			if (_realVelocity.y < 0.001f) {
				_matchCharacter.SetCharacterState (Character.CharacterState.Fall);
			}else if(_jumpState == 1){
				_matchCharacter.SetCharacterState (Character.CharacterState.Jump);
			}else if(_jumpState == 2){
   				_matchCharacter.SetCharacterState (Character.CharacterState.Jump2);
			} 
		}
	}

	protected virtual void CalulateRaySpacing(){
		if(_matchCharacter.isRiding){
			_collier = _matchCharacter.skeletonAnimationPet.transform.FindChild("bodyarea").GetComponent<PolygonCollider2D>();
		}else{
			_collier = _matchCharacter.skeletonAnimationBody.transform.FindChild("bodyarea").GetComponent<PolygonCollider2D>();
		}
		verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
		float minX = float.MaxValue;
		float maxX = float.MinValue;
		foreach(Vector2 v in _collier.points){
			if(v.x < minX){
				minX = v.x;
			}
			if(v.x > maxX){
				maxX = v.x;
			}
		}
		_verticalRaySpacing = ((maxX - minX) * this.transform.lossyScale.x) / (verticalRayCount - 1);
	}

	protected void UpdateRaycastOrigins(){
		PolygonCollider2D collier;
		if(_matchCharacter.isRiding){
			collier = _matchCharacter.skeletonAnimationPet.transform.FindChild("bodyarea").GetComponent<PolygonCollider2D>();
		}else{
			collier = _matchCharacter.skeletonAnimationBody.transform.FindChild("bodyarea").GetComponent<PolygonCollider2D>();
		}
		_raycastOrigins.bottomLeft = new Vector2(float.MaxValue, float.MaxValue);
		_raycastOrigins.bottomRight = new Vector2(float.MinValue, float.MaxValue);
		_raycastOrigins.topLeft = new Vector2(float.MaxValue, float.MinValue);
		_raycastOrigins.topRight = new Vector2(float.MinValue, float.MinValue);
		foreach(Vector2 v in collier.points){
			if(v.x + v.y < _raycastOrigins.bottomLeft.x + _raycastOrigins.bottomLeft.y){//min(x+y)
//			if(v.x <= _raycastOrigins.bottomLeft.x && v.y <= _raycastOrigins.bottomLeft.y){
				_raycastOrigins.bottomLeft = v;
			}
//			if(v.x >= _raycastOrigins.bottomRight.x && v.y <= _raycastOrigins.bottomRight.y){//max(max(x)-x+y)
//				_raycastOrigins.bottomRight = v;
//			}
//			if(v.x <= _raycastOrigins.topLeft.x && v.y >= _raycastOrigins.topLeft.y){//min(x+max(y)-y)
//				_raycastOrigins.topLeft = v;
//			}
			if(v.x + v.y > _raycastOrigins.topRight.x + _raycastOrigins.topRight.y){//max(x+y)
//			if(v.x >= _raycastOrigins.topRight.x && v.y >= _raycastOrigins.topRight.y){
				_raycastOrigins.topRight = v;
			}
		}
		_raycastOrigins.bottomRight = new Vector2(_raycastOrigins.topRight.x, _raycastOrigins.bottomLeft.y);
		_raycastOrigins.topLeft = new Vector2(_raycastOrigins.bottomLeft.x, _raycastOrigins.topRight.y);
		if(_matchCharacter.isRiding){
			_raycastOrigins.bottomLeft = _matchCharacter.skeletonAnimationPet.transform.TransformPoint(_raycastOrigins.bottomLeft);
			_raycastOrigins.bottomRight = _matchCharacter.skeletonAnimationPet.transform.TransformPoint(_raycastOrigins.bottomRight);
			_raycastOrigins.topLeft = _matchCharacter.skeletonAnimationPet.transform.TransformPoint(_raycastOrigins.topLeft);
			_raycastOrigins.topRight = _matchCharacter.skeletonAnimationPet.transform.TransformPoint(_raycastOrigins.topRight);
		}else{
			_raycastOrigins.bottomLeft = _matchCharacter.skeletonAnimationBody.transform.TransformPoint(_raycastOrigins.bottomLeft);
			_raycastOrigins.bottomRight = _matchCharacter.skeletonAnimationBody.transform.TransformPoint(_raycastOrigins.bottomRight);
			_raycastOrigins.topLeft = _matchCharacter.skeletonAnimationBody.transform.TransformPoint(_raycastOrigins.topLeft);
			_raycastOrigins.topRight = _matchCharacter.skeletonAnimationBody.transform.TransformPoint(_raycastOrigins.topRight);
		}
	}

	protected virtual void HandleInput(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump();
		}else if (Input.GetKeyDown (KeyCode.W) ) {
			this.photonView.RPC ("OnThrow", PhotonTargets.AllBufferedViaServer);
		} else if (Input.GetKeyDown (KeyCode.S)) {
			DrawThrowItem();
		} else if (Input.GetKeyDown (KeyCode.R)) {
			this.photonView.RPC ("OnRide", PhotonTargets.AllBufferedViaServer);
		} else if (Input.GetKeyDown (KeyCode.F)) {
			this.photonView.RPC ("OnUnRide", PhotonTargets.AllBufferedViaServer, false);
		}
	}

	protected void AddSpeedMultipler(SpeedMultipler.Type type, SpeedMultipler.ApplyType applyType, float value, float duration){
		ClearSpeedMultipler();

		SpeedMultipler speedMultipler = new SpeedMultipler(type, applyType , value, duration);
		_listSpeedMultiper.Add(speedMultipler);
		switch(applyType){
		case SpeedMultipler.ApplyType.Add:
			_currRunSpeed += speedMultipler.value;
			break;
		case SpeedMultipler.ApplyType.Multiple:
			_currRunSpeed *= speedMultipler.value;
			break;
		}
		if(type == SpeedMultipler.Type.Skill){
			_matchCharacter.ShowSpeedSkillAnimation();
		}
	}

	protected void ApplySpeedModifier(){
		foreach(SpeedMultipler sp in _listSpeedMultiper){
			if(sp.duration > 0){
				switch(sp.applyType){
				case SpeedMultipler.ApplyType.Add:
					_currRunSpeed += sp.value;
					break;
				case SpeedMultipler.ApplyType.Multiple:
					_currRunSpeed *= sp.value;
					break;
				}
			}
		}
	}

	protected void ClearSpeedMultipler(){
		_listSpeedMultiper.Clear();
#if UNITY_EDITOR
		if(useEditorValue){
			_currRunSpeed = normalRunSpeed;
		}else{
			_currRunSpeed = _matchCharacter.characterData.matchSpeed;
		}
#else
		_currRunSpeed = _character.characterData.matchSpeed;
#endif
		_matchCharacter.HideSpeedSkillAnimation();
	}

	protected void UpdateSpeedMultipler(float dt){
		List<SpeedMultipler> listRemove = new List<SpeedMultipler>();
		foreach(SpeedMultipler sp in _listSpeedMultiper){
			sp.duration -= dt;
			if(sp.duration <= 0){
				listRemove.Add(sp);
				switch(sp.applyType){
				case SpeedMultipler.ApplyType.Add:
					_currRunSpeed -= sp.value;
					break;
				case SpeedMultipler.ApplyType.Multiple:
					_currRunSpeed /= sp.value;
					break;
				}
				if(sp.type == SpeedMultipler.Type.Skill){
					_matchCharacter.HideSpeedSkillAnimation();
				}
			}
		}
		foreach(SpeedMultipler spRemove in listRemove){
			_listSpeedMultiper.Remove(spRemove);
		}
	}

	protected virtual IEnumerator SetRebornPositionAt(Vector3 position){
		yield return new WaitForSeconds(_matchCharacter.characterData.matchRecovery);
		_hurtID = 0;
		this.transform.position = position;
		_match.OnMatchCharacterControllerReborn(this);
	}
}
