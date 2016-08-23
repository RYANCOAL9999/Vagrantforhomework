using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Koocell/RepeatBackground")]
public class RepeatBackground : MapObjectBase {
	public Sprite skySprite = null;
	[Range (1, 100)]
	public int count = 1;

	public bool valid{
		get{
			return skySprite != null;	
		}
	}

#if UNITY_EDITOR
	override public void Reset(){
		ClearChildren();
		AddChildren();
	}

	protected override void AddChildren(){
		for(int i = 0; i < count; i++){
			GameObject spriteGO = new GameObject("Sprite", typeof(SpriteRenderer));
			spriteGO.hideFlags = HideFlags.NotEditable;
			SpriteRenderer spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = skySprite;
			spriteRenderer.sortingLayerID = renderer.sortingLayerID;
			spriteRenderer.sortingOrder = renderer.sortingOrder;
			spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
			spriteGO.transform.SetParent(transform);
			spriteGO.transform.localPosition = new Vector3(i * skySprite.bounds.size.x, 0);
		}
	}
#endif
}
