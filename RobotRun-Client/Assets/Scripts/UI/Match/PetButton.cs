using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PetButton : MonoBehaviour {
	Image _imageMask;
	Image _imagePet;
	Rect _rectImageFill;
	float _decreaseTime = 0;
	float _maxDecreaseTime = 0;

	int _maxValue = 0;

	int _value = 0;
	public int value{
		get{
			return _value;
		}
		set{
			_value = Mathf.Clamp(value, 0, _maxValue);
			UpdateUI();
		}
	}

	float percentage = 0.0f;

	public bool isFull{
		get{
			return _value == _maxValue;
		}
	}

	PetData _petData = null;
	public PetData petData{
		get{
			return _petData;
		}
		set{
			_petData = value;
			if(_petData != null){
				_imagePet.sprite = _petData.spriteIcon;
				_maxValue = _petData.requiredMilk;
				UpdateUI();
			}
		}
	}

	void Awake(){
		_imageMask = this.transform.FindChild("ImageMask").GetComponent<Image>();
		_imagePet = this.transform.FindChild("ImagePet").GetComponent<Image>();
		_rectImageFill = _imageMask.transform.FindChild("ImageFill").GetComponent<RectTransform>().rect;
		Debug.Log(_rectImageFill.height);
	}

	void Start () {
		
	}

	void Update () {
		if(_decreaseTime > 0){
			_decreaseTime -= Time.deltaTime;
			if(_decreaseTime <= 0){
				_decreaseTime = 0;
				percentage = 0;
			}else{
				percentage = _decreaseTime / _maxDecreaseTime;
			}
			_imageMask.rectTransform.sizeDelta = new Vector2(_imageMask.rectTransform.sizeDelta.x, _rectImageFill.height * percentage);
		}
	}

	void UpdateUI(){
		if(_maxValue == 0){
			percentage = 0;
		}else{
			percentage = (float)_value/(float)_maxValue;
		}
		_imageMask.rectTransform.sizeDelta = new Vector2(_imageMask.rectTransform.sizeDelta.x, _rectImageFill.height * percentage);
	}

	public void StartDecrease(){
		_maxDecreaseTime = _decreaseTime = _petData.duration;
	}
}
