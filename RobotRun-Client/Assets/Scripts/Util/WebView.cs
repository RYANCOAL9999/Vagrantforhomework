using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

[AddComponentMenu("Koocell/UI/WebView")]
public class WebView : MonoBehaviour {
	public Action<WebView> onLoadCompleteCallback;
	public Action<string> callback;
	Canvas _canvas = null;
	WebViewObject _webViewObject;
	string _url;
	bool _isPost;
	Dictionary<string, string> _dictParams;
	
	public string url{
		get{
			return _url;
		}
	}
	
	public bool visibility{
		get{
			return _webViewObject.GetVisibility();
		}
		set{
			_webViewObject.SetVisibility(value);
			//			UpdateSize();
		}
	}
	void Awake(){
		GameObject goCanvas = GameObject.Find("Canvas");
		if(goCanvas != null){
			_canvas = goCanvas.GetComponent<Canvas>();
		}
		_webViewObject = this.gameObject.AddComponent<WebViewObject>();//(new GameObject("WebViewObject")).AddComponent<WebViewObject>();
		_webViewObject.Init(OnWebViewObjectCallback);
	}
	
	void Start () {
		//		_webViewObject.SetVisibility(true);
		//		this.url = "http://www.google.com";
		//		this.visibility = true;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		UpdateSize();
	}
	
	public void LoadURL(string url){
		_url = url;
		_isPost = false;
		UpdateContent();
	}
	
	public void LoadPostURL(string url, Dictionary<string, string> dictParams = null){
		_url = url;
		_dictParams = dictParams;
		_isPost = true;
		UpdateContent();
	}
	
	void UpdateContent(){
		if(_webViewObject == null){
			Debug.LogError("Error Occur!!! Do not set URL to Web View before Start()");
			return;
		}
		if(_isPost){
			string strPostData = "";
			if(_dictParams != null){
				foreach(string key in _dictParams.Keys){
					if(strPostData != ""){
						strPostData += "&";
					}
					strPostData += key + "="+_dictParams[key];
				}
			}
			_webViewObject.LoadPostURL(_url, strPostData);
		}else{
			_webViewObject.LoadURL(_url);
		}
		switch (Application.platform) {
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.OSXPlayer:
		case RuntimePlatform.IPhonePlayer:
			_webViewObject.EvaluateJS(
				"window.addEventListener('load', function() {" +
				"	window.Unity = {" +
				"		call:function(msg) {" +
				"			var iframe = document.createElement('IFRAME');" +
				"			iframe.setAttribute('src', 'unity:' + msg);" +
				"			document.documentElement.appendChild(iframe);" +
				"			iframe.parentNode.removeChild(iframe);" +
				"			iframe = null;" +
				"		}" +
				"	}" +
				"}, false);");
			_webViewObject.EvaluateJS(
				"window.addEventListener('load', function() {" +
				"	window.addEventListener('click', function() {" +
				"		Unity.call('clicked');" +
				"	}, false);" +
				"}, false);");
			break;
		case RuntimePlatform.OSXWebPlayer:
		case RuntimePlatform.WindowsWebPlayer:
			_webViewObject.EvaluateJS(
				"parent.$(function() {" +
				"	window.Unity = {" +
				"		call:function(msg) {" +
				"			parent.unityWebView.sendMessage('WebViewObject', msg)" +
				"		}" +
				"	};" +
				"	parent.$(window).click(function() {" +
				"		window.Unity.call('clicked');" +
				"	});" +
				"});");
			break;
		case RuntimePlatform.Android:
			break;
		}
	}
	
	void UpdateSize(){
		Vector3[] corners  = new Vector3[4];
		(this.transform as RectTransform).GetWorldCorners(corners);
		
		Camera c = null;
		if(_canvas != null){
			c = _canvas.worldCamera;
		}
		for( int i = 0; i < 4; i++ )
		{
			corners[i] = RectTransformUtility.WorldToScreenPoint( c, corners[i] );
		}
		Vector3 bottomLeft = corners[0];
		Vector3 topRight = corners[2];
		_webViewObject.SetMargins(
			Mathf.RoundToInt(bottomLeft.x),
			Mathf.RoundToInt(Screen.height - topRight.y),
			Mathf.RoundToInt(Screen.width - topRight.x),
			Mathf.RoundToInt(bottomLeft.y)
			);
	}
	
	void OnWebViewObjectCallback(string msg){
		Debug.Log(string.Format("CallFromJS[{0}]", msg));
		if(msg == "LoadComplete"){
			this.visibility = true;
			if(onLoadCompleteCallback != null)
				onLoadCompleteCallback(this);
		}else if(callback != null){
			callback(msg);
		}
	}
}
