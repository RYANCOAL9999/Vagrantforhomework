using UnityEngine;
using System.Collections;
using Spine.Unity;

public class Character : MonoBehaviour {

	public enum CharacterState
	{
		Null,
		Idle,
		Run1,
		Run2,
		Run3,
		Jump,
		Jump2,
		Fall,
		Hurt1,
		Hurt2,
		Hurt3,
		Ride,
		Gun
	}

	public enum PetState
	{
		Null,
		Idle,
		Run,
		Jump,
		Fall,
		Attack
	}

	public static Color GetPlayerColor(int index){
		switch(index){
		case 0:
			return new Color(190/255f, 32/255f, 38/255f);
		case 1:
			return new Color(254/255f, 233/255f, 94/255f);
		case 2:
			return new Color(137/255f, 197/255f, 64/255f);
		case 3:
			return new Color(103/255f, 204/255f, 230/255f);
		}
		Debug.LogError("Should not use this index: "+index);
		return Color.white;
	}

	public const string animationCharacterIdle = "idle";
	public const string animationCharacterRun1 = "run";
	public const string animationCharacterRun2 = "run2";
	public const string animationCharacterRun3 = "run3";
	public const string animationCharacterJump = "jump";
	public const string animationCharacterDoubleJump = "jump_double";
	public const string animationCharacterFall = "fall";
	public const string animationCharacterHurt1 = "hurt";
	public const string animationCharacterHurt2 = "hurt2";
	public const string animationCharacterHurt3 = "hurt3";
	public const string animationCharacterThrow = "throw";
	public const string animationCharacterHold = "hold";
	public const string animationCharacterGun = "attack";
	public const string animationCharacterRide = "runwithpet";
	public const string animationCharacterSpeed = "speed";
	public const string animationCharacterAttackEnd = "attackend";

	public const string animationPetInit = "init";
	public const string animationPetIdle = "idle";
	public const string animationPetRun = "run";
	public const string animationPetJump = "jump";
	public const string animationPetFall = "fall";
	public const string animationPetAttack = "attack";
	public const string animationPetAttackEnd = "attackend";
	 
	public const string boneSeatPos = "seat";
	public const string boneHoldPos = "item";
	 
	//for eye blink
	public const string faceSlot = "face";
	public const string faceAttachment1 = "face1";
	public const string faceAttachment2 = "face2";
	public const string faceAttachment3 = "face3";
	public const string faceAttachment4 = "face4";//hurt

	//gun
	public const string gunSlot = "SK042";
	public const string gunAttachment = "SK042";


	protected CharacterState _currentCharacterState = CharacterState.Null;
	protected PetState _currentPetState = PetState.Null;


	Coroutine _coroutineBlink = null;

	protected CharacterData _characterData;
	public CharacterData characterData{
		get{
			return _characterData;
		}
		set{
			_characterData = value;
			UpdateCharacter();
		}
	}

	public virtual bool isRiding{
		get{
			return false;
		}
	}

	protected SkillData _holdingSkillData = null;
	public SkillData holdingSkillData{
		get{
			return _holdingSkillData;
		}
	}

	public CharacterState currentCharacterState{
		get{
			return _currentCharacterState;
		}
	}

	public PetState currentPetState{
		get{
			return _currentPetState;
		}
	}

	protected virtual void Awake(){
		
	}

	protected virtual void Start () {
		
	}

	protected virtual void Update () {
	
	}

	protected virtual void UpdateCharacter(){
		
	}

	protected void StartBlink(){
		if(_coroutineBlink == null){
			_coroutineBlink = StartCoroutine("Blink");
		}
	}

	protected void StopBlink(){
		if(_coroutineBlink != null){
			StopCoroutine(_coroutineBlink);
			_coroutineBlink = null;
		}
	}

	protected virtual IEnumerator Blink() {
		yield return 0;
	}

	public virtual void SetCharacterState (CharacterState state)
	{
		
	}

	public virtual void SetPetState (PetState state)
	{

	}

	public virtual void Ride(){
		
	}

	public virtual void UnRide(){
		
	}

//	public virtual Item Hold(string prefabNameitem){
//		if(_holdingItem == null){
//			_holdingItem = Instantiate(Resources.Load<Item>(prefabNameitem));
//			_holdingItem.prefabName = prefabNameitem;
//			_holdingItem.transform.SetParent(this.transform, false);
//
//			return _holdingItem;
//		}
//		return null;
//	}

	public virtual void UseSkill(){
		if(_holdingSkillData != null){
//			Destroy(_holdingSkillData.gameObject);
			_holdingSkillData = null;
		}
	}

	public virtual void ShowGun(){
		
	}

	public virtual void HideGun(){

	}
}
