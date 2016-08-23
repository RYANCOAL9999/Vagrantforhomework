using UnityEngine;
using System.Collections;
using System;

public class AnimationCallback : MonoBehaviour {
	public Action<string> eventCallback = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnAnimationEvent(string param){
		if(eventCallback != null){
			eventCallback(param);
		}
	}
}
