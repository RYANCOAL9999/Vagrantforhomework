using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ShopIconButton : MonoBehaviour {
	public Action<ShopIconButton> callbackOnClick;

	protected Image _imageIcon;
	protected Text _textPrice;
	protected Image _imageEquiped;

	IconData _iconData;
	public IconData iconData{
		get{
			return _iconData;
		}
		set{
			_iconData = value;
			UpdateIconData ();
		}
	}

	void Awake(){
		_imageIcon = this.GetComponent<Image> ();
		_textPrice = this.transform.FindChild ("Price/TextPrice").GetComponent<Text> ();
		_imageEquiped = this.transform.FindChild ("ImageEquiped").GetComponent<Image> ();
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void UpdateIconData(){
		_imageIcon.sprite = _iconData.spriteIcon;
		if (_iconData.GetType () == typeof(ClothData)) {
			//_textPrice.text = ((ClothData)_iconData).priceCoin.ToString ();
			if (((ClothData)_iconData).priceCoin == -1) {
				_textPrice.text = ((ClothData)_iconData).priceRealCoin.ToString ();
			} else {
				_textPrice.text = ((ClothData)_iconData).priceCoin.ToString ();
			}
			_imageEquiped.gameObject.SetActive (iconData.id == GameDataManager.Instance.userData.selectedCharacter.equipedClothID);
			//Debug.Log (iconData.id == GameDataManager.Instance.userData.selectedCharacter.equipedClothID);
			if (_imageEquiped.gameObject.activeSelf) {
				_textPrice.transform.parent.gameObject.SetActive (false);
			}
		} else if (_iconData.GetType () == typeof(HeadItemData)) {
			_textPrice.text = ((HeadItemData)_iconData).priceCoin.ToString ();
			_imageEquiped.gameObject.SetActive (iconData.id == GameDataManager.Instance.userData.selectedCharacter.equipedHeadID);
			//Debug.Log (iconData.id == GameDataManager.Instance.userData.selectedCharacter.equipedHeadID);
			if (_imageEquiped.gameObject.activeSelf) {
				_textPrice.transform.parent.gameObject.SetActive (false);
			}
		}
//		_imageEquiped.gameObject.SetActive(GameDataManager.Instance.userData.listInventoryClothID.Contains (_iconData.id));
	}

	public void OnClicked(){
		if (callbackOnClick != null) {
			callbackOnClick (this);
		}
	}
}
