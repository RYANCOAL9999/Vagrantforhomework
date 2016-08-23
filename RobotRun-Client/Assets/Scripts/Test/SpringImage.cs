using UnityEngine;
using System.Collections;

public class SpringImage : MonoBehaviour {
	public Transform upperPlatform;
	public Transform lowerPlatform;
	SpriteRenderer _spriteRenderer;
	float _initDistance;
	// Use this for initialization
	void Start () {
		_initDistance = upperPlatform.position.y - lowerPlatform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(1, (upperPlatform.position.y - lowerPlatform.position.y)/_initDistance, 0);
	}
}
