using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearTooth : PlatformController {
	Vector3 _lastPosition;
	public Vector3 velocity;
	public int rotateDirection = -1;

	public override void Start () {
		base.Start();
		_collider = GetComponent<BoxCollider2D>();
		CalulateRaySpacing(true);
		_lastPosition = transform.position;
	}

	public override void Update () {
//		base.Update();
		UpdateRaycastOrigins(true);

		Vector3 velocity = (transform.position - _lastPosition);
//		Vector3 velocity = this.velocity;

		CalculatePassengersMovement(velocity);
		MovePassengers(true);
//		transform.Translate(velocity);
		MovePassengers(false);
		_lastPosition = transform.position;

		Debug.DrawLine(_raycastOrigins.bottomLeft, _raycastOrigins.bottomRight, Color.blue);
		Debug.DrawLine(_raycastOrigins.bottomLeft, _raycastOrigins.topLeft, Color.blue);
		Debug.DrawLine(_raycastOrigins.topLeft, _raycastOrigins.topRight, Color.blue);
		Debug.DrawLine(_raycastOrigins.bottomRight, _raycastOrigins.topRight, Color.blue);

		Debug.Log(transform.rotation.eulerAngles.z);
	}

	protected override void CalculatePassengersMovement(Vector3 velocity){
		base.CalculatePassengersMovement(velocity);
		HashSet<Transform> movedPassengers = new HashSet<Transform>();
		float directionX = rotateDirection;
		float rayLength = velocity.magnitude + skinWidth;
		float rotateRad = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
		float cosValue = Mathf.Cos(rotateRad);
		float sinValue = Mathf.Sin(rotateRad);
		for(int i = 0; i < horizontalRayCount; i++){
			Vector2 rayOrigin = (directionX == -1)?_raycastOrigins.bottomLeft:_raycastOrigins.bottomRight;
			rayOrigin += new Vector2(-sinValue, cosValue) * (_horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, new Vector2(cosValue, sinValue), rayLength, passengerMask);
			Debug.DrawRay(rayOrigin, new Vector2(cosValue, sinValue) * directionX * rayLength, Color.red);

			if(hit){
				if(!movedPassengers.Contains(hit.transform)){
					movedPassengers.Add(hit.transform);
					float pushX = velocity.x - (hit.distance - skinWidth) * rotateDirection;
					float pushY = -skinWidth;

					passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), false, true));
				}
			}
		}
	}
}
