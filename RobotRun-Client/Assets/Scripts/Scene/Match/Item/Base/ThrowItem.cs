using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class ThrowItem : Photon.PunBehaviour {
	public MatchCharacterController fromCharacterController = null;

	public virtual int skillDataID{
		get{
			return 0;
		}	
	}
	protected Rigidbody2D _rigidBody2D;
	protected Animator _animator;

	public SkillData skillData{
		get{
			return GameDataManager.Instance.GetSkillData(skillDataID);
		}
	}

	protected virtual void Awake () {
		_rigidBody2D = this.GetComponent<Rigidbody2D>();
		_animator = this.GetComponent<Animator>();
		this.photonView.ObservedComponents.Add(this);
	}

	protected virtual void Start () {
		object[] data = this.photonView.instantiationData;
		fromCharacterController = GameDataManager.Instance.dictOwnerIDMatchCharacterController[int.Parse(data[0].ToString())];
		this.transform.localScale = JsonUtility.FromJson<Vector3>(data[1].ToString());
		_rigidBody2D.velocity = new Vector3(JsonUtility.FromJson<Vector3>(data[2].ToString()).x, 0);
	}

	protected virtual void Update () {
	
	}

	protected virtual void OnTriggerEnter2D(Collider2D collider){
		Debug.Log("OnTriggerEnter2D: "+collider.name);
		MatchCharacterController hitMatchCharacterController = collider.GetComponentInParent<MatchCharacterController>();
		if(hitMatchCharacterController != null){
			if(fromCharacterController != hitMatchCharacterController){
				hitMatchCharacterController.OnHitByItem(this);
				this.photonView.RPC ("OnCallDestroy", PhotonTargets.AllBufferedViaServer);
			}

		}else{
			this.photonView.RPC ("OnCallDestroy", PhotonTargets.AllBufferedViaServer);
		}
	}

	[PunRPC]
	protected void OnCallDestroy (PhotonMessageInfo msgInfo)
	{
		Destroy(this.gameObject);
	}
}
