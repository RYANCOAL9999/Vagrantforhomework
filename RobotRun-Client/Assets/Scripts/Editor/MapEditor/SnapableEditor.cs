using UnityEngine;
using UnityEditor;
using Spine.Unity.Editor;
using System.Collections.Generic;

[CustomEditor (typeof(Snapable))]
public class SnapableEditor : Editor
{
	int _controlHash;
	bool _mouseDown;
	Snapable.AttachPoint _selectedAttachPoint;
	int _currentPointIndex = -1;

	void OnEnable ()
	{
		Tools.hidden = true;
		_controlHash = GetHashCode ();
		_mouseDown = false;
		Snapable t = (target as Snapable);
		if (t != null) {
			t.UpdateAttachPoints ();
			ResetDragPosition ();
		}
	}

	void OnDisable(){
		Tools.hidden = false;
	}

	public void OnSceneGUI ()
	{
		Snapable t = (target as Snapable);
//		Event current = Event.current;
//		bool mouseUp = false;
//
//		switch (current.type) {
//		case EventType.MouseDown:
//			_mouseDown = true;
//			break;
//		case EventType.MouseUp:
//			mouseUp = true;
//			_currentPointIndex = -1;
//			ResetDragPosition ();
//			break;
//		case EventType.MouseDrag:
//			break;
//		case EventType.layout:
//			break;
//		}
		/*float size = HandleUtility.GetHandleSize(t.transform.position) * .1f;
		for (int i = 0; i < t.attachPoints.Count; ++i) {
			Snapable.AttachPoint attachPoint = t.attachPoints [i];
			Vector3 worldPosition = t.transform.TransformPoint (attachPoint.position);
			if (t.IsAttached (attachPoint)) {
				Handles.color = Color.green;
			} else {
				Handles.color = Color.white;
			}
			Handles.CubeCap (1, worldPosition, Quaternion.identity, 0.2f);
			Handles.color = Color.white;
			EditorGUI.BeginChangeCheck ();
			Vector3 newAttachPointPos = Handles.FreeMoveHandle (t.transform.TransformPoint (attachPoint.dragPosition), Quaternion.identity, .1f, Vector3.zero, Handles.RectangleCap);
			if (EditorGUI.EndChangeCheck ()) {
				Undo.RecordObject (target, "Snapable Attach Point Move");
				attachPoint.dragPosition = t.transform.InverseTransformPoint (newAttachPointPos);
				_selectedAttachPoint = attachPoint;
			}
			Handles.DrawDottedLine (worldPosition, newAttachPointPos, 1);
		}
		if (_mouseDown) {
			Snapable nearestSnapable = null;
			Snapable.AttachPoint nearestAttachPoint = null;
			float nearesrDist = float.MaxValue;
			Snapable[] snapabes = GameObject.FindObjectsOfType<Snapable> ();
			foreach (Snapable snapable in snapabes) {
				if (snapable != t) {
					List<Snapable.AttachPoint> attachPoints = snapable.attachPoints;
					Handles.color = Color.white;
					foreach (Snapable.AttachPoint attachPoint in attachPoints) {
						Vector3 worldPos = snapable.transform.TransformPoint (attachPoint.position);
						Handles.CubeCap (1, worldPos, Quaternion.identity, 0.2f);
						float dist = HandleUtility.DistanceToRectangle (worldPos, Quaternion.identity, 0.2f);
						if (dist < nearesrDist) {
							nearestSnapable = snapable;
							nearestAttachPoint = attachPoint;
							nearesrDist = dist;
						}
					}
				}
			}
			bool attach = false;
			bool detach = false;
			if (nearestSnapable != null && nearestAttachPoint != null) {
				if (nearesrDist < 0.5f) {
					Handles.color = Color.green;
					Vector3 worldPos = nearestSnapable.transform.TransformPoint (nearestAttachPoint.position);
					Handles.CubeCap (1, worldPos, Quaternion.identity, 0.2f);
					attach = true;
				} else if (nearesrDist > 5 && _selectedAttachPoint != null && t.IsAttached (_selectedAttachPoint)) {
					Handles.color = Color.red;
					Handles.CubeCap (1, t.transform.TransformPoint (_selectedAttachPoint.position), Quaternion.identity, 0.2f);
					detach = true;
				}
			}
			if (mouseUp) {
				_mouseDown = false;
				ResetDragPosition ();
				if(_selectedAttachPoint != null){
					if (attach) {
						t.AttachTo (_selectedAttachPoint, nearestAttachPoint);
					}
					if (detach) {
						Snapable.AttachPoint attacedPoint = t.GetAttached (_selectedAttachPoint);
						attacedPoint.parentSnapable.Detach (attacedPoint);
						t.Detach (_selectedAttachPoint);
					}
				}
			}
		}*/
		EditorGUI.BeginChangeCheck ();
//		Vector3 newPos = Handles.FreeMoveHandle (t.transform.position, t.transform.rotation, 1, Vector3.zero, Handles.RectangleCap);
		Vector3 newPos = Handles.PositionHandle (t.transform.position, t.transform.rotation);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (t.transform, "Snapable Move");
			t.transform.position = new Vector3(Mathf.RoundToInt(newPos.x)+t.snapOffset.x, Mathf.RoundToInt(newPos.y)+t.snapOffset.y, Mathf.RoundToInt(newPos.z));
//			t.UpdatePositionFromAttached (new List<Snapable> ());
//			_selectedAttachPoint = null;
		}
	}

	public override void OnInspectorGUI ()
	{
		Snapable t = (target as Snapable);
		serializedObject.Update ();

		EditorGUIUtility.LookLikeInspector ();
		SerializedProperty snapOffset = serializedObject.FindProperty ("snapOffset");
//		SerializedProperty attachPoints = serializedObject.FindProperty ("attachPoints");

		EditorGUI.BeginChangeCheck ();
		EditorGUILayout.PropertyField (snapOffset, true);
//		EditorGUILayout.PropertyField (attachPoints, true);
		if (EditorGUI.EndChangeCheck ()) {
			serializedObject.ApplyModifiedProperties ();
			t.UpdateAttachPoints ();
		}
		EditorGUIUtility.LookLikeControls ();
		if (GUI.changed)
			EditorUtility.SetDirty (t);
	}

	void ResetDragPosition ()
	{
		Snapable t = (target as Snapable);
		foreach (Snapable.AttachPoint attachPoint in t.attachPoints) {
			attachPoint.dragPosition = attachPoint.position;
		}
	}
}
