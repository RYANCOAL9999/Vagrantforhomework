using UnityEngine;
using UnityEditor;
using System.Collections;

namespace RobotRun.Koocell.Unity.Editor
{
	[CustomEditor (typeof(RepeatBackground))]
	public class RepeatBackgroundEditor : MapObjectBaseEditor
	{
		SerializedProperty skySprite, count;
		RepeatBackground component;

		override protected void OnEnable ()
		{
			base.OnEnable();
			count = serializedObject.FindProperty ("count");
			skySprite = serializedObject.FindProperty ("skySprite");
			component = (RepeatBackground)target;
		}

		override public void OnInspectorGUI ()
		{
			if (needsReset) {
				component.Reset ();
				needsReset = false;
				SceneView.RepaintAll ();
			}
			serializedObject.Update ();


			if (component.valid) {
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (skySprite);
				EditorGUILayout.PropertyField (count);
				if (EditorGUI.EndChangeCheck ()) {
					serializedObject.ApplyModifiedProperties ();
					needsReset = true;
					serializedObject.Update ();
				}
			} else {
				EditorGUILayout.PropertyField (skySprite);
				GUILayout.Label ("INVALID");
			}

			if (serializedObject.ApplyModifiedProperties () ||
			    (UnityEngine.Event.current.type == EventType.ValidateCommand && UnityEngine.Event.current.commandName == "UndoRedoPerformed")) {
				component.Reset ();
			}
			base.OnInspectorGUI();
		}
	}
}
