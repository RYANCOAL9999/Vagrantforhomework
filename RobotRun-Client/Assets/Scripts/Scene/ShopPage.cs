using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using KoocellUnityPlugin.UI_Page;

public class ShopPage : SubPage {

	public RectTransform content;
	public GameObject prefabShopIconButton;
	public Transform buybutton;
	public Transform skillSet;
	public GameObject textImage;

	[HideInInspector]
	public Transform[] CharacterButton;
	[HideInInspector]
	public int CharacterlistHandler = 0;
	[HideInInspector]
	public int Characterlist = 0;

	UICharacter _uiCharacter;
	CharacterData _clonedCharacterData;
	int iconContainerid = 0;
	string gettingData ="";
	bool setHeadORClothes;

	protected override void Awake () {
		base.Awake();
		_uiCharacter = transform.FindChild("Transform").FindChild ("UICharacter").GetComponent<UICharacter> ();
		InstantaiteHead ();

	}

	protected override void Start () {
		base.Start();
		CloneCharacterData ();
		_uiCharacter.characterData = _clonedCharacterData;
		setHeadORClothes = false;

	}

	protected override void Update () {
		base.Update();
		Characterlist = content.childCount;
		CharacterButton = new Transform[Characterlist];
		for (int i = 0; i < CharacterButton.Length; i++) {
			CharacterButton [i] = content.transform.GetChild (i);
		}
	}

	void fixingposition(){
		float NewAnchoredPositionX = -1 * (CharacterButton [CharacterlistHandler].GetComponent<RectTransform> ().anchoredPosition.x - CharacterButton [CharacterlistHandler].GetComponent<RectTransform> ().rect.width/2);
		content.anchoredPosition = new Vector2 (NewAnchoredPositionX, content.anchoredPosition.y);
	}

	public void LeftArrowClick(){
		if (CharacterlistHandler < Characterlist - 1) {
			CharacterlistHandler += 1;
			fixingposition ();
		}
	}

	public void RightArrowClick(){
		if (CharacterlistHandler > 0) {
			CharacterlistHandler -= 1;
			fixingposition ();
		}
	}

	public override void PageBtnClick(Button button) {
		
		base.PageBtnClick(button);
		switch (button.name) {

			case "ConfirmButton":
				//Temp
				for (int i = 0; i < GameDataManager.Instance.userData.listCharacterData.Count; i++) {
					CharacterData characterData = GameDataManager.Instance.userData.listCharacterData [i];
					if (characterData.id == _clonedCharacterData.id) {
						GameDataManager.Instance.userData.listCharacterData [i] = _clonedCharacterData;
						break;
					}
				}

				if (setHeadORClothes == true) {
					InstantiateCloth ();
				} 
				else {
					InstantaiteHead();
				}
				break;
			
			case "iconcharButton":
				skillSet.gameObject.SetActive (false);
				InstantaiteHead();
				break;

			case "iconShirtButton":
				skillSet.gameObject.SetActive (true);
				Addskillpicture(GameDataManager.Instance.userData.selectedCharacter.equipedClothData);
				InstantiateCloth();
				break;

			case "iconeffectButton":
				break;

			case "BUYButton":

				if (gettingData == "HeadItemData") {
					//Error handling with HeadItemData
					//if (iconContainerid == 17) {
					//	CharacterButton [4].FindChild ("Price").gameObject.SetActive (false);	
					//} else if (iconContainerid == 18) {
					//	CharacterButton [5].FindChild ("Price").gameObject.SetActive (false);	
					//} else {
						CharacterButton [iconContainerid - 1].FindChild ("Price").gameObject.SetActive (false);
					//}
					buybutton.gameObject.SetActive (false);
					
				} else if (gettingData == "ClothData") {
					//Error handling with clothData
					if (iconContainerid == 18) {
						CharacterButton [5].FindChild ("Price").gameObject.SetActive (false);	
					} else if (iconContainerid == 19) {
						CharacterButton [6].FindChild ("Price").gameObject.SetActive (false);
					}
					 else {
						CharacterButton [iconContainerid -1].FindChild ("Price").gameObject.SetActive (false);
					}
					buybutton.gameObject.SetActive (false);
				}
				break;

		}
			
			
	}

	void InstantaiteHead(){
		foreach (Transform abc in content) {
			Destroy (abc.gameObject);
		}
		foreach (HeadItemData headItemData in GameDataManager.Instance.listHeadItemData) {
			GameObject iconContainer = Instantiate (prefabShopIconButton);
			ShopIconButton shopIconButton = iconContainer.GetComponent<ShopIconButton> ();
			shopIconButton.iconData = headItemData;
			shopIconButton.callbackOnClick = OnShopIconButtonClicked;
			iconContainer.transform.SetParent (content, false);
		}	
		setHeadORClothes = false;
	}

	void InstantiateCloth(){
		foreach (Transform abc in content) {
			Destroy (abc.gameObject);
		}
		foreach (ClothData clothData in GameDataManager.Instance.listClothData) {
			GameObject iconContainer = Instantiate (prefabShopIconButton);
			ShopIconButton shopIconButton = iconContainer.GetComponent<ShopIconButton> ();
			shopIconButton.iconData = clothData;
			shopIconButton.callbackOnClick = OnShopIconButtonClicked;
			iconContainer.transform.SetParent (content, false);
		}
		setHeadORClothes = true;
	}

	void OnShopIconButtonClicked(ShopIconButton shopIconButton){

		if (shopIconButton.iconData.GetType () == typeof(ClothData)) {
			_clonedCharacterData.equipedClothID = shopIconButton.iconData.id;
			ClothData clothData = (ClothData)shopIconButton.iconData;
			Addskillpicture (clothData);

		} else if (shopIconButton.iconData.GetType () == typeof(HeadItemData)) {
			_clonedCharacterData.equipedHeadID = shopIconButton.iconData.id;
		}
			
		gettingData = shopIconButton.GetComponent<ShopIconButton> ().iconData.ToString();
		iconContainerid = shopIconButton.GetComponent<ShopIconButton> ().iconData.id;
		//Debug.Log(iconContainerid);
		_uiCharacter.characterData = _clonedCharacterData;
		//Debug.Log (GameDataManager.Instance.userData.listInventoryClothID.Contains (iconContainerid));
		//Debug.Log (shopIconButton.transform.FindChild ("Price").gameObject.activeSelf);
		if (!GameDataManager.Instance.userData.listInventoryClothID.Contains (iconContainerid) && shopIconButton.transform.FindChild("Price").gameObject.activeSelf == true) {
			buybutton.gameObject.SetActive (true);
		} 
		else {
			buybutton.gameObject.SetActive (false);
		}

	}
		
	void CloneCharacterData(){
		_clonedCharacterData = new CharacterData (GameDataManager.Instance.userData.selectedCharacter);
	}

	void Addskillpicture(ClothData clothData){
		foreach (Transform abc in skillSet) {
			if (abc.FindChild ("Image(Clone)") != null) {
				Destroy (abc.FindChild ("Image(Clone)").gameObject);
			} 
		}
		for (int i = 0; i < skillSet.childCount; i++) {
			GameObject image = Instantiate (textImage);
			SkillData skillData = clothData.GetSkillData (i);
			if(skillData != null){
				image.GetComponent<Image>().sprite = skillData.spriteIcon;
				image.transform.SetParent (skillSet.GetChild (i), false);	
			}
		}
	}

}
