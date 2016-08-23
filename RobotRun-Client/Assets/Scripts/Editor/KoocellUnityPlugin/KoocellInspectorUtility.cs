using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Koocell.Unity.Editor {

	public static class KoocellInspectorUtility {

		public static string Pluralize (int n, string singular, string plural) {
			return n + " " + (n == 1 ? singular : plural);
		}

		public static string PluralThenS (int n) {
			return n == 1 ? "" : "s";
		}

		#region Sorting Layer Field Helpers
		static readonly GUIContent SortingLayerLabel = new GUIContent("Sorting Layer");
		static readonly GUIContent OrderInLayerLabel = new GUIContent("Order in Layer");

		static MethodInfo m_SortingLayerFieldMethod;
		static MethodInfo SortingLayerFieldMethod {
			get {
				if (m_SortingLayerFieldMethod == null)
					m_SortingLayerFieldMethod = typeof(EditorGUILayout).GetMethod("SortingLayerField", BindingFlags.Static | BindingFlags.NonPublic, null, new [] { typeof(GUIContent), typeof(SerializedProperty), typeof(GUIStyle) }, null);

				return m_SortingLayerFieldMethod;
			}
		}

		public struct SerializedSortingProperties {
			public SerializedObject renderer;
			public SerializedProperty sortingLayerID;
			public SerializedProperty sortingOrder;

			public SerializedSortingProperties (Renderer r) {
				renderer = new SerializedObject(r);
				sortingLayerID = renderer.FindProperty("m_SortingLayerID");
				sortingOrder = renderer.FindProperty("m_SortingOrder");
			}

			public void ApplyModifiedProperties () {
				renderer.ApplyModifiedProperties();
			}
		}

		public static void SortingPropertyFields (SerializedSortingProperties prop, bool applyModifiedProperties) {
			if (applyModifiedProperties) {
				EditorGUI.BeginChangeCheck();
				SortingPropertyFields(prop.sortingLayerID, prop.sortingOrder);
				if(EditorGUI.EndChangeCheck()) {
					prop.ApplyModifiedProperties();
					EditorUtility.SetDirty(prop.renderer.targetObject);
				}
			} else {
				SortingPropertyFields(prop.sortingLayerID, prop.sortingOrder);
			}
		}

		public static void SortingPropertyFields (SerializedProperty m_SortingLayerID, SerializedProperty m_SortingOrder) {
			if (KoocellInspectorUtility.SortingLayerFieldMethod != null && m_SortingLayerID != null) {
				KoocellInspectorUtility.SortingLayerFieldMethod.Invoke(null, new object[] { SortingLayerLabel, m_SortingLayerID, EditorStyles.popup } );
			} else {
				EditorGUILayout.PropertyField(m_SortingLayerID);
			}

			EditorGUILayout.PropertyField(m_SortingOrder, OrderInLayerLabel);
		}
		#endregion
	}
}