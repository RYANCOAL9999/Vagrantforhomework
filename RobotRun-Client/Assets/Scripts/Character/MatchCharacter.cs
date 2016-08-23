using UnityEngine;
using System.Collections;
using Spine.Unity;

public class MatchCharacter : Character {
	SkeletonAnimation _skeletonAnimationBody;
	SkeletonAnimation _skeletonAnimationHead;
	SkeletonAnimation _skeletonAnimationPet;
	GameObject _aniDust;
	BoneFollower _boneFollowerBody;
	SpineAddColliderTrigger _petCollierTrigger;

	bool _petInited;

	#if UNITY_EDITOR
	public int editorSkillID = 1;
	#endif

	public override bool isRiding{
		get{
			return _skeletonAnimationPet.gameObject.activeSelf;
		}
	}

	public SkeletonAnimation skeletonAnimationBody{
		get{
			return _skeletonAnimationBody;
		}
	}

	public SkeletonAnimation skeletonAnimationHead{
		get{
			return _skeletonAnimationHead;
		}
	}

	public SkeletonAnimation skeletonAnimationPet{
		get{
			return _skeletonAnimationPet;
		}
	}

	protected override void Awake () {
		base.Awake();
		_skeletonAnimationBody = this.transform.FindChild("BodySpine").GetComponent<SkeletonAnimation>();
		_skeletonAnimationHead = this.transform.FindChild("HeadSpine").GetComponent<SkeletonAnimation>();
		_skeletonAnimationPet = this.transform.FindChild("PetSpine").GetComponent<SkeletonAnimation>();
		_aniDust = this.transform.FindChild("dust").gameObject;
		_boneFollowerBody = _skeletonAnimationBody.GetComponent<BoneFollower>();
	}

	protected override void Start () {
		base.Start();
		_skeletonAnimationPet.gameObject.SetActive(false);
		_aniDust.SetActive(false);
	}

	protected override void Update () {
		base.Update();
	}

	protected override void UpdateCharacter(){

		SkeletonDataAsset assetBody = _characterData.bodySkeletonDataAsset;
		_skeletonAnimationBody.skeletonDataAsset = assetBody;
		if(_characterData.bodySpineSkinName != ""){
			_skeletonAnimationBody.initialSkinName = _characterData.bodySpineSkinName;
		}
		_skeletonAnimationBody.Initialize(true);
		_skeletonAnimationBody.state.SetAnimation (0, animationCharacterIdle, true);
		_skeletonAnimationBody.GetComponent<Renderer>().sortingLayerName = "Player";


		SkeletonDataAsset assetHead = _characterData.headSkeletonDataAsset;
		_skeletonAnimationHead.skeletonDataAsset = assetHead;
		if(_characterData.headSpineSkinName != ""){
			_skeletonAnimationHead.initialSkinName = _characterData.headSpineSkinName;
		}
		_skeletonAnimationHead.GetComponent<BoneFollower>().boneName = "head";
		_skeletonAnimationHead.OnRebuild += ApplyBlink;
		_skeletonAnimationHead.Initialize(true);

		if(_characterData.equipedPetData != null){
			SkeletonDataAsset assetPet = _characterData.petSkeletonDataAsset;
			_skeletonAnimationPet.skeletonDataAsset = assetPet;
			if(_characterData.petSpineSkinName != ""){
				_skeletonAnimationPet.initialSkinName = _characterData.petSpineSkinName;
			}
			_petInited = false;
			_skeletonAnimationPet.Initialize(true);
			_skeletonAnimationPet.state.Event += OnPetSkeletonEvent;
			_skeletonAnimationPet.state.SetAnimation (0, animationPetInit, false);
			_skeletonAnimationPet.gameObject.AddComponent<SpineAddCollider>();
			_petCollierTrigger = _skeletonAnimationPet.gameObject.AddComponent<SpineAddColliderTrigger>();
			_petCollierTrigger.DisableTrigger();
		}

		_boneFollowerBody = _skeletonAnimationBody.gameObject.GetComponent<BoneFollower>();

		_skeletonAnimationBody.gameObject.AddComponent<SpineAddCollider>();

	}

	protected void ApplyBlink(SkeletonRenderer skeletonRenderer) {
		StartBlink();
	}

