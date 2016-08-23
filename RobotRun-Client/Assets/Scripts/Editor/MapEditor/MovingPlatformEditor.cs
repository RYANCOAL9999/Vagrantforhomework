using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(MovingPlatform))]
public class MovingPlatformEditor : Editor {
	
	public void OnSceneGUI ()
	{
		if(!Application.isPlaying){
			MovingPlatform t = target as MovingPlatform;
			float size = HandleUtility.GetHandleSize(t.transform.position) * .1f;
			for (int i = 0; i < t.localWayPoints.Length; i++){
				Vector3 pos = t.localWayPoints[i];
				EditorGUI.BeginChangeCheck ();
				Vector3 newPos = Handles.FreeMoveHandle(t.transform.TransformPoint(pos), Quaternion.identity, size, Vector3.zero, Handles.CubeCap);
				if (EditorGUI.EndChangeCheck ()) {
					Undo.RecordObject (t, "MovingPlatform Way PointMove");
					t.localWayPoints[i] = t.transform.InverseTransformPoint(newPos);
				}
			}
		}
	}
}
