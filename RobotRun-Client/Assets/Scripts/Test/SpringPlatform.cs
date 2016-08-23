using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpringPlatform : MonoBehaviour {
	public LayerMask collisionMask;
	public float reboundJumpHeight = 1;
	public Transform upperPlatform;
	public Transform bottomPlatform;
	public Transform spring;

	Animator _animator;
//	float _jumpForce;
//	Collision2D _lastCollision = null;
	List<GameObject> _listCollidedGameObject = new List<GameObject>();

	void Awake(){
		_animator = transform.GetComponent<Animator>();

		Collider2DEvent upperCollider2DEvent = upperPlatform.GetComponent<Collider2DEvent>();
		upperCollider2DEvent.onTriggerEnter2D = OnTriggerEnter2D;
		upperCollider2DEvent.onTriggerExit2D = OnTriggerExit2D;
	}

	void Start () {
		
	}


	void Update () {
		
	}

	public void OnAnimationEvent(string strParam){
		if(strParam == "AddForce"){
			foreach(GameObject go in _listCollidedGameObject){
				Rigidbody2D rigidBody2D = go.GetComponentInParent<Rigidbody2D>();
				if(rigidBody2D != null){
					float g = rigidBody2D.gravityScale * Physics2D.gravity.magnitude;
					float jumpForce = (Mathf.Sqrt(2 * g * reboundJumpHeight) + 0.1f) * rigidBody2D.mass;
					rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
				}
			}
			_listCollidedGameObject.Clear();
		}
	}

	public void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject != null && IsInLayerMask(collider.gameObject, collisionMask) && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Rebound")){
			_animator.SetTrigger("Rebound");
			_listCollidedGameObject.Add(collider.gameObject);
		}
//		_lastCollision = collision;
	}

	public void OnCollisionStay2D(Collision2D collider){
//		_lastCollision = collision;
	}

	public void OnTriggerExit2D(Collider2D collider){
//		if(collider.gameObject != null && IsInLayerMask(collider.gameObject, collisionMask)){
//			Rigidbody2D rigidBody2D = collider.gameObject.GetComponent<Rigidbody2D>();
//			if(rigidBody2D != null){
//
//				float g = rigidBody2D.gravityScale * Physics2D.gravity.magnitude;
//				float jumpForce = (Mathf.Sqrt(2 * g * reboundumpHeight) + 0.1f) * rigidBody2D.mass;
//				rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
//			}
//		}
	}

	bool IsInLayerMask(GameObject obj, LayerMask layerMask)
	{
		// Convert the object's layer to a bitfield for comparison
		int objLayerMask = (1 << obj.layer);
		if ((layerMask.value & objLayerMask) > 0)  // Extra round brackets required!
			return true;
		else
			return false;
	}

//	bool IpUpperContact(Collider2D collision){
//		foreach(ContactPoint2D pt in collision){
//			if(pt.normal == -Vector2.up){
//				return true;
//			}
//		}
//		return false;
//	}

	bool IpUpperContact(Collision2D collision){
		foreach(ContactPoint2D pt in collision.contacts){
			if(pt.normal == -Vector2.up){
				return true;
			}
		}
		return false;
	}
}
