using UnityEngine;
using System.Collections;
using Spine.Unity;

public class UICharacter : Character {
	SkeletonGraphic _skeletonGraphicBody;
	SkeletonGraphic _skeletonGraphicHead;
	SkeletonGraphic _skeletonGraphicPet;
	UIBoneFollower _boneFollowerBody;


	public override bool isRiding{
		get{
			return _skeletonGraphicPet.gameObject.activeSelf;
		}
	}

	public SkeletonGraphic skeletonGraphicBody{
		get{
			return _skeletonGraphicBody;
		}
	}

	public SkeletonGraphic skeletonGraphicHead{
		get{
			return _skeletonGraphicHead;
		}
	}

	public SkeletonGraphic skeletonGraphicPet{
		get{
			return _skeletonGraphicPet;
		}
	}

	protected override void Awake () {
		base.Awake();
		_skeletonGraphicBody = this.transform.FindChild("BodySpine").GetComponent<SkeletonGraphic>();
		_skeletonGraphicHead = this.transform.FindChild("HeadSpine").GetComponent<SkeletonGraphic>();
		_skeletonGraphicPet = this.transform.FindChild("PetSpine").GetComponent<SkeletonGraphic>();
		_boneFollowerBody = _skeletonGraphicBody.GetComponent<UIBoneFollower>();
	}

	protected override void Start () {
		base.Start();
		_skeletonGraphicPet.gameObject.SetActive(false);
	}

	protected override void Update () {
		base.Update();
	}

	protected override void UpdateCharacter(){

		SkeletonDataAsset assetBody = _characterData.bodySkeletonDataAsset;
		_skeletonGraphicBody.skeletonDataAsset = assetBody;
		if(_characterData.bodySpineSkinName != ""){
			_skeletonGraphicBody.initialSkinName = _characterData.bodySpineSkinName;
		}
		//		_skeletonGraphicBody.OnRebuild += Apply;
		_skeletonGraphicBody.Initialize(true);
		_skeletonGraphicBody.AnimationState.SetAnimation (0, animationCharacterIdle, true);
//		_skeletonGraphicBody.GetComponent<Renderer>().sortingLayerName = "Player";
		//		spriteRenderer.sortingLayerID = renderer.sortingLayerID;
		//		spriteRenderer.sortingOrder = renderer.sortingOrder;


		SkeletonDataAsset assetHead = _characterData.headSkeletonDataAsset;
		_skeletonGraphicHead.skeletonDataAsset = assetHead;
		if(_characterData.headSpineSkinName != ""){
			_skeletonGraphicHead.initialSkinName = _characterData.headSpineSkinName;
		}
		_skeletonGraphicHead.GetComponent<UIBoneFollower>().boneName = "head";
//		_skeletonGraphicHead.OnRebuild += ApplyBlink;
		_skeletonGraphicHead.Initialize(true);
		//		_skeletonGraphicHead.skeleton.SetAttachment("attachment_001", null);
		//		_skeletonGraphicHead.state.SetAnimation (0, animationCharacterIdle, true);
		//		_skeletonGraphicHead.GetComponent<Renderer>().sortingLayerName = "Player";
		if(_characterData.equipedPetData != null){
			SkeletonDataAsset assetPet = _characterData.petSkeletonDataAsset;
			_skeletonGraphicPet.skeletonDataAsset = assetPet;
			if(_characterData.petSpineSkinName != ""){
				_skeletonGraphicPet.initialSkinName = _characterData.petSpineSkinName;
			}
			_skeletonGraphicPet.Initialize(true);
			_skeletonGraphicPet.AnimationState.SetAnimation (0, animationPetIdle, true);
		}

		_boneFollowerBody = _skeletonGraphicBody.gameObject.GetComponent<UIBoneFollower>();

		StartBlink();
	}

	protected override IEnumerator Blink() {
		while (true) {
			_skeletonGraphicHead.Skeleton.SetAttachment(faceSlot, faceAttachment1);
			yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
			_skeletonGraphicHead.Skeleton.SetAttachment(faceSlot, faceAttachment2);
			yield return new WaitForSeconds(0.02f);
			_skeletonGraphicHead.Skeleton.SetAttachment(faceSlot, faceAttachment3);
			yield return new WaitForSeconds(0.02f);
			_skeletonGraphicHead.Skeleton.SetAttachment(faceSlot, faceAttachment2);
			yield return new WaitForSeconds(0.02f);
		}
	}

