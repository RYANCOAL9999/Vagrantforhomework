using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using SimpleJSON;

public class LoginScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RobotRunRequest.Instance.CallAPI(RobotRunURL.API_USER_LOGIN, null, OnRequestLoginSuccess);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnRequestLoginSuccess(JSONNode jsonNode){
		SceneManager.LoadScene("Main");
	}
}
