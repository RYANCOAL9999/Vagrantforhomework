using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using KoocellUnityPlugin.UI_Page;

public class CharacterPage : SubPage {

	public Transform content;
	public Transform[] subPanel;
	public Transform skillSet;
	public GameObject textImage;



	List<GameObject> TextImageList = new List<GameObject> ();
	List <Transform> CharacterButton = new List<Transform>();
	Transform[] values;
	CharacterSelector characterSelector;


	protected override void Awake () {
		base.Awake();

		subPanel [0].gameObject.SetActive (true);

		values = new Transform[5];

		values[0] = subPanel [0].FindChild ("TextGroup/LV");
		values[1] = subPanel [0].FindChild ("TextGroup/MAX");
		values[2] = subPanel [0].FindChild ("Speed");
		values[3] = subPanel [0].FindChild ("Lucky");
		values[4] = subPanel [0].FindChild ("Jump");
		characterSelector = transform.FindChild ("Transform/ImageObject/CharacterImage/CharacterSelector").GetComponent<CharacterSelector> (); 

	}

	protected override void Start () {
		base.Start();
		for (int i = 0; i < skillSet.childCount; i++) {
			GameObject image = Instantiate (textImage);
			SkillData skillData = GameDataManager.Instance.userData.selectedCharacter.equipedClothData.GetSkillData (i);
			if(skillData != null){
				image.GetComponent<Image> ().sprite = skillData.spriteIcon;
				image.transform.SetParent (skillSet.GetChild (i), false);
				TextImageList.Add (image);
			}
		}

	}

	protected override void Update () {
		base.Update();
		foreach (Transform abc in content) {
			CharacterButton.Add (abc);
			float X = content.GetComponent<RectTransform> ().anchoredPosition.x + abc.GetComponent<RectTransform> ().anchoredPosition.x;
			HandleSubPanel (X, abc);
		}
	}

	public override void PageBtnClick(Button button) {
		base.PageBtnClick(button);
			switch (button.name) {

		case "ConfirmButton":
				GameDataManager.Instance.userData.selectedCharacterIndex = characterSelector._currentIdx;
				break;

			case "Upgradebutton":
				break;

			case "BlockButton":
				break;

		}

	}

	void HandleSubPanel(float thanX, Transform abc){
		if ( !(thanX < 1 || thanX > 220)) {
			UICharacter character = abc.GetComponent<UICharacter> ();
			if (character.characterData.unlocked == true) {
				subPanel [0].gameObject.SetActive (true);
				subPanel [1].gameObject.SetActive (false);
			} 
			else {
				subPanel [0].gameObject.SetActive (false);
				subPanel [1].gameObject.SetActive (true);
			}
			for (int i = 0; i < TextImageList.Count; i++) {
				TextImageList[i].GetComponent<Image>().sprite = GameDataManager.Instance.userData.selectedCharacter.equipedClothData.GetSkillData (i).spriteIcon;
			}
		
				
			values [0].GetComponent<Text> ().text = GameDataManager.Instance.userData.selectedCharacter.lv + "/99";
			if (GameDataManager.Instance.userData.selectedCharacter.lv == 99) {
					values [1].gameObject.SetActive (true);
			}


			values[2].FindChild("Value").GetComponent<Text> ().text = character.characterData.speed + "";
			values[2].FindChild("statuscolorbar").GetComponent<Image> ().fillAmount = character.characterData.speed / 100;
			values[3].FindChild("Value").GetComponent<Text> ().text = character.characterData.luck + "";
			values[3].FindChild("statuscolorbar").GetComponent<Image> ().fillAmount = character.characterData.luck / 100;
			values[4].FindChild("Value").GetComponent<Text> ().text = character.characterData.jump + "";
			values[4].FindChild("statuscolorbar").GetComponent<Image> ().fillAmount = character.characterData.jump / 100;
		}
	}
		
}
