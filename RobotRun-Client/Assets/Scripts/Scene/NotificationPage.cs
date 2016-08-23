using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using KoocellUnityPlugin.UI_Page;


public class NotificationPage : PopupPage {

	protected override void Awake () {
		base.Awake();
	}

	protected override void Start () {
		base.Start();
	}

	protected override void Update () {
		base.Update();
		//PBDestroy(2);

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
		}
	}

	public override void ButtonClick(Button button) {
		base.ButtonClick (button);
		switch (button.name) {

		case "BackButton":
			PBDestroy(0);
			break;

		}
	}

	public void PBDestroy(float time){
		Destroy (this.gameObject,time);
	}
}
