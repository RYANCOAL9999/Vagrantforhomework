using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float moveSpeed = 6;
	public float  jumpHeight = 4;
	public float timeToJumpApex = 0.4f;
	public float accelerationTimeAirborne = 0.2f;
	public float accelerationTimeGrounded = 0.1f;
	float _gravity;
	float _jumpVelocity = 8;
	Vector3 _velocity;
	Vector3 _velocityApplied;
	bool _doubleJump;
	float velocityXSmoothing;
	PlayerController2D _playerController2D;

	void Start () {
		_playerController2D = GetComponent<PlayerController2D>();
		_gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		_jumpVelocity = Mathf.Abs(_gravity) * timeToJumpApex;
		_velocityApplied = Vector3.zero;
	}

	void Update () {

		if(_playerController2D.collisionInfo.above || _playerController2D.collisionInfo.below){
			_velocity.y = 0;
		}

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		if(Input.GetKeyDown(KeyCode.Space)){// && _playerController2D.collisionInfo.below){
			if( _playerController2D.collisionInfo.below){
				_doubleJump = false;
				_velocity.y = _jumpVelocity;
			}else if(!_doubleJump){
				_doubleJump = true;
				_velocity.y = _jumpVelocity;
			}
		}

		float targetVelocityX = input.x * moveSpeed;
		_velocity.x = Mathf.SmoothDamp(_velocity.x, targetVelocityX, ref velocityXSmoothing, (_playerController2D.collisionInfo.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		_velocity += _velocityApplied;
		_velocity.y += _gravity * Time.deltaTime;
		_playerController2D.Move(_velocity * Time.deltaTime);
		_velocityApplied = Vector3.zero;
	}

	public void ApplyExternalVelocity(Vector3 velocity){
		_velocityApplied += velocity;
	}
}
