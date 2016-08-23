using UnityEngine;
using UnityEditor;

namespace RobotRun.Koocell.Unity.Editor
{
	[CustomEditor (typeof(Obstacle))]
	public class ObstacleEditor : MapObjectBaseEditor
	{
		protected SerializedProperty width, height, sprite, spriteWidth, spriteHeight, spriteOffset, colliderOffsetMin, colliderOffsetMax;
		protected Obstacle component;
		override protected void OnEnable ()
		{
			base.OnEnable();
			width = serializedObject.FindProperty ("width");
			height = serializedObject.FindProperty ("height");
			sprite = serializedObject.FindProperty ("sprite");
			spriteWidth = serializedObject.FindProperty ("spriteWidth");
			spriteHeight = serializedObject.FindProperty ("spriteHeight");
			spriteOffset = serializedObject.FindProperty ("spriteOffset");
			colliderOffsetMin = serializedObject.FindProperty ("colliderOffsetMin");
			colliderOffsetMax = serializedObject.FindProperty ("colliderOffsetMax");
			component = (Obstacle)target;
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
				EditorGUILayout.PropertyField (width);
				EditorGUILayout.PropertyField (height);
				EditorGUILayout.PropertyField (sprite);
				EditorGUILayout.PropertyField (spriteWidth);
				EditorGUILayout.PropertyField (spriteHeight);
				EditorGUILayout.PropertyField (spriteOffset);
				EditorGUILayout.PropertyField (colliderOffsetMin);
				EditorGUILayout.PropertyField (colliderOffsetMax);
				if (EditorGUI.EndChangeCheck ()) {
					serializedObject.ApplyModifiedProperties ();
					needsReset = true;
					serializedObject.Update ();
				}
			} else {
				EditorGUILayout.PropertyField (sprite);
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
