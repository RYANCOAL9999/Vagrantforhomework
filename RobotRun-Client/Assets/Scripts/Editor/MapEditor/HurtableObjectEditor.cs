using UnityEngine;
using UnityEditor;
using System.Collections;
using RobotRun.Koocell.Unity.Editor;

[CustomEditor (typeof(HurtableObject))]
[CanEditMultipleObjects]
public class HurtableObjectEditor : MapObjectBaseEditor {

	void OnSceneGUI ()
	{
		HurtableObject t = (target as HurtableObject);
		EditorGUI.BeginChangeCheck ();
		Vector3 newPos = Handles.PositionHandle (t.transform.TransformPoint(t.rebornPosition), Quaternion.identity);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (target, "HurtableObject Move");
			t.rebornPosition = t.transform.InverseTransformPoint(newPos);
		}
		Handles.DrawDottedLine (t.transform.position, t.transform.TransformPoint(t.rebornPosition), 1);

		if (GUI.changed)
			EditorUtility.SetDirty (t);
	}

	override public void OnInspectorGUI ()
	{

		EditorGUIUtility.LookLikeInspector ();
		SerializedProperty rebornPosition = serializedObject.FindProperty ("rebornPosition");

		EditorGUI.BeginChangeCheck ();
		EditorGUILayout.PropertyField (rebornPosition, true);
		if (EditorGUI.EndChangeCheck ()) {
			serializedObject.ApplyModifiedProperties ();
		}
	}

	[MenuItem ("Koocell/ChangeRebornPointToLocal")]
	static void ChangeRebornPointToLocal () {
		HurtableObject[] hurtableObjects = GameObject.FindObjectsOfType<HurtableObject>();
		foreach(HurtableObject h in hurtableObjects){
			Debug.Log("ChangeRebornPointToLocal: "+h.name);
			Undo.RecordObject (h, "ChangeRebornPointToLocal");
			h.rebornPosition = h.transform.InverseTransformPoint(h.rebornPosition);
			EditorUtility.SetDirty (h);
		}
	}
}
