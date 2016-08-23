using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public GameObject[] objectsOnTop;
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

	void Start () {
		globalWayPoints = new Vector3[localWayPoints.Length];
		for(int i = 0; i < localWayPoints.Length; i++){
			globalWayPoints[i] = localWayPoints[i] + transform.position;
		}
	}

	void Update () {

		Vector3 velocity = CalculatePlatformMovement();
		transform.Translate(velocity);
		foreach(GameObject go in objectsOnTop){
			go.transform.Translate(velocity);
		}
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
}
