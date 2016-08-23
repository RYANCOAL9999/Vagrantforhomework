using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Obstacle : MapObjectBase
{
	public Sprite sprite;

	[Range(1,100)]
	public int width = 1;
	[Range(1,100)]
	public int height = 1;

	public float spriteWidth = 1;
	public float spriteHeight = 1;

	public Vector2 spriteOffset;
	public Vector2 colliderOffsetMin;
	public Vector2 colliderOffsetMax;

	BoxCollider2D _collider;

	public bool valid{
		get{
			return sprite != null;	
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
		for(int y = height-1; y >= 0; y--){
			for(int x = 0; x < width; x++){
				GameObject spriteGO = new GameObject("Sprite", typeof(SpriteRenderer));
				spriteGO.hideFlags = HideFlags.NotEditable;
				SpriteRenderer spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();
				spriteRenderer.sprite = sprite;
				spriteGO.transform.localPosition = new Vector3(spriteWidth * x + spriteOffset.x, spriteHeight * y + spriteOffset.y);
				spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
				spriteRenderer.sortingLayerID = renderer.sortingLayerID;
				spriteRenderer.sortingOrder = renderer.sortingOrder + y * width - x + width;
				spriteGO.transform.SetParent(transform, false);
			}
		}
		Vector2 newSize = new Vector2(Mathf.Max(0.01f, spriteWidth * width - colliderOffsetMin.x - colliderOffsetMax.x), Mathf.Max(0.01f, spriteHeight * height - colliderOffsetMin.y - colliderOffsetMax.y));
		Vector2 newOffset = new Vector2((spriteWidth * width + colliderOffsetMin.x - colliderOffsetMax.x)/2 + spriteOffset.x, (spriteHeight * height +  colliderOffsetMin.y - colliderOffsetMax.y)/2 + spriteOffset.y);
		_collider.offset = newOffset;
		_collider.size = newSize;
	}
	#endif
}
