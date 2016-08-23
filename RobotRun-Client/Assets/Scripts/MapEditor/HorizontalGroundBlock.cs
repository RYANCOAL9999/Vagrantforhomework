using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(BoxCollider2D))]
public class HorizontalGroundBlock : MapObjectBase {
	public Sprite spriteLeft;
	public Sprite spriteMiddle;
	public Sprite spriteRight;

	[Range(2,100)]
	public int size = 2;
	public float spriteLeftWidth = 1;
	public float spriteMiddleWidth = 1;
	public float spriteRightWidth = 1;
	public Vector2 colliderOffsetMin;
	public Vector2 colliderOffsetMax;

	BoxCollider2D _collider;

	public bool valid{
		get{
			return spriteLeft != null && spriteMiddle != null && spriteRight != null;	
		}
	}

	override protected void OnEnable(){
		_collider = GetComponent<BoxCollider2D>();
		base.OnEnable();
	}

#if UNITY_EDITOR
	override public void Reset(){
		ClearChildren();
		AddChildren();
	}

	protected override void AddChildren(){
		float boundsWidth = (size -2 ) * spriteMiddleWidth + spriteLeftWidth + spriteRightWidth;
		float boundsHeight = 0;
		for(int i = 0; i < size; i++){
			GameObject spriteGO = new GameObject("Sprite", typeof(SpriteRenderer));
			spriteGO.hideFlags = HideFlags.NotEditable;
			SpriteRenderer spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();
			if(i == 0){
				spriteRenderer.sprite = spriteLeft;
				spriteGO.transform.localPosition = new Vector3((spriteLeftWidth-boundsWidth)/2, 0);
			}else if(i == size-1){
				spriteRenderer.sprite = spriteRight;
				spriteGO.transform.localPosition = new Vector3(spriteLeftWidth + (spriteRightWidth-boundsWidth)/2 + ((i-1) * spriteMiddleWidth), 0);
			}else{
				spriteRenderer.sprite = spriteMiddle;
				spriteGO.transform.localPosition = new Vector3(spriteLeftWidth + (spriteMiddleWidth-boundsWidth)/2 + ((i-1) * spriteMiddleWidth), 0);
			}
			spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
			spriteRenderer.sortingLayerID = renderer.sortingLayerID;
			spriteRenderer.sortingOrder = renderer.sortingOrder;
			spriteGO.transform.SetParent(transform, false);
			if(spriteRenderer.bounds.size.y > boundsHeight){
				boundsHeight = spriteRenderer.bounds.size.y;
			}
		}
		Vector2 newSize = new Vector2(Mathf.Max(0.01f, boundsWidth - colliderOffsetMin.x - colliderOffsetMax.x), Mathf.Max(0.01f, boundsHeight - colliderOffsetMin.y - colliderOffsetMax.y));
		Vector2 newOffset = new Vector2((colliderOffsetMin.x - colliderOffsetMax.x)/2, (colliderOffsetMin.y - colliderOffsetMax.y)/2);
		_collider.offset = newOffset;
		_collider.size = newSize;
	}
	#endif
}
