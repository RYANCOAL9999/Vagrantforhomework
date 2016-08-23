using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using KoocellUnityPlugin.UI_Page;

public class HomePage : SubPage {

	protected Transform pageContent;
	protected Transform[] iconObject;
	//public Transform buttonPet;

	//UIPet _uiPet;

	protected override void Awake () {
		base.Awake();
		pageContent = GameObject.Find ("Canvas/MainScene/PageContent").transform;
		iconObject = new Transform[3];
		iconObject[0] = pageContent.FindChild("iconlv");
		iconObject[1] = pageContent.FindChild("icontoken");
		iconObject[2] = pageContent.FindChild ("icongold");
		//_uiPet = buttonPet.FindChild ("UIPet").GetComponent<UIPet> ();
	}

	protected override void Start () {
		base.Start();
		iconObject[0].FindChild ("Text").GetComponent<Text> ().text = GameDataManager.Instance.userData.lv+"/999";
		iconObject[0].FindChild ("statuscolorbar").GetComponent<Image> ().fillAmount = GameDataManager.Instance.userData.lv/100;
		iconObject[1].FindChild ("Text").GetComponent<Text> ().text = GameDataManager.Instance.userData.realCoin + "";
		//iconObject[1].FindChild ("statuscolorbar").GetComponent<Image> ().fillAmount = GameDataManager.Instance.userData.realCoin/100;
		iconObject[2].FindChild ("Text").GetComponent<Text> ().text = GameDataManager.Instance.userData.coin + "";
		//iconObject[2].FindChild ("statuscolorbar").GetComponent<Image> ().fillAmount = GameDataManager.Instance.userData.coin/100;
		/*if(GameDataManager.Instance.userData.selectedCharacter.equipedPetData != null){
			buttonPet.FindChild ("PetImage").GetComponent<Image> ().sprite = GameDataManager.Instance.userData.selectedCharacter.equipedPetData.spriteIcon;
		}*/

	}

	protected override void Update () {
		base.Update();


	}


	//hints:this.transform.parent <maybe a lot of parent/a parent> .GetChild <maybe a lot of child/a child> (i).name = this.name for InviteToRunButton search parents and child know that to enabled functions. 
	public override void PageBtnClick(Button button) {
		base.PageBtnClick(button);
		switch (button.name) {

			case "GOButton":
			    GameDataManager.Instance.userData.selectedCharacterIndex = transform.FindChild("ImageObject/CharacterImage/CharacterSelector").GetComponent<CharacterSelector>()._currentIdx;
				Application.LoadLevel("Ready");
				break;

			case "InviteToRunButton":
				break;

			case "InvitedButton":
				break;

			case "ButtonPet":
				mainPage.OpenPage(PageManager.pageResourcesPath + "Pet");
				break;

			case "UICharacter":
				mainPage.OpenPage (PageManager.pageResourcesPath + "Character");
				break;
		
		}



	}

}
