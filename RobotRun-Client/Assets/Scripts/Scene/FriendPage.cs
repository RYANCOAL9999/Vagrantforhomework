using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using KoocellUnityPlugin.UI_Page;

public class FriendPage : SubPage {


	protected override void Awake () {
		base.Awake();
	}

	protected override void Start () {
		base.Start();
	}

	protected override void Update () {
		base.Update();
	}


	public override void PageBtnClick(Button button) {
		base.PageBtnClick(button);
		switch (button.name) {
			case "ConfirmButton":
				break;
			
			case "InviteButton":
				break;

			case "GETREWARDSButton":
				break;

		}
	}

}
