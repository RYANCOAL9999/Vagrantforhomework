using UnityEngine;
using System.Collections;

public class ScrollBackground : MonoBehaviour {
	public float scrollRatioX = 1.0f;
	public float scrollRatioY = 1.0f;
	public Vector2 offset;
	public Camera targetCamera;

	Vector3 _startPos;
	Vector3 _cameraStartPos;
	// Use this for initialization
	void Start () {
		if(targetCamera == null){
			targetCamera = Camera.main;
		}
		_startPos  = this.transform.position;
		if(targetCamera != null){
			_cameraStartPos = targetCamera.transform.position;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(targetCamera != null){
			Vector3 newPos = this.transform.position;
			newPos.x = _startPos.x + (targetCamera.transform.position.x) * (1 - scrollRatioX);
			newPos.y = _startPos.y + (targetCamera.transform.position.y) * (1 - scrollRatioY);
			this.transform.position = newPos;
		}
	}

	public void SetCameraStartPos(Vector3 pos){
		_cameraStartPos = pos;
	}
}
