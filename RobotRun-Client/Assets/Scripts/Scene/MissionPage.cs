using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using KoocellUnityPlugin.UI_Page;

public class MissionPage : PopupPage {

	public Transform missionScrollRect;
	public Transform achievenmentScrollRect;


	protected override void Awake () {
		base.Awake();
		missionScrollRect.gameObject.SetActive (true);
	}

	protected override void Start () {
		base.Start();
	}

	protected override void Update () {
		base.Update();
	}

	public override void FadeIn (float duration)
	{
		base.FadeIn (duration);
	}

	protected override void FadeInAniUpdate (PopupPage popup)
	{
		//base.FadeInAniUpdate (popup);
		_currFadeTime += Time.deltaTime;

		float deltaT = _currFadeTime / _fadeDuration;

		if(dimBG != null){
			Color dimColor = dimBG.color;
			dimColor.a = Mathf.Clamp01(deltaT);
			dimBG.color = dimColor;
		}

		rectContent.localScale = Vector3.one * Mathf.Clamp01(deltaT);

		if(deltaT >= 1f){
			onPopupUpdate -= FadeInAniUpdate;
			_popupPageState = PageState.Active;
			missionScrollRect.GetComponent<ScrollRect> ().enabled = true;
			achievenmentScrollRect.GetComponent<ScrollRect> ().enabled = true;
		}
	}

	public override void ButtonClick(Button button) {
		base.ButtonClick(button);
		switch (button.name) {

			case "BackButton":
				Destroy(this.gameObject);
				break;

			case "DailyMissionButton":
				missionScrollRect.gameObject.SetActive (true);
				achievenmentScrollRect.gameObject.SetActive (false);
				break;

			case "AchievementButton":
				missionScrollRect.gameObject.SetActive (false);
				achievenmentScrollRect.gameObject.SetActive (true);
				break;

			case "ClaimRewardButton":
				break;
			
			case "SKIPButton":
				break;

			case "ClaimedButton":
				break;
			
			case "ClaimedALLButton":
				break;

		}

	}
}
