using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour {
	public JumpPlatform jumpPlatform = null;
	Vector3 scaleOld;
	// Use this for initialization
	void Start () {
		scaleOld = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if(jumpPlatform != null){
			float compressPercentage = (jumpPlatform.positionOld.y - jumpPlatform.transform.position.y) / jumpPlatform.compressDistance ;
			this.transform.localScale = new Vector3(scaleOld.x, scaleOld.y * (1 - compressPercentage), scaleOld.z);
		}
	}
}
