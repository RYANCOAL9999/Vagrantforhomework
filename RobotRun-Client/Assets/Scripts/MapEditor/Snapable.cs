using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
[SelectionBase]
public class Snapable : MonoBehaviour {
	[System.Serializable]
	public class AttachPoint{
		public Vector3 position = Vector3.zero;
		[HideInInspector]
		public Vector3 dragPosition = Vector3.zero;
		[HideInInspector]
		public Snapable parentSnapable = null;
	}

	public Vector2 snapOffset;
	public List<AttachPoint> attachPoints = new List<AttachPoint>();
//	Dictionary<AttachPoint, AttachPoint> _dictAttached = new Dictionary<AttachPoint, AttachPoint>();
	[System.Serializable]
	class AttachPointPairDictionary : SerializableDictionary<AttachPoint, AttachPoint> {}
	[SerializeField]
	private AttachPointPairDictionary _dictAttached = new AttachPointPairDictionary();
	public bool hasAttached{
		get{
			return _dictAttached.Count > 0;
		}
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
		if(Application.isEditor){
//			Debug.Log(this.name+" destroy");
			DetachAll();
		}
	}

	public bool IsAttached(AttachPoint attachPoint){
		return _dictAttached.ContainsKey(attachPoint);
	}

	public AttachPoint GetAttached(AttachPoint attachPoint){
		if(IsAttached(attachPoint)){
			return _dictAttached[attachPoint];
		}
		return null;
	}

	public void AttachTo(AttachPoint fromAttachPoint, AttachPoint targetAttachPoint){
		this.transform.position = targetAttachPoint.parentSnapable.transform.TransformPoint(targetAttachPoint.position) - fromAttachPoint.position;
		if(IsAttached(fromAttachPoint)){
			AttachPoint attacedPoint = _dictAttached[fromAttachPoint];
			attacedPoint.parentSnapable.Detach (attacedPoint);
			Detach(fromAttachPoint);
		}
		_dictAttached.Add(fromAttachPoint, targetAttachPoint);
		targetAttachPoint.parentSnapable.AttachBy(targetAttachPoint, fromAttachPoint);
	}

	public void AttachBy(AttachPoint myAttachPoint, AttachPoint targetAttachPoint){
		if(IsAttached(myAttachPoint)){
			Detach(myAttachPoint);
		}
		_dictAttached.Add(myAttachPoint, targetAttachPoint);
	}

	public void Detach(AttachPoint attachPoint){
		if(_dictAttached.ContainsKey(attachPoint)){
			_dictAttached.Remove(attachPoint);
		}
	}

	public void DetachAll(){
		foreach(AttachPoint attachPoint in _dictAttached.Keys){
			_dictAttached[attachPoint].parentSnapable.Detach(_dictAttached[attachPoint]);
		}
		_dictAttached.Clear();
	}

	public void UpdateAttachPoints(){
		if(attachPoints != null){
			foreach(AttachPoint attachPoint in attachPoints){
				attachPoint.parentSnapable = this;
			}
		}
	}

	public void UpdatePositionFromAttached(List<Snapable> listUpdated){
		if(!listUpdated.Contains(this)){
			listUpdated.Add(this);
			foreach(AttachPoint attachedPoint in _dictAttached.Keys){
				AttachPoint targetAttachPoint = _dictAttached[attachedPoint];
				Snapable target = targetAttachPoint.parentSnapable;
				if(!listUpdated.Contains(target)){
					target.transform.position = transform.TransformPoint(attachedPoint.position) - targetAttachPoint.position;
					target.UpdatePositionFromAttached(listUpdated);
				}
			}
		}
	}
}