	public override void SetCharacterState (CharacterState state)
	{
		if (_currentCharacterState != state && _skeletonGraphicBody.skeletonDataAsset != null) {
			_currentCharacterState = state;

			switch (_currentCharacterState) {
			case CharacterState.Idle:
				_skeletonGraphicBody.AnimationState.SetAnimation (0, animationCharacterIdle, true);
				break;
			case CharacterState.Run1:
				_skeletonGraphicBody.AnimationState.SetAnimation (0, animationCharacterRun1, true);
				break;
			case CharacterState.Run2:
				_skeletonGraphicBody.AnimationState.SetAnimation (0, animationCharacterRun2, true);
				break;
			case CharacterState.Run3:
				_skeletonGraphicBody.AnimationState.SetAnimation (0, animationCharacterRun3, true);
				break;
			case CharacterState.Jump:
				_skeletonGraphicBody.AnimationState.SetAnimation (0, animationCharacterJump, true);
				break;
			case CharacterState.Jump2:
				_skeletonGraphicBody.AnimationState.SetAnimation (0, animationCharacterDoubleJump, false);
				_skeletonGraphicBody.AnimationState.AddAnimation (0, animationCharacterFall, true, 0);
				break;
			case CharacterState.Fall:
				_skeletonGraphicBody.AnimationState.SetAnimation (0, animationCharacterFall, true);
				break;
			case CharacterState.Hurt1:
				_skeletonGraphicBody.AnimationState.ClearTrack (1);
				_skeletonGraphicBody.AnimationState.SetAnimation (0, animationCharacterHurt1, false);
				break;
			case CharacterState.Hurt2:
				_skeletonGraphicBody.AnimationState.ClearTrack (1);
				_skeletonGraphicBody.AnimationState.SetAnimation (0, animationCharacterHurt2, false);
				break;
			case CharacterState.Hurt3:
				_skeletonGraphicBody.AnimationState.ClearTrack (1);
				_skeletonGraphicBody.AnimationState.SetAnimation (0, animationCharacterHurt3, false);
				break;
			case CharacterState.Ride:
				_skeletonGraphicBody.AnimationState.SetAnimation (0, animationCharacterRide, true);
				break;
			}
			if(_currentCharacterState == CharacterState.Hurt1 
				|| _currentCharacterState == CharacterState.Hurt2 
				|| _currentCharacterState == CharacterState.Hurt3){
				StopBlink();
				_skeletonGraphicHead.Skeleton.SetAttachment(faceSlot, faceAttachment4);
//				if(_holdingSkillData != null){
//					_holdingSkillData.gameObject.SetActive(false);
//				}
			}else{
				StartBlink();
//				if(_holdingSkillData != null){
//					_holdingSkillData.gameObject.SetActive(true);
//				}
			}
		}
	}

	public override void SetPetState (PetState state)
	{
		if (_currentPetState != state && _skeletonGraphicPet.skeletonDataAsset != null) {
			_currentPetState = state;

			switch (_currentPetState) {
			case PetState.Idle:
				_skeletonGraphicPet.AnimationState.SetAnimation (0, animationPetIdle, true);
				break;
			case PetState.Run:
				_skeletonGraphicPet.AnimationState.SetAnimation (0, animationPetRun, true);
				break;
			case PetState.Jump:
				_skeletonGraphicPet.AnimationState.SetAnimation (0, animationPetJump, true);
				break;
			case PetState.Fall:
				_skeletonGraphicPet.AnimationState.SetAnimation (0, animationPetFall, true);
				break;
			case PetState.Attack:
				_skeletonGraphicPet.AnimationState.SetAnimation (0, animationPetAttack, true);
				break;
			}
		}
	}

	public override void Ride(){
		base.Ride();
		_boneFollowerBody.enabled = true;
		_boneFollowerBody.boneName = boneSeatPos;
		_skeletonGraphicPet.gameObject.SetActive(true);
		this.transform.position = new Vector3(this.transform.position.x, _skeletonGraphicBody.transform.position.y);
	}

	public override void UnRide(){
		base.UnRide();
		_boneFollowerBody.enabled = false;
		_skeletonGraphicPet.gameObject.SetActive(false);
		_skeletonGraphicBody.transform.localPosition = Vector2.zero;
		_skeletonGraphicBody.transform.localRotation = Quaternion.identity;
	}

//	public override Item Hold(string prefabNameitem){
//		if(_holdingItem == null){
//			_skeletonGraphicBody.AnimationState.SetAnimation (1, Character.animationCharacterHold, true);
//		}
//		return base.Hold(prefabNameitem);
//	}

	public override void UseSkill(){
		if(_holdingSkillData != null){
			_skeletonGraphicBody.AnimationState.SetAnimation (1, Character.animationCharacterThrow, false);
		}
		base.UseSkill();
	}

	public override void ShowGun(){
		base.ShowGun();
		_skeletonGraphicBody.AnimationState.SetAnimation (1, Character.animationCharacterGun, true);
	}

	public override void HideGun(){
		base.HideGun();
		_skeletonGraphicBody.AnimationState.ClearTrack(1);
	}

	public void Enable(){
		_skeletonGraphicBody.color = Color.white;
		_skeletonGraphicHead.color = Color.white;
		_skeletonGraphicPet.color = Color.white;
	}

	public void Disable(){
		_skeletonGraphicBody.color = Color.black;
		_skeletonGraphicHead.color = Color.black;
		_skeletonGraphicPet.color = Color.black;
	}
}
