using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchProgressBar : MonoBehaviour {
	public List<MatchProgressBarIcon> listMatchProgressBarIcon = new List<MatchProgressBarIcon>();

	Transform _tranStart;
	Transform _tranEnd;

	RectTransform _rtBounds = null;
	float _worldWidth = 0;
	float _barWidth = 0;

	void Awake (){
		_rtBounds = this.transform.FindChild("bounds").GetComponent<RectTransform>();
	}

	void Start () {
		_barWidth = _rtBounds.rect.width;
		for(int i = 0; i < listMatchProgressBarIcon.Count; i++){
			MatchProgressBarIcon matchProgressBarIcon = listMatchProgressBarIcon[i];
			matchProgressBarIcon.index = i;
		}
	}
		
	void Update () {
		if(_tranStart == null || _tranEnd == null){
			return;
		}
		foreach(MatchProgressBarIcon icon in listMatchProgressBarIcon){
			if(icon.matchCharacterController != null){
				float moved = icon.matchCharacterController.transform.position.x - _tranStart.position.x;
				float localX = Mathf.Clamp(-_barWidth/2 + moved/_worldWidth*_barWidth, -_barWidth/2, _barWidth/2);
				icon.transform.localPosition = new Vector3(localX, icon.transform.localPosition.y, icon.transform.localPosition.z);
			}
		}
	}

	public void SetStartEndTransform(Transform start, Transform end){
		_tranStart = start;
		_tranEnd = end;
		_worldWidth = end.position.x-start.position.x;
	}

	public void SetMatchCharacterControllerToIcon(int index, MatchCharacterController matchCharacterController){
		if(index < 0 || index >= listMatchProgressBarIcon.Count)return;
		listMatchProgressBarIcon[index].matchCharacterController = matchCharacterController;
	}
}
