#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

public class AddBackSpriteShader : MonoBehaviour {
	[MenuItem("Koocell/AddBackSpriteShader")]
	static void DoAddBackSpriteShader()
	{
		for (int i = 0; i < Selection.objects.Length; ++i)
		{
			AddSprideRenderer(Selection.objects[i]);
		}
	}

	static void AddSprideRenderer(Object obj){
		if(obj.GetType() == typeof(GameObject)){
			GameObject go = obj as GameObject;
			SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
			if(sr != null){
				sr.sharedMaterial = new Material(Shader.Find("Sprites/Default"));
			}
			foreach(Transform t in go.transform){
				AddSprideRenderer(t.gameObject);
			}
		}
	}
}
#endif