using UnityEngine;
using System.Collections;

public class TestPlayer : MonoBehaviour {

	public struct RaycastOrigins{
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}
	public LayerMask collisionMask;
	public float forceX = 1;
	public float forceY = 0;
	public float skinWidth = 0.015f;
	public Rigidbody2D _ridgidBody;
	public BoxCollider2D _collider;
	public Transform myTransform;
	public float rayLength = 1.0f;
	protected RaycastOrigins _raycastOrigins;
	bool _isGround = false;
	// Use this for initialization
	void Start () {
//		_ridgidBody = this.transform.GetComponent<Rigidbody2D>();
//		_collider = this.transform.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.D)){
			_ridgidBody.AddForce(new Vector2(forceX,forceY));
		}else if(Input.GetKey(KeyCode.Space)){
			_ridgidBody.AddForce(new Vector2(0,20));
		}
		if(!_isGround){
//			Debug.Log("Air");
		}
		_ridgidBody.velocity = new Vector2(Mathf.Clamp(_ridgidBody.velocity.x, 0, 5), _ridgidBody.velocity.y);
		UpdateRaycastOrigins();
		UpdateIsGround();
		DetectGroundNormal();
	}

	void UpdateIsGround(){
		_isGround = _collider.IsTouchingLayers(1<<8);
	}

	void DetectGroundNormal(){
//		if(_isGround){
			float rayLength = this.rayLength;//Mathf.Abs(_ridgidBody.velocity.x) + skinWidth;
			RaycastHit2D hit = Physics2D.Raycast(_raycastOrigins.bottomRight, -Vector2.up, rayLength, collisionMask);
			Debug.DrawRay(_raycastOrigins.bottomRight, -Vector2.up * rayLength, Color.red);
			if(hit){
				Debug.DrawRay(hit.point, hit.normal * 2, Color.blue);
			float slopeangle = Vector2.Angle(hit.normal, Vector2.up) * (hit.normal.x > 0?-1:1);
				myTransform.rotation = Quaternion.Lerp(myTransform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, slopeangle), Time.deltaTime * 2.0f);
			}
//		}else{
//
//			myTransform.rotation = Quaternion.Lerp(myTransform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0), Time.deltaTime * 100.0f);
//		}
	}

	void UpdatePivot(){
		
	}

	protected void UpdateRaycastOrigins(bool withRotate = false){
		Bounds bounds = _collider.bounds;
		bounds.Expand (skinWidth * -2);

		_raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		_raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		_raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		_raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}
}
