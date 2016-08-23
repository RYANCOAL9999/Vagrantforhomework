using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(TestObject))]
public class TestObjectEditor : Editor {
	void OnEnable(){
		Tools.hidden = true;
	}

	void OnSceneGUI(){
		TestObject t = target as TestObject;
		EditorGUI.BeginChangeCheck ();
		Vector3 newPos = Handles.PositionHandle (t.transform.position, t.transform.rotation);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (t.transform, "TestObject Move");
			t.transform.position = new Vector3(Mathf.RoundToInt(newPos.x), Mathf.RoundToInt(newPos.y), Mathf.RoundToInt(newPos.z));
		}
		//
//		if (GUI.changed)
//			EditorUtility.SetDirty (t);
	}

	override public void OnInspectorGUI ()
	{
		TestObject t = (target as TestObject);
		serializedObject.Update ();
		SerializedProperty sprite = serializedObject.FindProperty ("sprite");
		SerializedProperty size = serializedObject.FindProperty ("size");
		EditorGUI.BeginChangeCheck ();
		EditorGUILayout.PropertyField (sprite, true);
		EditorGUILayout.PropertyField (size, true);
		//		EditorGUILayout.PropertyField (attachPoints, true);
		if (EditorGUI.EndChangeCheck ()) {
			serializedObject.ApplyModifiedProperties ();
			t.AddChildren ();
		}
	}
}
