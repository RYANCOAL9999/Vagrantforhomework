using UnityEngine;
using System.Collections;

public class SpinePreview : MonoBehaviour {
	Character _character;
	// Use this for initialization
	void Start () {
		object[] data = new object[6];
		data[0] = "Spaceman";//Character Skeleton File Name
		data[1] = "Spaceman";//Character Skeleton File Name
		data[2] = "cow";//Pet Skeleton File Name
		data[3] = Random.Range(0,2)==0?"spaceman":"BB";//Character Skin Name
		data[4] = "H001";//Random.Range(0,2)==0?"char01":"char02";//Character Skin Name
		data[5] = "";//Pet Skin Name
		_character = this.GetComponent<Character>();

		CharacterData characterData = new CharacterData(data);
		_character.characterData = characterData;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
