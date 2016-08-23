using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using KoocellUnityPlugin.UI_Page;
using KoocellUnityPlugin.Localization;

public class MainScene : MainPage {
	
	Page _currentPage = null;
	[HideInInspector]
	float time_f = 0f;
	[HideInInspector]
	float timer_i = 150f;
	public Transform[] ButtonFromImage;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
		_currentPage = OpenPage(PageManager.pageResourcesPath+"Home");
		changeColor (0);

	}

	protected override void Update()
	{
		base.Update ();
		time_f += Time.timeScale;
		if (time_f > timer_i ) {
		//	PageManager.OpenPopup (PageManager.pageResourcesPath + "Notification", 1f);
			time_f = 0;
		}

	}

	public override void PageBtnClick(Button button) {
		
		base.PageBtnClick (button);


		switch (button.name) {

			case "ButtonShop":
				if (_currentPage.transform.name != "Shop(Clone)") {
					Localization.language = "Chinese";
					Localization.Get ("name");
					_currentPage = OpenPage (PageManager.pageResourcesPath + "Shop");
					changeColor (1);
				}
				break;

			case "ButtonCharacter":
				if (_currentPage.transform.name != "Character(Clone)") {
					_currentPage = OpenPage (PageManager.pageResourcesPath + "Character");
					changeColor (2);
				}
				break;

			case "ButtonPet":
				if (_currentPage.transform.name != "Pet(Clone)") {
					_currentPage = OpenPage (PageManager.pageResourcesPath + "Pet");
					changeColor (3);
				}
				break;

			case "ButtonFriend":
				if (_currentPage.transform.name != "Friend(Clone)") {
					_currentPage = OpenPage (PageManager.pageResourcesPath + "Friend");
					changeColor (4);
				}
				break;

			case "ButtonHome":
				if (_currentPage.transform.name != "Home(Clone)") {
					_currentPage = OpenPage (PageManager.pageResourcesPath + "Home");
					changeColor (0);
				}
				break;


			case "Mission":
				if (transform.parent.childCount < 2) {
					PageManager.OpenPopup (PageManager.pageResourcesPath + "Mission", 0.3f);
				}
				break;

			case "InboxImage":
				if (transform.parent.childCount < 2) {
					PageManager.OpenPopup (PageManager.pageResourcesPath + "InboxImage", 0.3f);
				}
				break;

			case "Settings":
				if (transform.parent.childCount < 2) {
					PageManager.OpenPopup (PageManager.pageResourcesPath + "Settings", 0.3f);
				}
				break;
		}

	}

	void changeColor(int i){
		foreach (Transform abc in ButtonFromImage) {
			abc.GetComponent<Image> ().color = new Color(1, 1, 1, 0);
		}
		ButtonFromImage [i].GetComponent<Image> ().color = new Color(0, 0, 0, 0.392f);			
	}
		
}
