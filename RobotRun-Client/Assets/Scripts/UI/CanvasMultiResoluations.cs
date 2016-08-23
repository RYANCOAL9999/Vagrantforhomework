﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasMultiResoluations : MonoBehaviour {

	void Awake(){
		CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
		
		float screenWidthScale = Screen.width / canvasScaler.referenceResolution.x;
		float screenHeightScale = Screen.height / canvasScaler.referenceResolution.y;
		
		canvasScaler.matchWidthOrHeight = screenWidthScale > screenHeightScale ? 1 : 0;

	}

	void Update () {
		

	}

	public void backbutton(){
		Application.LoadLevel("Main");
	}
		
}
