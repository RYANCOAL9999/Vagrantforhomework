using UnityEngine;
using UnityEditor;
using System.Collections;
using Koocell.Unity.Editor;

namespace RobotRun.Koocell.Unity.Editor
{
	[CustomEditor (typeof(MapObjectBase))]
	public class MapObjectBaseEditor : UnityEditor.Editor {
		protected KoocellInspectorUtility.SerializedSortingProperties sortingProperties;
		MapObjectBase _mapObjectBase;

		bool needsUpdateLayer;
		protected bool needsReset;

		virtual protected void OnEnable(){
			_mapObjectBase = (MapObjectBase)target;
			if(_mapObjectBase != null){
				var renderer = _mapObjectBase.GetComponent<Renderer>();
				_mapObjectBase.renderer = (MeshRenderer)renderer;
				sortingProperties = new KoocellInspectorUtility.SerializedSortingProperties(renderer);
			}
		}

		override public void OnInspectorGUI ()
		{
			if(needsUpdateLayer){
				_mapObjectBase.UpdateChildrenOrder();
				needsUpdateLayer = false;
				SceneView.RepaintAll ();
			}
			serializedObject.Update ();
//			SerializedProperty hideChildGameObject = serializedObject.FindProperty ("hideChildGameObject");
//			EditorGUI.BeginChangeCheck ();
//			EditorGUILayout.PropertyField (hideChildGameObject, true);
//			if (EditorGUI.EndChangeCheck ()) {
//				serializedObject.ApplyModifiedProperties ();
//				_mapObjectBase.UpdateChildrenHide();
//			}
			EditorGUI.BeginChangeCheck ();
			KoocellInspectorUtility.SortingPropertyFields(sortingProperties, applyModifiedProperties: true);
			if (EditorGUI.EndChangeCheck ()) {
				serializedObject.ApplyModifiedProperties ();
				needsUpdateLayer = true;
			}
		}
	}
}
