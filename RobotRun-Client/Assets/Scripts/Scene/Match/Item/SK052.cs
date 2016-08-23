using UnityEngine;
using System.Collections;

public class SK052 : MonoBehaviour {
	public GameObject activeEffect;
	public GameObject endEffect;
	// Use this for initialization
	void Start () {
		activeEffect.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void End(){
		activeEffect.SetActive(false);
		endEffect.SetActive(true);
		Invoke("DestroyMe", 2.0f);
	}

	void DestroyMe(){
		Destroy(this.gameObject);
	}
}
