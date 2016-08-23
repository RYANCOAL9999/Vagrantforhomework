using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace RobotRun.Koocell.Unity.Editor
{
	[CustomEditor (typeof(ListBackground))]
	public class ListBackgroundEditor : MapObjectBaseEditor {
		SerializedProperty listSprite;
		ListBackground component;

		void OnEnable ()
		{
			base.OnEnable();
			listSprite = serializedObject.FindProperty ("listSprite");
			component = (ListBackground)target;
		}

		override public void OnInspectorGUI ()
		{
			if (needsReset) {
				component.Reset ();
				needsReset = false;
				SceneView.RepaintAll ();
			}
			serializedObject.Update ();
			this.DrawDefaultInspector(); // show the default inspector so we can set some values

			if (GUI.changed){
				needsReset = true;
				EditorUtility.SetDirty(component);
			}
			EditorGUILayout.Space();
			base.OnInspectorGUI();
		}
	}
}