	protected override IEnumerator Blink() {
		while (true) {
			_skeletonAnimationHead.skeleton.SetAttachment(faceSlot, faceAttachment1);
			yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
			_skeletonAnimationHead.skeleton.SetAttachment(faceSlot, faceAttachment2);
			yield return new WaitForSeconds(0.02f);
			_skeletonAnimationHead.skeleton.SetAttachment(faceSlot, faceAttachment3);
			yield return new WaitForSeconds(0.02f);
			_skeletonAnimationHead.skeleton.SetAttachment(faceSlot, faceAttachment2);
			yield return new WaitForSeconds(0.02f);
		}
	}

	public override void SetCharacterState (CharacterState state)
	{
		base.SetCharacterState(state);
		if (_currentCharacterState != state && _skeletonAnimationBody.skeletonDataAsset != null) {
			_currentCharacterState = state;

			switch (_currentCharacterState) {
			case CharacterState.Idle:
				_skeletonAnimationBody.state.SetAnimation (0, animationCharacterIdle, true);
				break;
			case CharacterState.Run1:
				_skeletonAnimationBody.state.SetAnimation (0, animationCharacterRun1, true);
				break;
			case CharacterState.Run2:
				_skeletonAnimationBody.state.SetAnimation (0, animationCharacterRun2, true);
				break;
			case CharacterState.Run3:
				_skeletonAnimationBody.state.SetAnimation (0, animationCharacterRun3, true);
				break;
			case CharacterState.Jump:
				_skeletonAnimationBody.state.SetAnimation (0, animationCharacterJump, true);
				break;
			case CharacterState.Jump2:
				_skeletonAnimationBody.state.SetAnimation (0, animationCharacterDoubleJump, false);
				break;
			case CharacterState.Fall:
				_skeletonAnimationBody.state.SetAnimation (0, animationCharacterFall, true);
				break;
			case CharacterState.Hurt1:
				_skeletonAnimationBody.state.ClearTrack (1);
				_skeletonAnimationBody.state.SetAnimation (0, animationCharacterHurt1, false);
				break;
			case CharacterState.Hurt2:
				_skeletonAnimationBody.state.ClearTrack (1);
				_skeletonAnimationBody.state.SetAnimation (0, animationCharacterHurt2, false);
				break;
			case CharacterState.Hurt3:
				_skeletonAnimationBody.state.ClearTrack (1);
				_skeletonAnimationBody.state.SetAnimation (0, animationCharacterHurt3, false);
				break;
			case CharacterState.Ride:
				_skeletonAnimationBody.state.SetAnimation (0, animationCharacterRide, true);
				break;
			}
			if(_currentCharacterState == CharacterState.Hurt1 
				|| _currentCharacterState == CharacterState.Hurt2 
				|| _currentCharacterState == CharacterState.Hurt3){
				StopBlink();
				_skeletonAnimationHead.skeleton.SetAttachment(faceSlot, faceAttachment4);
			}else{
				StartBlink();
			}
		}
	}

	public override void SetPetState (PetState state)
	{
		base.SetPetState(state);
		if(!_petInited)return;
		if (_currentPetState != state && _skeletonAnimationPet.skeletonDataAsset != null) {
			_currentPetState = state;

			switch (_currentPetState) {
			case PetState.Idle:
				_skeletonAnimationPet.state.SetAnimation (0, animationPetIdle, true);
				break;
			case PetState.Run:
				_skeletonAnimationPet.state.SetAnimation (0, animationPetRun, true);
				break;
			case PetState.Jump:
				_skeletonAnimationPet.state.SetAnimation (0, animationPetJump, true);
				break;
			case PetState.Fall:
				_skeletonAnimationPet.state.SetAnimation (0, animationPetFall, true);
				break;
			case PetState.Attack:
				_skeletonAnimationPet.state.SetAnimation (0, animationPetAttack, true);
				break;
			}
		}
	}

	public override void Ride(){
		base.Ride();
		_boneFollowerBody.enabled = true;
		_boneFollowerBody.boneName = boneSeatPos;
		_skeletonAnimationPet.gameObject.SetActive(true);
		this.transform.position = new Vector3(this.transform.position.x, _skeletonAnimationBody.transform.position.y);
		StartCoroutine(ShowDust());
	}

	public override void UnRide(){
		base.UnRide();
		_boneFollowerBody.enabled = false;
		_skeletonAnimationPet.gameObject.SetActive(false);
		_skeletonAnimationBody.transform.localPosition = Vector2.zero;
		_skeletonAnimationBody.transform.localRotation = Quaternion.identity;
		StartCoroutine(ShowDust());
	}

	public void EnablePetTrigger(){
		if(!_petCollierTrigger.triggerIsActive){
			_skeletonAnimationPet.state.SetAnimation (1, animationPetAttack, true);
//			_skeletonAnimationPet.state.SetAnimation (1, "speed", true);
			_petCollierTrigger.EnableTrigger();
		}
	}

