﻿using UnityEngine;
using System.Collections;

public class HorizontalThrowItem : ThrowItem {

	protected override void Awake(){
		base.Awake();
	}

	protected override void Start () {
		base.Start();
		_rigidBody2D.gravityScale = 0;
		_rigidBody2D.AddForce(new Vector2(10,0), ForceMode2D.Impulse);
	}

	protected override void Update () {
		base.Update();
	}

	protected override void OnTriggerEnter2D(Collider2D collider){
		Debug.Log("OnTriggerEnter2D: "+collider.name);
		MatchCharacterController hitMatchCharacterController = collider.GetComponentInParent<MatchCharacterController>();
		if(hitMatchCharacterController != null){
			if(fromCharacterController != hitMatchCharacterController){
				PlayhitAnimation();
				this.photonView.RPC ("OnPlayHitAnimation", PhotonTargets.Others);
				hitMatchCharacterController.OnHitByItem(this);
			}
		}else{
			PlayhitAnimation();
			this.photonView.RPC ("OnPlayHitAnimation", PhotonTargets.Others);
		}
	}

	void PlayhitAnimation(){
		_animator.SetTrigger("hit");
		_rigidBody2D.isKinematic = true;
		_rigidBody2D.velocity = Vector2.zero;
	}

	public void OnHitAnimationStart(){
		_rigidBody2D.isKinematic = true;
		_rigidBody2D.velocity = Vector2.zero;
	}

	public void OnHitAnimationFinish(){
		this.photonView.RPC ("OnCallDestroy", PhotonTargets.AllBufferedViaServer);
	}

	[PunRPC]
	protected void OnPlayHitAnimation(PhotonMessageInfo msgInfo){
		PlayhitAnimation();
	}

}