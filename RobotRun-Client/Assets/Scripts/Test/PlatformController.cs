using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformController : RaycastController {
	public struct PassengerMovement{
		public Transform transform;
		public Vector3 velocity;
		public bool standingOnPlatform;
		public bool moveBeforePlatform;

		public PassengerMovement(Transform transform, Vector3 velocity, bool standingOnPlatform, bool moveBeforePlatform){
			this.transform = transform;
			this.velocity = velocity;
			this.standingOnPlatform = standingOnPlatform;
			this.moveBeforePlatform = moveBeforePlatform;
		}
	}

	public LayerMask passengerMask;

	public Vector3[] localWayPoints;
	Vector3[] globalWayPoints;

	public float speed;
	public bool cyclic;
	public float waitTime;
	[Range(0,2)]
	public float easeAmount;
	int fromWayPointIndex;
	float percentBetweenTwoWayPoints;
	float nextMoveTime;

	protected List<PassengerMovement> passengerMovement;
	protected Dictionary<Transform, PlayerController2D> passengerDictionary = new Dictionary<Transform, PlayerController2D>();

	public override void Start () {
		base.Start();
		globalWayPoints = new Vector3[localWayPoints.Length];
		for(int i = 0; i < localWayPoints.Length; i++){
			globalWayPoints[i] = localWayPoints[i] + transform.position;
		}
	}

	public override void Update () {
		base.Update();
		UpdateRaycastOrigins();

		Vector3 velocity = CalculatePlatformMovement();

		CalculatePassengersMovement(velocity);
		MovePassengers(true);
		transform.Translate(velocity);
		MovePassengers(false);
	}

	protected virtual void OnDrawGizmos(){
		if(localWayPoints != null){
			Gizmos.color = Color.red;
			float size = 0.3f;

			for (int i = 0; i < localWayPoints.Length; i++){
				Vector3 globalWayPointPos = (Application.isPlaying)?globalWayPoints[i]:localWayPoints[i] + transform.position;
				Gizmos.DrawLine(globalWayPointPos - Vector3.up * size, globalWayPointPos + Vector3.up * size);
				Gizmos.DrawLine(globalWayPointPos - Vector3.left * size, globalWayPointPos + Vector3.left * size);
			}
		}
	}

	float Ease(float x){
		float a = easeAmount + 1;
		return Mathf.Pow(x,a)/(Mathf.Pow(x,a) + Mathf.Pow(1-x,a));
	}

	protected virtual Vector3 CalculatePlatformMovement(){

		if(globalWayPoints.Length == 0 || Time.time < nextMoveTime){
			return Vector3.zero;
		}

		fromWayPointIndex %= globalWayPoints.Length;
		int toWayPointindex = (fromWayPointIndex + 1) % globalWayPoints.Length;
		float distanceBetweenWayPoints = Vector3.Distance(globalWayPoints[fromWayPointIndex], globalWayPoints[toWayPointindex]);
		percentBetweenTwoWayPoints += Time.deltaTime * speed/distanceBetweenWayPoints;
		percentBetweenTwoWayPoints = Mathf.Clamp01(percentBetweenTwoWayPoints);
		float easedPercentBetweenWayPoints = Ease(percentBetweenTwoWayPoints);

		Vector3 newPos = Vector3.Lerp(globalWayPoints[fromWayPointIndex], globalWayPoints[toWayPointindex], easedPercentBetweenWayPoints);

		if(percentBetweenTwoWayPoints >= 1){
			percentBetweenTwoWayPoints = 0;
			fromWayPointIndex++;

			if(!cyclic){
				if(fromWayPointIndex >= globalWayPoints.Length - 1){
					fromWayPointIndex = 0;
					System.Array.Reverse(globalWayPoints);
				}
			}
			nextMoveTime = Time.time + waitTime;
		}

		return newPos - transform.position;
	}

	protected virtual void MovePassengers(bool beforeMovePlatform){
		foreach(PassengerMovement passenger in passengerMovement){
			if(!passengerDictionary.ContainsKey(passenger.transform)){
				passengerDictionary.Add(passenger.transform, passenger.transform.GetComponent<PlayerController2D>());
			}
			if(passenger.moveBeforePlatform == beforeMovePlatform){
				passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.standingOnPlatform);
			}
		}
	}

	protected virtual void CalculatePassengersMovement(Vector3 velocity){
		HashSet<Transform> movedPassengers = new HashSet<Transform>();
		passengerMovement = new List<PassengerMovement>();

		float directionX = Mathf.Sign(velocity.x);
		float directionY = Mathf.Sign(velocity.y);

		//Vertically moving platform
		if(velocity.y != 0){
			float rayLength = Mathf.Abs(velocity.y) + skinWidth;
			for(int i = 0; i < verticalRayCount; i++){
				Vector2 rayOrigin = (directionY == -1)?_raycastOrigins.bottomLeft:_raycastOrigins.topLeft;
				rayOrigin += Vector2.right * (_verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

				if(hit){
					if(!movedPassengers.Contains(hit.transform)){
						movedPassengers.Add(hit.transform);
						float pushX = (directionY == 1)?velocity.x:0;
						float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

						passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), directionY == 1, true));
					}
				}
			}
		}

		//Horizontally moving platform
		if(velocity.x != 0){
			float rayLength = Mathf.Abs(velocity.x) + skinWidth;
			for(int i = 0; i < horizontalRayCount; i++){
				Vector2 rayOrigin = (directionX == -1)?_raycastOrigins.bottomLeft:_raycastOrigins.bottomRight;
				rayOrigin += Vector2.up * (_horizontalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

				if(hit){
					if(!movedPassengers.Contains(hit.transform)){
						movedPassengers.Add(hit.transform);
						float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
						float pushY = -skinWidth;

						passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), false, true));
					}
				}
			}
		}

		//Passenger on top of a horizontally or downward moving platform
		if(directionY == -1 || velocity.y == 0 && velocity.x != 0){
			float rayLength = skinWidth * 2;
			for(int i = 0; i < verticalRayCount; i++){
				Vector2 rayOrigin = _raycastOrigins.topLeft + Vector2.right * (_verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

				if(hit){
					if(!movedPassengers.Contains(hit.transform)){
						movedPassengers.Add(hit.transform);
						float pushX = velocity.x;
						float pushY = velocity.y;

						passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), true, false));
					}
				}
			}
		}
	}
}
