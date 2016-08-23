using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JumpPlatform : PlatformController {

	public struct PassengerExternalVelocity{
		public Transform transform;
		public Vector3 velocity;

		public PassengerExternalVelocity(Transform transform, Vector3 velocity){
			this.transform = transform;
			this.velocity = velocity;
		}
	}

	public float compressSpeed = 0.1f;
	public float compressDistance = 1;
	public float reboundSpeed = 20;
	public Vector3 positionOld{
		get{
			return _positionOld;
		}
	}

	protected List<PassengerExternalVelocity> passengerExternalVelocity;
	protected Dictionary<Transform, Player> passengerPlayerDictionary = new Dictionary<Transform, Player>();

	Vector3 _positionOld;
	Vector3 _positionTarget;
	bool _rebound;

	public override void Start () {
		base.Start();
		_positionOld = transform.position;
		_positionTarget = new Vector3(_positionOld.x, _positionOld.y - compressDistance);
	}

	public override void Update () {
		base.Update();
	}

	protected virtual void OnDrawGizmos(){
		if(localWayPoints != null){
			Gizmos.color = Color.red;
			float size = 0.3f;
			Vector3 globalCompressedPos = (Application.isPlaying)?new Vector3(_positionTarget.x, _positionTarget.y):new Vector3(transform.position.x, transform.position.y - compressDistance);
			Gizmos.DrawLine(globalCompressedPos - Vector3.up * size, globalCompressedPos + Vector3.up * size);
			Gizmos.DrawLine(globalCompressedPos - Vector3.left * size, globalCompressedPos + Vector3.left * size);
		}
	}

	protected override void MovePassengers(bool beforeMovePlatform){
		base.MovePassengers(beforeMovePlatform);
		if(!beforeMovePlatform){
			foreach(PassengerExternalVelocity passenger in passengerExternalVelocity){
				if(!passengerPlayerDictionary.ContainsKey(passenger.transform)){
					passengerPlayerDictionary.Add(passenger.transform, passenger.transform.GetComponent<Player>());
				}
				passengerPlayerDictionary[passenger.transform].ApplyExternalVelocity(passenger.velocity);
			}
		}
	}

	protected override void CalculatePassengersMovement(Vector3 velocity){
		base.CalculatePassengersMovement(velocity);
		HashSet<Transform> movedPassengers = new HashSet<Transform>();
		passengerExternalVelocity = new List<PassengerExternalVelocity>();
		if(_rebound){
			_rebound = false;
			float rayLength = skinWidth * 2;
			for(int i = 0; i < verticalRayCount; i++){
				Vector2 rayOrigin = _raycastOrigins.topLeft + Vector2.right * (_verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);
				if(hit){
					if(!movedPassengers.Contains(hit.transform)){
						movedPassengers.Add(hit.transform);
						float compressDistance = -compressSpeed * Time.deltaTime;
						if(transform.position.y + compressDistance <= _positionTarget.y){
							compressDistance = _positionTarget.y - transform.position.y;
						}
						passengerExternalVelocity.Add(new PassengerExternalVelocity(hit.transform, new Vector3(0, reboundSpeed)));
					}
				}
			}
		}
	}

	protected override Vector3 CalculatePlatformMovement(){
		base.CalculatePlatformMovement();
		float rayLength = skinWidth * 2;

		if(!_rebound){
			for(int i = 0; i < verticalRayCount; i++){
				Vector2 rayOrigin = _raycastOrigins.topLeft + Vector2.right * (_verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);
				if(hit){
					float compressDistance = -compressSpeed * Time.deltaTime;
					if(transform.position.y + compressDistance <= _positionTarget.y){
						_rebound = true;
					}else{
						return new Vector3(0, compressDistance);
					}
				}
			}
		}
		//no passenger
		float expandDistance = reboundSpeed * Time.deltaTime;
		if(transform.position.y + expandDistance >= _positionOld.y){
			expandDistance = _positionOld.y - transform.position.y;
		}
		return new Vector3(0, expandDistance);
	}
}
