using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour {
	
	public RectTransform characterContainer = null;
	public GameObject prefabUICharacter = null;
	public Transform RightButton;
	public Transform LeftButton;

	[HideInInspector]
	public int _currentIdx = 0;
	List<UICharacter> _listUICharacter = new List<UICharacter>();

	public int currectSelecedIndex{
		get{
			return _currentIdx;
		}
	}
		

	void Start () {
		//_currentIdx = 0;
		_currentIdx = GameDataManager.Instance.userData.selectedCharacterIndex;
		Init();
	}
		
	void Update () {
		
		UpdateSelection ();
		GameDataManager.Instance.userData.selectedCharacterIndex = _currentIdx;

		if (_currentIdx == 0) {
			LeftButton.gameObject.SetActive (false);
		} 
		else {
			LeftButton.gameObject.SetActive (true);
		}


		if (_currentIdx == _listUICharacter.Count - 1) {
			RightButton.gameObject.SetActive (false);
		} 
		else {
			RightButton.gameObject.SetActive (true);
		}

	}

	void Init(){
		foreach(CharacterData characterData in GameDataManager.Instance.userData.listCharacterData){
			UICharacter uiCharacter = Instantiate (prefabUICharacter).GetComponent<UICharacter>();
			uiCharacter.characterData = characterData;
			if (characterData.unlocked) {
				uiCharacter.Enable ();
			} else {
				uiCharacter.Disable ();
			}
			uiCharacter.transform.SetParent (characterContainer, false);
			_listUICharacter.Add(uiCharacter);
		}
	}

	void UpdateSelection(){
		RectTransform rtSelected = _listUICharacter[_currentIdx].GetComponent<RectTransform>();
		float newPosX = -(rtSelected.anchoredPosition.x - rtSelected.rect.width / 2);
		characterContainer.anchoredPosition = new Vector2 (newPosX, characterContainer.anchoredPosition.y);

	}

	public void OnLeftBtnClick(){
		_currentIdx = Mathf.Max(0, _currentIdx - 1);
		UpdateSelection();

	}

	public void OnRightBtnClick(){
		_currentIdx = Mathf.Min(_listUICharacter.Count-1, _currentIdx + 1);
		UpdateSelection();

	}


		
}
