using UnityEngine;
using System.Collections;
using System;

public class Collider2DEvent : MonoBehaviour {
	public Action<Collision2D> onCollisionEnter2D = null;
	public Action<Collision2D> onCollisionStay2D = null;
	public Action<Collision2D> onCollisionExit2D = null;
	public Action<Collider2D> onTriggerEnter2D = null;
	public Action<Collider2D> onTriggerStay2D = null;
	public Action<Collider2D> onTriggerExit2D = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision){
		Debug.Log("collision Enter: "+collision.gameObject.name);
		if(onCollisionEnter2D != null)
			onCollisionEnter2D(collision);
	}

	void OnCollisionStay2D(Collision2D collision) {
		Debug.Log("collision Stay: "+collision.gameObject.name);
		if(onCollisionStay2D != null)
			onCollisionStay2D(collision);
	}

	void OnCollisionExit2D(Collision2D collision) {
		Debug.Log("collision Exit: "+collision.gameObject.name);
		if(onCollisionExit2D != null)
			onCollisionExit2D(collision);
	}

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log("Trigger Enter: "+collider.gameObject.name);
		if(onTriggerEnter2D != null)
			onTriggerEnter2D(collider);
	}

	void OnTriggerStay2D(Collider2D collider) {
		Debug.Log("Trigger Stay: "+collider.gameObject.name);
		if(onTriggerStay2D != null)
			onTriggerStay2D(collider);
	}

	void OnTriggerExit2D(Collider2D collider) {
		Debug.Log("Trigger Exit: "+collider.gameObject.name);
		if(onTriggerExit2D != null)
			onTriggerExit2D(collider);
	}
}
