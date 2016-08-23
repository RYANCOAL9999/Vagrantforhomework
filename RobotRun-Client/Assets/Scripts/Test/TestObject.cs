using UnityEngine;
using System.Collections;

public class TestObject : MonoBehaviour {
	public Sprite sprite;
	public int size = 2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddChildren(){
		for(int i = 0; i < size; i++){
			GameObject spriteGO = new GameObject("Sprite", typeof(SpriteRenderer));
			spriteGO.hideFlags = HideFlags.NotEditable;
			SpriteRenderer spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = sprite;
			spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
			spriteRenderer.transform.SetParent(this.transform, false);
		}
	}
}
