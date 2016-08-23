using UnityEngine;
using System.Collections;

public class SpineAddColliderTrigger : SpineAddCollider {

	public bool triggerIsActive{
		get{
			return _boundingBoxFollower.gameObject.activeSelf;
		}
	}

	protected virtual void Awake(){
		base.Awake();
		_boundingBoxFollower.gameObject.name = "ColliderTrigger";
		_boundingBoxFollower.gameObject.layer = LayerMask.NameToLayer("PlayerTrigger");
	}

	protected override void OnEnable () {
		
	}

	public void EnableTrigger(){
		_boundingBoxFollower.gameObject.SetActive(true);
	}

	public void DisableTrigger(){
		_boundingBoxFollower.gameObject.SetActive(false);
	}
}
