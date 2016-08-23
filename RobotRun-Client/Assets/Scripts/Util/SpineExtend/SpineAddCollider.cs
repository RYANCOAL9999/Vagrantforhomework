using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;

[RequireComponent (typeof(SkeletonRenderer))]
public class SpineAddCollider : MonoBehaviour {
	[SpineSlot]
	public string boundingBoxName = "bodyarea";
	protected BoundingBoxFollower _boundingBoxFollower;
	protected virtual void Awake(){
		GameObject colliderGO = new GameObject();
		colliderGO.name = boundingBoxName;
		colliderGO.transform.SetParent(this.transform, false);
		colliderGO.layer = this.gameObject.layer;

		_boundingBoxFollower = colliderGO.AddComponent<BoundingBoxFollower>();
		_boundingBoxFollower.slotName = boundingBoxName;
		_boundingBoxFollower.HandleReset(this.GetComponent<SkeletonRenderer>());

		BoneFollower boneFollower = colliderGO.AddComponent<BoneFollower>();
		boneFollower.skeletonRenderer = this.GetComponent<SkeletonAnimation>();
		boneFollower.boneName = _boundingBoxFollower.Slot.Data.BoneData.Name;
	}

	protected virtual void Start () {
		UpdateCollider();
	}

	protected virtual void OnEnable () {
   		UpdateCollider();
	}

	public void UpdateCollider(){

		foreach(Spine.BoundingBoxAttachment att in _boundingBoxFollower.colliderTable.Keys){
			PolygonCollider2D polyCollider = _boundingBoxFollower.colliderTable[att];
			if(polyCollider.name == boundingBoxName){
				polyCollider.isTrigger = false;
				polyCollider.enabled = true;
			}
		}
	}
}