	public void DisablePetTrigger(){
		_petCollierTrigger.DisableTrigger();

		Spine.TrackEntry track = _skeletonAnimationPet.state.GetCurrent(1);
		if(track == null)return;
		Spine.Animation animation = track.Animation;
		if(animation == null)return;
		if(animation.name == animationPetAttackEnd){
			_skeletonAnimationPet.state.SetAnimation (1, animationPetAttackEnd, false);
		}
	}

	IEnumerator ShowDust(){
		_aniDust.SetActive(true);
		yield return new WaitForSeconds(1);
		_aniDust.SetActive(false);
	}

	public SkillData DrawSkill(int rank){
//		Throw
		if(_holdingSkillData == null){
			SkillData randSkillData = _characterData.equipedClothData.GetSkillData(GameDataManager.Instance.GetRandSkillIndex(rank));
			_holdingSkillData = randSkillData;
//			Debug.Log("Draw Skill: "+randSkillData.id);
#if UNITY_EDITOR
			_holdingSkillData = GameDataManager.Instance.GetSkillData(editorSkillID);
#endif
			return _holdingSkillData;
		}
		return null;
	}

	public void ThrowWithAnimation(){
		if(_holdingSkillData != null){
			_skeletonAnimationBody.state.SetAnimation (1, Character.animationCharacterThrow, false);
		}
		base.UseSkill();
	}

	public override void ShowGun(){
		base.ShowGun();
		_skeletonAnimationBody.skeleton.SetAttachment(gunSlot, gunAttachment);
		_skeletonAnimationBody.state.SetAnimation (1, Character.animationCharacterGun, true);
	}

	public override void HideGun(){
		base.HideGun();
		_skeletonAnimationBody.state.ClearTrack(1);
		_skeletonAnimationBody.skeleton.SetAttachment(gunSlot, null);
	}

	public void ShowSpeedSkillAnimation(){
		_skeletonAnimationBody.state.SetAnimation (1, animationCharacterSpeed, true);
	}

	public void HideSpeedSkillAnimation(){
		Spine.TrackEntry track = _skeletonAnimationBody.state.GetCurrent(1);
		if(track == null)return;
		Spine.Animation animation = track.Animation;
		if(animation == null)return;
		if(animation.name == animationCharacterSpeed){
 			_skeletonAnimationBody.state.SetAnimation (1, animationCharacterAttackEnd, false);
//			_skeletonAnimationBody.skeleton.SetAttachment("speed0001",null );
		}
	}

	//Spine Event
	private void OnPetSkeletonEvent(Spine.AnimationState animationState, int trackIndex, Spine.Event e) 
	{
		if(e.Data.name == "SetSortOrder"){
			_skeletonAnimationPet.GetComponent<Renderer>().sortingOrder = e.Int;
		}
		_petInited = true;

		Spine.Slot separatorSlot = _skeletonAnimationPet.Skeleton.FindSlot("separator");
		if(separatorSlot != null){
			Spine.Unity.Modules.SkeletonRenderSeparator seperator = _skeletonAnimationPet.gameObject.AddComponent<Spine.Unity.Modules.SkeletonRenderSeparator>();
			seperator.SkeletonRenderer = _skeletonAnimationPet;
			seperator.SkeletonRenderer.separatorSlots.Add(separatorSlot);
			Spine.Unity.Modules.SkeletonPartsRenderer parts0 = Spine.Unity.Modules.SkeletonPartsRenderer.NewPartsRendererGameObject(_skeletonAnimationPet.transform, "0");
			Spine.Unity.Modules.SkeletonPartsRenderer parts1 = Spine.Unity.Modules.SkeletonPartsRenderer.NewPartsRendererGameObject(_skeletonAnimationPet.transform, "1");
			parts0.gameObject.layer = this.gameObject.layer;
			parts0.MeshRenderer.sortingLayerName = _skeletonAnimationPet.GetComponent<MeshRenderer>().sortingLayerName;
			parts1.gameObject.layer = this.gameObject.layer;
			parts1.MeshRenderer.sortingLayerName = _skeletonAnimationPet.GetComponent<MeshRenderer>().sortingLayerName;
			parts1.MeshRenderer.sortingOrder = 20;
			seperator.partsRenderers.Add(parts0);
			seperator.partsRenderers.Add(parts1);
			seperator.enabled = true;
		}
	}
}
