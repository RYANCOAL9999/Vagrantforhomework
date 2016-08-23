#if UNITY_ANDROID
using UnityEngine;
using System.Collections;

public class AndroidPlugin
{
	private static AndroidJavaObject ggoPlugin = null;
	private static AndroidJavaObject activityContext = null;
	
	public static void CopyMessgeToClipBord(string messge){
		using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			AndroidJavaObject activityContext = activityClass.GetStatic<AndroidJavaObject> ("currentActivity");
					activityContext.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
						activityContext.Call ("copyMessage", messge);
					}));
		}
	}
}
#endif
