using UnityEngine;
using System.Collections;

public class SK023 : DropThrowItem {
	public override int skillDataID{
		get{
			return 23;
		}	
	}

	protected override void OnTriggerEnter2D(Collider2D collider){
		Debug.Log("OnTriggerEnter2D: "+collider.name);
		MatchCharacterController hitMatchCharacterController = collider.GetComponentInParent<MatchCharacterController>();
		if(hitMatchCharacterController != null){
			if(fromCharacterController != hitMatchCharacterController){
				if(fromCharacterController != hitMatchCharacterController){
					hitMatchCharacterController.OnHitByItem(this);
				}
				this.photonView.RPC ("OnCallDestroy", PhotonTargets.AllBufferedViaServer);
			}

		}else{
			_rigidBody2D.isKinematic = true;
			_rigidBody2D.velocity = Vector2.zero;
		}
	}
}
