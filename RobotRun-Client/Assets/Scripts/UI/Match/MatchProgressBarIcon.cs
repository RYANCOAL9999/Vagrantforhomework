using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Spine.Unity;

public class MatchProgressBarIcon : MonoBehaviour {
	Image _imageBG;
	SkeletonGraphic _skeletonGraphicHead;

	protected MatchCharacterController _matchCharacterController = null;
	public MatchCharacterController matchCharacterController{
		get{
			return _matchCharacterController;
		}
		set{
			_matchCharacterController = value;
			UpdateCharacter();
		}
	}

	protected int _index = -1;
	public int index{
		get{
			return _index;
		}
		set{
			_index = value;
			_imageBG.color = Character.GetPlayerColor(_index);
		}
	}

	void Awake (){
		_imageBG = this.GetComponent<Image>();
		_skeletonGraphicHead = this.transform.FindChild("SkeletonGraphic").GetComponentInChildren<SkeletonGraphic>();
	}

	void Start () {
	
	}

	void Update () {
	
	}

	void UpdateCharacter(){
		CharacterData characterData = _matchCharacterController.matchCharacter.characterData;
		SkeletonDataAsset assetHead = characterData.headSkeletonDataAsset;
		_skeletonGraphicHead.skeletonDataAsset = assetHead;
		if(characterData!= null && characterData.headSpineSkinName != ""){
			_skeletonGraphicHead.initialSkinName = characterData.headSpineSkinName;
		}
		_skeletonGraphicHead.Initialize(true);
	}
}
