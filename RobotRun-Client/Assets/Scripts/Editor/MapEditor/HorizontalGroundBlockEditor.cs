using UnityEngine;
using UnityEditor;
using System.Collections;


namespace RobotRun.Koocell.Unity.Editor
{
	[CustomEditor (typeof(HorizontalGroundBlock))]
	public class HorizontalGroundBlockEditor : MapObjectBaseEditor {
		protected SerializedProperty spriteLeft, spriteMiddle, spriteRight, size, spriteLeftWidth, spriteMiddleWidth, spriteRightWidth, colliderOffsetMin, colliderOffsetMax;
		protected HorizontalGroundBlock component;

		override protected void OnEnable ()
		{
			base.OnEnable();
			size = serializedObject.FindProperty ("size");
			spriteLeftWidth = serializedObject.FindProperty ("spriteLeftWidth");
			spriteMiddleWidth = serializedObject.FindProperty ("spriteMiddleWidth");
			spriteRightWidth = serializedObject.FindProperty ("spriteRightWidth");
			spriteLeft = serializedObject.FindProperty ("spriteLeft");
			spriteMiddle = serializedObject.FindProperty ("spriteMiddle");
			spriteRight = serializedObject.FindProperty ("spriteRight");
			colliderOffsetMin = serializedObject.FindProperty ("colliderOffsetMin");
			colliderOffsetMax = serializedObject.FindProperty ("colliderOffsetMax");
			component = (HorizontalGroundBlock)target;
		}

		override public void OnInspectorGUI ()
		{
			if (needsReset) {
				component.Reset ();
				needsReset = false;
				SceneView.RepaintAll ();
			}
			serializedObject.Update ();


			if (component != null && component.valid) {
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (spriteLeft);
				EditorGUILayout.PropertyField (spriteMiddle);
				EditorGUILayout.PropertyField (spriteRight);
				EditorGUILayout.PropertyField (size);
				EditorGUILayout.PropertyField (spriteLeftWidth);
				EditorGUILayout.PropertyField (spriteMiddleWidth);
				EditorGUILayout.PropertyField (spriteRightWidth);
				EditorGUILayout.PropertyField (colliderOffsetMin);
				EditorGUILayout.PropertyField (colliderOffsetMax);
				if (EditorGUI.EndChangeCheck ()) {
					serializedObject.ApplyModifiedProperties ();
					needsReset = true;
					serializedObject.Update ();
				}
			} else {
				EditorGUILayout.PropertyField (spriteLeft);
				EditorGUILayout.PropertyField (spriteMiddle);
				EditorGUILayout.PropertyField (spriteRight);
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
