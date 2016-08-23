using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMatchResultReward : MonoBehaviour {
	Text _textEarnAmount;
	Text _textTotalAmount;

	public int earn{
		set{
			_textEarnAmount.text = value.ToString();
		}
	}

	public int total{
		set{
			_textTotalAmount.text = value.ToString();
		}
	}

	void Awake(){
		_textEarnAmount = this.transform.FindChild("RowEarn/TextEarnAmount").GetComponent<Text>();
		_textTotalAmount = this.transform.FindChild("RowTotal/TextTotalAmount").GetComponent<Text>();
	}

	void Start () {
	
	}

	void Update () {
	
	}
}
