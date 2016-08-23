using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Gear : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.DORotate(new Vector3(0,0,360),10.0f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
