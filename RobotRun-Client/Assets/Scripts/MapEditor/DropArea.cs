using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
public class DropArea : MapObjectBase {
	public Vector3 rebornPosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos(){
		BoxCollider2D collider = GetComponent<BoxCollider2D>();
		Gizmos.color = Color.red;
		Gizmos.DrawCube(this.transform.TransformPoint(collider.offset), collider.size);
	}
}
