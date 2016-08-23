using UnityEngine;
using UnityEditor;
using System.Collections;
using RobotRun.Koocell.Unity.Editor;


[CustomEditor (typeof(DropArea))]
[CanEditMultipleObjects]
public class DropAreaEditor : MapObjectBaseEditor {

	void OnSceneGUI ()
	{
		DropArea t = (target as DropArea);
		EditorGUI.BeginChangeCheck ();
		Vector3 newPos = Handles.PositionHandle (t.transform.TransformPoint(t.rebornPosition), Quaternion.identity);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (target, "DropArea Move");
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
}
