using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class AIMatchCharacterController : MatchCharacterController {

	class AIRay{
		public Vector2 origin;
		public Vector2 direction;
		public int length;
		public int layerMask;
		public bool shouldhit;

		public AIRay(Vector2 origin, Vector2 direction, int length, int layerMask, bool shouldHit){
			this.origin = origin;
			this.direction = direction;
			this.length = length;
			this.layerMask = layerMask;
			this.shouldhit = shouldHit;
		}
	}

	public int horizontalRayCount = 4;
	protected float _horizontalRaySpacing;

	Vector2 _jumpArrivePos;
	Vector2 _lastJumpPos;
	bool _jumpForItem;
	Coroutine _corountineCallPet = null;

	List<Vector2> _listProjectilePts = new List<Vector2>();
	List<Vector2> _listItemProjectilePts = new List<Vector2>();

	List<AIRay> _listDetectRay = new List<AIRay>();

	protected override void Awake () {
		base.Awake();
		_listDetectRay.Add(new AIRay(_raycastOrigins.bottomRight, Vector2.right, 3, MatchConfig.layerMaskGround | MatchConfig.layerMaskObstacle | MatchConfig.layerMaskHurtable, false));
		_listDetectRay.Add(new AIRay(_raycastOrigins.topRight, Vector2.right, 3, MatchConfig.layerMaskGround | MatchConfig.layerMaskObstacle | MatchConfig.layerMaskHurtable, false));
	}

	protected override void Start () {
		base.Start();
	}

	protected override void Update () {
		base.Update();
		CalculateJumpProjectile();
		UpdateAI();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		foreach(Vector2 pt in _listProjectilePts){
			Gizmos.DrawSphere(pt, 0.1f);
		}
		foreach(Vector2 pt in _listItemProjectilePts){
			Gizmos.DrawSphere(pt, 0.1f);
		}
	}

	protected override void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate");

		_matchCharacter.characterData = new CharacterData(JSON.Parse(this.photonView.instantiationData[0].ToString()));
		#if UNITY_EDITOR
		if(!GameDataManager.Instance.dictPlayerIDSlot.ContainsKey(info.sender.ID)){
			foreach(MatchProgressBarIcon icon in _match.matchProgressBar.listMatchProgressBarIcon)
				if(icon.matchCharacterController == null){
					icon.matchCharacterController = this;
				}

		}else{
		#endif
			int playerIndex = GameDataManager.Instance.dictPlayerIDSlot[info.sender.ID];
			_match.matchProgressBar.listMatchProgressBarIcon[playerIndex].matchCharacterController = this;
			#if UNITY_EDITOR
		}
			#endif
	}



	protected override void CalulateRaySpacing(){
		base.CalulateRaySpacing();
		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
		float minY = float.MaxValue;
		float maxY = float.MinValue;
		foreach(Vector2 v in _collier.points){
			if(v.y < minY){
				minY = v.y;
			}
//			if(v.x > maxY){
//				maxY = v.y;
//			}
		}
		maxY = minY + this.matchCharacter.characterData.matchJump;
//		minY = minY - this.matchCharacter.characterData.matchJump;
		_horizontalRaySpacing = (maxY - minY) / (horizontalRayCount - 1);
	}	

	public override void Jump(){
		base.Jump();
		_lastJumpPos = this.transform.position;
//		if( _rigidBody2D.velocity.y != 0){
////			_listProjectilePts.Clear();
//			float g = _rigidBody2D.gravityScale * Physics2D.gravity.magnitude;
//			float t = _rigidBody2D.velocity.y / g * 2;
//			int section = 20;
//			for(int i = 0; i < section * 2; i++){
//				float dt = i * t / section;
//				float y = this.transform.position.y + _rigidBody2D.velocity.y * dt + (-g * dt * dt)/2;
//				float x = this.transform.position.x + _rigidBody2D.velocity.x * dt;
////				_listProjectilePts.Add(new Vector2(x, y));
//			}
//		}
	}

	protected override void DestroyMapItem(GameObject gameObject){
		//no need to destroy
	}

	protected override void HandleInput(){
		//ignore input
	}

	void UpdateAI(){
		if(_hurtID > 0)return;
		if(_isGround){
			CharacterData characterData = _matchCharacter.characterData;
			RaycastHit2D hitTop = Physics2D.Raycast(_raycastOrigins.topRight, Vector2.right, 3, MatchConfig.layerMaskGround | MatchConfig.layerMaskObstacle | MatchConfig.layerMaskHurtable);
			RaycastHit2D hitBottom = Physics2D.Raycast(_raycastOrigins.bottomRight + (Vector2.up * .1f), Vector2.right, 3, MatchConfig.layerMaskGround | MatchConfig.layerMaskObstacle | MatchConfig.layerMaskHurtable);
			Debug.DrawRay(_raycastOrigins.topRight, Vector2.right * 3, Color.white);
			Debug.DrawRay(_raycastOrigins.bottomRight + (Vector2.up * .1f), Vector2.right * 3, Color.white);
			if(hitTop || hitBottom){
				if(_realVelocity.x > 0){
					Collider2D collider = hitBottom.collider == null ? hitTop.collider : hitBottom.collider;
					AcceleratePlatform acceleratePlatform = collider.GetComponent<AcceleratePlatform>();
					if(acceleratePlatform != null){
						if(acceleratePlatform.speedMultipler >= 0){
							return;
						}
					}
					float g = _rigidBody2D.gravityScale * Physics2D.gravity.magnitude;
					float minX = collider.bounds.min.x;
					float maxY = collider.bounds.max.y;
					float vy = _jumpForce / _rigidBody2D.mass;
					float ty = Mathf.Abs(maxY - vy) / g;
					float dx = _realVelocity.x * ty;
					if(minX - _raycastOrigins.bottomRight.x < dx && collider.GetComponent<DropArea>() == null){
						Jump();
						Debug.Log("Jump 1");
						return;
					}
				}else{
					Jump();
					Debug.Log("Jump 1.1");
					return;
				}
			}else if(_jumpForItem && _jumpArrivePos.x != float.MinValue){
				Jump();
				Debug.Log("Jump 3.2");
				return;
			}else{
				RaycastHit2D hitUnderGround = Physics2D.Raycast(_raycastOrigins.bottomRight + (Vector2.down * .1f), Vector2.right, 3, MatchConfig.layerMaskGround | MatchConfig.layerMaskObstacle);
				Debug.DrawRay(_raycastOrigins.bottomRight + (Vector2.down * .1f), Vector2.right * 3, Color.green);
				if(!hitUnderGround){
					Jump();
					Debug.Log("Jump 2");
					return;
				}
			}
		}else if(_jumpState != 2){
			if(_realVelocity.x == 0){
				RaycastHit2D hitBottom = Physics2D.Raycast(_raycastOrigins.bottomRight + (Vector2.up * .1f), Vector2.right, 3, MatchConfig.layerMaskGround | MatchConfig.layerMaskObstacle | MatchConfig.layerMaskHurtable);
				if(hitBottom && this.transform.position.y + _matchCharacter.characterData.matchDoubleJump >= hitBottom.collider.bounds.max.y){
					Jump();
					Debug.Log("Jump 3");
					return;
				}
			}else if(_jumpArrivePos.y > _lastJumpPos.y){
				Jump();
				Debug.Log("Jump 3.1");
				return;
			}else if(_rigidBody2D.velocity.y < 0){
				RaycastHit2D hitBottomRight = Physics2D.Raycast(_raycastOrigins.bottomRight, Vector2.down, 0.2f, MatchConfig.layerMaskHurtable | MatchConfig.layerMaskObstacle);
				Debug.DrawRay(_raycastOrigins.bottomRight, Vector2.down * 0.1f, Color.green);
				RaycastHit2D hitBottomLeft = Physics2D.Raycast(_raycastOrigins.bottomLeft, Vector2.down, 0.2f, MatchConfig.layerMaskHurtable | MatchConfig.layerMaskObstacle);
				Debug.DrawRay(_raycastOrigins.bottomLeft, Vector2.down * 0.1f, Color.green);
				if(hitBottomRight || hitBottomLeft){
					Collider2D collider = hitBottomRight.collider == null ? hitBottomLeft.collider : hitBottomRight.collider;
					if(((1<<collider.gameObject.layer) & MatchConfig.layerMaskHurtable) != 0)
					{
						Jump();
						Debug.Log("Jump 4");
						return;
					}else{
						AcceleratePlatform acceleratePlatform = collider.GetComponent<AcceleratePlatform>();
						if(acceleratePlatform != null){
							if(acceleratePlatform.speedMultipler < 0){
								Jump();
								Debug.Log("Jump 5");
								return;
							}
						}
					}
				}
			}
		}else{

		}
		if(_matchCharacter.holdingSkillData != null){
			RaycastHit2D hitBottom = Physics2D.Raycast(_raycastOrigins.bottomRight + (Vector2.up * .1f), Vector2.right, 6, MatchConfig.layerMaskPlayer);
			Debug.DrawRay(_raycastOrigins.bottomRight + (Vector2.up * .1f), Vector2.right * 6, Color.green);
			if(hitBottom && hitBottom.collider.transform.parent.parent != this.transform){
				this.photonView.RPC ("OnThrow", PhotonTargets.AllBufferedViaServer);
			}
		}
		if(canRide && _corountineCallPet == null){
			_corountineCallPet = StartCoroutine(CallRandomTimePet());
		}
	}

	void CalculateJumpProjectile(){
		_listProjectilePts.Clear();
		_listItemProjectilePts.Clear();
		_jumpForItem = false;
		_jumpArrivePos = new Vector2(float.MinValue, float.MinValue);
		float g = _rigidBody2D.gravityScale * Physics2D.gravity.magnitude;
		float uy = (_jumpState == 1 ? _jumpDoubleForce : _jumpForce) / _rigidBody2D.mass;
		float t = uy / g * 2;
		int section = 20;
		bool bottomFound = false;
		for(int i = 0; i < section * 2; i++){
			float dt = i * t / section;
			float y = this.transform.position.y + uy * dt + (-g * dt * dt)/2;
			float x = this.transform.position.x + _realVelocity.x * dt;
			Vector2 pt = new Vector2(x, y);
			if(!bottomFound){
				_listProjectilePts.Add(pt);
				int collisionMask = MatchConfig.layerMaskGround | MatchConfig.layerMaskObstacle | MatchConfig.layerMaskHurtable;
				Collider2D hitCollider = Physics2D.OverlapPoint(pt, collisionMask);
				if(hitCollider != null && Vector2.Distance(this.transform.position, pt) > 0.01f){
					if(hitCollider.GetComponent<HorizontalGroundBlock>() != null){
						_jumpArrivePos = pt;
					}
					bottomFound = true;
					if(_jumpForItem)break;
				}
			}
			pt.y += _raycastOrigins.topRight.y - _raycastOrigins.bottomRight.y;

			if(!_jumpForItem){
				_listItemProjectilePts.Add(pt);
				Collider2D hitItemCollider = Physics2D.OverlapPoint(pt, MatchConfig.layerMaskItem);
				if(hitItemCollider != null && hitItemCollider.bounds.min.y > _raycastOrigins.topRight.y){
					_jumpForItem = true;
					if(bottomFound)break;
				}
			}
		}
	}

	IEnumerator CallRandomTimePet(){
		yield return new WaitForSeconds(Random.Range(0.0f, 6.0f));
		this.photonView.RPC ("OnRide", PhotonTargets.AllBufferedViaServer);
		_corountineCallPet = null;
	}
}
