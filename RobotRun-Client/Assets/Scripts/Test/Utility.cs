using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RobotRun.Koocell.Unity
{
	public class Utility{
		public static IEnumerable<Transform> GetTransform (Transform transform)
		{

			IEnumerator tor = transform.GetEnumerator ();
			while (tor.MoveNext ()) {

				object current = tor.Current;

				yield return (Transform)current;

			}

		}

		public static void GizmosDrawCross(Vector3 position, float size, Color color){
			Gizmos.color = color;
			Gizmos.DrawLine(position - Vector3.up * size, position + Vector3.up * size);
			Gizmos.DrawLine(position - Vector3.left * size, position + Vector3.left * size);
		}
	}
}
