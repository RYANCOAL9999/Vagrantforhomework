using System;
using UnityEngine;

namespace Spine.Unity {
	/// <summary>Sets a GameObject's transform to match a bone on a Spine skeleton.</summary>
	[ExecuteInEditMode]
	[AddComponentMenu("Spine/BoneFollower(Unity UI Canvas)")]
	public class UIBoneFollower : MonoBehaviour {

		#region Inspector
		public SkeletonGraphic skeletonGraphic;
		public SkeletonGraphic SkeletonGraphic {
			get { return skeletonGraphic; }
			set {
				skeletonGraphic = value;
				Reset();
			}
		}
		/// <summary>If a bone isn't set, boneName is used to find the bone.</summary>
//		[SpineBone(dataField: "skeletonGraphic")]
		public String boneName;

		public bool followZPosition = true;
		public bool followBoneRotation = true;
		public bool resetOnAwake = true;
		#endregion

		[NonSerialized]
		public bool valid;

		[NonSerialized]
		public Bone bone;
		Transform skeletonTransform;

		public void HandleResetRenderer (SkeletonRenderer skeletonRenderer) {
			Reset();
		}

		public void Reset () {
			bone = null;
			valid = skeletonGraphic != null && skeletonGraphic.IsValid;

			if (!valid) return;

			skeletonTransform = skeletonGraphic.transform;
//			skeletonGraphic.OnRebuild -= HandleResetRenderer;
//			skeletonGraphic.OnRebuild += HandleResetRenderer;

			#if UNITY_EDITOR
			if (Application.isEditor)
				DoUpdate();
			#endif
		}

		void OnDestroy () {
//			if (skeletonGraphic != null)
//				skeletonGraphic.OnRebuild -= HandleResetRenderer;
		}

		public void Awake () {
			if (resetOnAwake)
				Reset();
		}

		void LateUpdate () {
			DoUpdate();
		}

		public void DoUpdate () {
			if (!valid) {
				Reset();
				return;
			}

			if (bone == null) {
				if (boneName == null || boneName.Length == 0)
					return;
				bone = skeletonGraphic.Skeleton.FindBone(boneName);
				if (bone == null) {
					Debug.LogError("Bone not found: " + boneName, this);
					return;
				}
			}

			Skeleton skeleton = skeletonGraphic.Skeleton;
			float flipRotation = (skeleton.flipX ^ skeleton.flipY) ? -1f : 1f;
			Transform thisTransform = this.transform;
			float canvasScale = skeletonGraphic.canvas == null?1:skeletonGraphic.canvas.referencePixelsPerUnit;

			// Recommended setup: Use local transform properties if Spine GameObject is parent
			if (thisTransform.parent == skeletonTransform) {
				thisTransform.localPosition = new Vector3(bone.worldX, bone.worldY, followZPosition ? 0f : thisTransform.localPosition.z);

				if (followBoneRotation) {
					Vector3 rotation = thisTransform.localRotation.eulerAngles;
					thisTransform.localRotation = Quaternion.Euler(rotation.x, rotation.y, bone.WorldRotationX * flipRotation);
				}

				// For special cases: Use transform world properties if transform relationship is complicated
			} else {
				Vector3 targetWorldPosition = skeletonTransform.TransformPoint(new Vector3(bone.worldX * canvasScale, bone.worldY * canvasScale, 0f));
				if (!followZPosition)
					targetWorldPosition.z = thisTransform.position.z;

				thisTransform.position = targetWorldPosition;

				if (followBoneRotation) {
					Vector3 worldRotation = skeletonTransform.rotation.eulerAngles;
					thisTransform.rotation = Quaternion.Euler(worldRotation.x, worldRotation.y, skeletonTransform.rotation.eulerAngles.z + (bone.WorldRotationX * flipRotation));
				}
			}

		}
	}

}
