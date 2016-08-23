using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AcceleratePlatform : MonoBehaviour {
	public MatchCharacterController.SpeedMultipler.ApplyType applyType = MatchCharacterController.SpeedMultipler.ApplyType.Add;
	public float speedMultipler = 0;
	public float multiplerDurationSec = 0;

//	public override void Start () {
//		base.Start();
//	}
//
//	public override void Update () {
//		base.Update();
//	}

//	protected override void CalculatePassengersMovement(Vector3 velocity){
//		base.CalculatePassengersMovement(velocity);
//		HashSet<Transform> movedPassengers = new HashSet<Transform>();
//		float rayLength = skinWidth * 2;
//		for(int i = 0; i < verticalRayCount; i++){
//			Vector2 rayOrigin = _raycastOrigins.topLeft + Vector2.right * (_verticalRaySpacing * i);
//			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);
//			if(hit){
//				if(!movedPassengers.Contains(hit.transform)){
//					movedPassengers.Add(hit.transform);
//					passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(accelerateSpeed, 0), true, false));
//				}
//			}
//		}
//	}
}
