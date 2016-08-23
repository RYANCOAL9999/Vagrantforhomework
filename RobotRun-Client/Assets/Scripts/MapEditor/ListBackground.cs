using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RobotRun.Koocell.Unity;

[ExecuteInEditMode]
[AddComponentMenu("Koocell/ListBackground")]
public class ListBackground : MapObjectBase {
	public List<Sprite> listSprite = new List<Sprite>();

#if UNITY_EDITOR
	override public void Reset(){
		ClearChildren();
		AddChildren();
	}

	protected override void AddChildren(){
		int count = 0;
		foreach(Sprite sprite in listSprite){
			if(sprite != null){
				GameObject spriteGO = new GameObject("Sprite", typeof(SpriteRenderer));
				spriteGO.hideFlags = HideFlags.NotEditable;
				SpriteRenderer spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();
				spriteRenderer.sprite = sprite;
				spriteRenderer.sortingLayerID = renderer.sortingLayerID;
				spriteRenderer.sortingOrder = renderer.sortingOrder;
				spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
				spriteGO.transform.SetParent(transform);
				spriteGO.transform.localPosition = new Vector3(count * sprite.bounds.size.x, 0);
				count++;
			}
		}
	}
#endif
}
