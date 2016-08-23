using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour {

	public struct RaycastOrigins{
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}
	public const float skinWidth = 0.015f;

	public LayerMask collisionMask;

	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;

	protected float _horizontalRaySpacing;
	protected float _verticalRaySpacing;

	protected BoxCollider2D _collider;
	protected RaycastOrigins _raycastOrigins;
	public virtual void Start () {
		_collider = GetComponent<BoxCollider2D>();
		CalulateRaySpacing();
	}

	public virtual void Update () {
	
	}

	protected void UpdateRaycastOrigins(bool withRotate = false){
		if(withRotate){
			float top = _collider.offset.y + (_collider.size.y / 2f) - skinWidth;
			float btm = _collider.offset.y - (_collider.size.y / 2f) - skinWidth;
			float left = _collider.offset.x - (_collider.size.x / 2f) - skinWidth;
			float right = _collider.offset.x + (_collider.size.x /2f) - skinWidth;

			_raycastOrigins.topLeft = transform.TransformPoint (new Vector3( left, top, 0f));
			_raycastOrigins.topRight = transform.TransformPoint (new Vector3( right, top, 0f));
			_raycastOrigins.bottomLeft = transform.TransformPoint (new Vector3( left, btm, 0f));
			_raycastOrigins.bottomRight = transform.TransformPoint (new Vector3( right, btm, 0f));
		}else{
			Bounds bounds = _collider.bounds;
			bounds.Expand (skinWidth * -2);

			_raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
			_raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
			_raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
			_raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
		}
	}

	protected void CalulateRaySpacing(bool withRotate = false){
		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
		if(withRotate){
			float sizeX = (_collider.size.x - (skinWidth * 2)) * _collider.transform.lossyScale.x;
			float sizeY = (_collider.size.y - (skinWidth * 2)) * _collider.transform.lossyScale.y;

			_horizontalRaySpacing = sizeY / (horizontalRayCount - 1);
			_verticalRaySpacing = sizeX / (verticalRayCount - 1);
		}else{
			Bounds bounds = _collider.bounds;
			bounds.Expand (skinWidth * -2);

			_horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
			_verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
		}
	}
}
