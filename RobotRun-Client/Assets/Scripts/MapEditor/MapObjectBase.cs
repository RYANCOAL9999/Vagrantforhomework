using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RobotRun.Koocell.Unity;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
public class MapObjectBase : MonoBehaviour {
//	public SortingLayer childSortingLayer;
//	public int childOrderInLayer;
	#if UNITY_EDITOR
	public MeshRenderer renderer;
//	public bool hideChildGameObject = false;
	#endif

	virtual protected void OnEnable(){
		#if UNITY_EDITOR
		renderer = GetComponent<MeshRenderer>();
		renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		renderer.receiveShadows = false;
		renderer.useLightProbes = false;
		renderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
		renderer.hideFlags = HideFlags.HideInInspector;
		if(!Application.isPlaying){
			Reset();
		}
		#endif
	}
	#if UNITY_EDITOR

	virtual public void Reset(){
		
	}

	virtual protected void ClearChildren(){
		List<Transform> trans = new List<Transform>(Utility.GetTransform(transform));
		foreach(Transform tran in trans){
			if(Application.isEditor){
				DestroyImmediate(tran.gameObject);
			}else{
				Destroy(tran.gameObject);
			}
		}
	}

	virtual protected void AddChildren(){
		
	}

	virtual public void UpdateChildrenOrder(){
		foreach(SpriteRenderer spriteRenderer in transform.GetComponentsInChildren<SpriteRenderer>()){
			spriteRenderer.sortingLayerID = renderer.sortingLayerID;
			spriteRenderer.sortingOrder = renderer.sortingOrder;
		}
	}

	virtual public void UpdateChildrenHide(){
//		foreach(Transform child in transform){
//			child.gameObject.hideFlags = hideChildGameObject?HideFlags.HideInHierarchy:HideFlags.None;
//		}
//		EditorApplication.RepaintHierarchyWindow();
	}

	virtual protected void Awake(){

	}

	virtual protected void Start(){
		
	}

	virtual protected void Update(){

	}

	virtual protected void LateUpdate(){

	}

	virtual protected void FixedUpdate(){

	}

	#endif
}
