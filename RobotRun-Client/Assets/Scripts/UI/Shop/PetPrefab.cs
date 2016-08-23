using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Spine.Unity;
using System;

public class PetPrefab : MonoBehaviour {

	public  Action<PetPrefab> sellButton;
	public  Action<PetPrefab> setButton;

	[HideInInspector]
	public Transform eQUIPED;
	[HideInInspector]
	public Transform blackImage;
	[HideInInspector]
	public Transform sell;
	[HideInInspector]
	public Transform set;

	private SkeletonGraphic _skeletonGraphic;

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
		blackImage = transform.parent.FindChild ("BlackImage");
		eQUIPED = transform.parent.FindChild ("BlackImage/EQUIPED");
		set = transform.parent.FindChild ("BlackImage/SELLButton");
		sell = transform.parent.FindChild ("BlackImage/SETButton");

	}

	void Start () {
		_skeletonGraphic.AnimationState.ClearTracks ();
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

		if (_petData.id == GameDataManager.Instance.userData.selectedCharacter.equipedPetID) {
			blackImage.GetComponent<Image> ().color = Color.white;
			eQUIPED.gameObject.SetActive (true);
			set.gameObject.SetActive (false);
			sell.gameObject.SetActive (false);
		} 
		else {
			blackImage.GetComponent<Image> ().color = Color.clear;
			eQUIPED.gameObject.SetActive (false);
			set.gameObject.SetActive (true);
			sell.gameObject.SetActive (true);
		}

	}
		

	public void OnClickedSell(){

		if (sellButton != null) {
			sellButton (this);
		}

	}

	public void OnClickedSet(){

		if (setButton != null) {
			setButton (this);
		}

	}
}
