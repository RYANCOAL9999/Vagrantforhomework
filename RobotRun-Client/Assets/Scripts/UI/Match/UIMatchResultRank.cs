using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMatchResultRank : MonoBehaviour {
	RectTransform _rtContent;
	Text _textPlayerName;
	CanvasGroup _canvasGroup;

	public string playerName{
		set{
			_textPlayerName.text = value;
		}
	}

	public bool isHide{
		get{
			return _canvasGroup.alpha == 0;
		}
	}

	public RectTransform rtContent{
		get{
			return _rtContent;
		}
	}

	void Awake (){
		_canvasGroup = this.transform.GetComponent<CanvasGroup>();
		_rtContent = this.transform.FindChild("ImageBG").GetComponent<RectTransform>();
		_textPlayerName = _rtContent.FindChild("TextPlayerName").GetComponent<Text>();
	}

	void Start () {
	
	}

	void Update () {
	
	}

	public void Show(){
		_canvasGroup.alpha = 1;
	}

	public void Hide(){
		_canvasGroup.alpha = 0;
	}
}
