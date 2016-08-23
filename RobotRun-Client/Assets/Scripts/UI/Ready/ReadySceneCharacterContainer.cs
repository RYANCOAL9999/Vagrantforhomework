using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReadySceneCharacterContainer : MonoBehaviour {

	CanvasGroup _canvasGroup = null;
	UICharacter _uiCharacter = null;
	Image _imagePlayerNameBG = null;
	Text _textPlayerName = null;
	Text _textLevel = null;


	UserData _userData = null;
	public UserData userData{
		get{
			return _userData;
		}
		set{
			_userData = value;
			if(_userData != null){
				if(_userData.selectedCharacter != null){
					_uiCharacter.characterData = _userData.selectedCharacter;
				}
				_textPlayerName.text = _userData.name;
				_textLevel.text = _userData.lv.ToString();
			}
		}
	}
	Color _color = Color.white;
	public Color color{
		get{
			return _color;
		}
		set{
			_color = value;
			_imagePlayerNameBG.color = _color;
		}
	}

	void Awake(){
		_canvasGroup = this.GetComponent<CanvasGroup>();
		_uiCharacter = this.transform.FindChild("UICharacter").GetComponent<UICharacter>();
		_imagePlayerNameBG = this.transform.FindChild("ImageNameBG").GetComponent<Image>();
		_textPlayerName = _imagePlayerNameBG.transform.FindChild("TextPlayerName").GetComponent<Text>();
		_textLevel = _imagePlayerNameBG.transform.FindChild("ImageLevelBG/TextLevel").GetComponent<Text>();
	}

	void Start () {
	
	}


	void Update () {
	
	}

	public void Hide(){
		_canvasGroup.alpha = 0;
	}

	public void Show(UserData userData){
		this.userData = userData;
		_canvasGroup.alpha = 1;
	}
}
