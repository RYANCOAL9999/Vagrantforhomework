using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Spine.Unity;
using System;

public class UIPet : MonoBehaviour {

	private SkeletonGraphic _skeletonGraphic;
	//public Transform pages;

	private PetData _petData;
	public PetData petData{
		get{
			return _petData;
		}
		set{ 
			_petData = value;
			UpdateUI ();
		}


	}

	void Awake(){
		_skeletonGraphic = GetComponent<SkeletonGraphic> ();
	
	}

	void Start () {
		//if (pages.gameObject.name == "Home(Clone)") {
		//	_skeletonGraphic.AnimationState.ClearTracks ();
		//}
	}
	

	void Update () {
	
	}

	void UpdateUI(){
		SkeletonDataAsset assetPet = _petData.skeletonDataAsset;

		_skeletonGraphic.skeletonDataAsset = assetPet;
		if(_petData.skinName != ""){
			_skeletonGraphic.initialSkinName = _petData.skinName;

		}

		_skeletonGraphic.Initialize(true);
		_skeletonGraphic.AnimationState.SetAnimation (0, Character.animationPetIdle, true);
	
		
	}


}
