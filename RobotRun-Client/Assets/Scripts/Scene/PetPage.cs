using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using KoocellUnityPlugin.UI_Page;

public class PetPage : SubPage {
	
	public Transform[] values;

	public GameObject petPrefab;
	public Transform content;
	private UIPet _uiPet;
	private Transform buyPage;
	private Transform icongold;

	protected override void Awake () {
		
		base.Awake();
		_uiPet = transform.FindChild("charbg/UIPet").GetComponent<UIPet>();
		buyPage = transform.FindChild ("BUYPAGE");
		icongold = GameObject.Find ("Canvas/MainScene/PageContent/icongold").transform;
	}

	protected override void Start () {
		base.Start();
		_uiPet.petData = GameDataManager.Instance.userData.selectedCharacter.equipedPetData;
		InstantaitePet();

	}

	protected override void Update () {
		base.Update();

		values [0].GetComponent<Text>().text = _uiPet.petData.duration + "";
		values [0].parent.GetComponent<Image> ().fillAmount = _uiPet.petData.duration / 100;

		values [1].GetComponent<Text>().text = _uiPet.petData.speed + "";
		values [1].parent.GetComponent<Image> ().fillAmount = _uiPet.petData.speed / 100;

		values [2].GetComponent<Text>().text = _uiPet.petData.attackDuration + "";
		values [2].parent.GetComponent<Image> ().fillAmount = _uiPet.petData.attackDuration / 100;

	}

	public override void PageBtnClick(Button button) {
		base.PageBtnClick(button);
		switch (button.name) {

			case "ConfirmButton":
				GameDataManager.Instance.userData.selectedCharacter.equipedPetID = _uiPet.petData.id;
				break;

			case "GETNEWPETButton":
				break;

		   case "Yes":
				icongold.FindChild ("Text").GetComponent<Text> ().text = (int.Parse (icongold.FindChild ("Text").GetComponent<Text> ().text) + 200) + "";
				buyPage.gameObject.SetActive(false);
				break;
			 
			case "No":
				buyPage.gameObject.SetActive(false);
				break;
		}
	}

	void InstantaitePet(){
		foreach (Transform abc in content) {
			Destroy (abc.gameObject);
		}
			
		foreach (PetData petData in GameDataManager.Instance.listPetData) {
			GameObject petContainer = Instantiate (petPrefab);
			PetPrefab _petPrefab = petContainer.transform.FindChild("UIPet").GetComponent<PetPrefab>();
			_petPrefab.petData = petData;
			_petPrefab.sellButton = OnsellButton;
			_petPrefab.setButton = OnsetButton;
			petContainer.transform.SetParent (content, false);
		}
	}

	void OnsetButton(PetPrefab petPrefab){

		foreach(Transform abc in content){
			abc.FindChild ("BlackImage").GetComponent<Image> ().color = Color.clear;
			abc.FindChild ("BlackImage/EQUIPED").gameObject.SetActive (false);
			abc.FindChild ("BlackImage/SELLButton").gameObject.SetActive (true);
			abc.FindChild ("BlackImage/SETButton").gameObject.SetActive (true);
		}

		petPrefab.sell.gameObject.SetActive(false);
		petPrefab.set.gameObject.SetActive (false);
		petPrefab.blackImage.GetComponent<Image> ().color = Color.white;
		petPrefab.eQUIPED.gameObject.SetActive (true);
		_uiPet.petData = petPrefab.petData;


	}

	void OnsellButton(PetPrefab petPrefab){

		foreach(Transform abc in content){
			abc.FindChild ("BlackImage").GetComponent<Image> ().color = Color.clear;
			abc.FindChild ("BlackImage/EQUIPED").gameObject.SetActive (false);
			abc.FindChild ("BlackImage/SELLButton").gameObject.SetActive (true);
			abc.FindChild ("BlackImage/SETButton").gameObject.SetActive (true);
		}
		//meybe GameDataManager.Instance.userData.selectedCharacter.equipedPetData.id without -1

		int vlaues = GameDataManager.Instance.userData.selectedCharacter.equipedPetData.id - 1;
		if(vlaues >= 0){
			content.GetChild (vlaues).FindChild ("BlackImage").GetComponent<Image> ().color = Color.white;
			content.GetChild (vlaues).FindChild ("BlackImage/EQUIPED").gameObject.SetActive (true);
			content.GetChild (vlaues).FindChild ("BlackImage/SELLButton").gameObject.SetActive (false);
			content.GetChild (vlaues).FindChild ("BlackImage/SETButton").gameObject.SetActive (false);
		}
		buyPage.gameObject.SetActive(true);

	}





}
