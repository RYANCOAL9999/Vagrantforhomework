using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

public class UserData : BaseData {
	
	public string name;
	public int coin;
	public int realCoin;
	public int lv;	
	public string language;
	public int selectedCharacterIndex;
	public List<CharacterData> listCharacterData = new List<CharacterData>();
	public List<int> listInventoryClothID = new List<int>();
	public List<int> listInventoryPetID = new List<int>();
	public List<int> listInventoryHeadItemID = new List<int>();

	public CharacterData selectedCharacter{
		get{
			return listCharacterData[selectedCharacterIndex];
		}
	}

	public UserData (JSONNode jsonNode):base(jsonNode)
	{ 
		name = jsonNode ["name"];
		coin = jsonNode ["coin"].AsInt;
		realCoin = jsonNode ["real_coin"].AsInt;
		lv = jsonNode ["lv"].AsInt;
		language = jsonNode ["language"];
		selectedCharacterIndex = jsonNode ["selected_character_id"].AsInt;
		foreach (JSONNode data in jsonNode["character_arr"].AsArray) {
			CharacterData characterData = new CharacterData (data);
			listCharacterData.Add (characterData);
		}
		foreach (JSONNode data in jsonNode["inventory_cloth_arr"].AsArray) {
			listInventoryClothID.Add (data.AsInt);
		}
		foreach (JSONNode data in jsonNode["inventory_pet_arr"].AsArray) {
			listInventoryPetID.Add (data.AsInt);
		}
		foreach (JSONNode data in jsonNode["inventory_head_item_arr"].AsArray) {
			listInventoryHeadItemID.Add (data.AsInt);
		}
	}

	public string GetMatchJSONString (){
		JSONNode jsonNode = JSON.Parse ("{}");
		jsonNode["name"] = name;
		jsonNode["lv"].AsInt = lv;
		jsonNode["character_arr"][0] = this.selectedCharacter.GetJSONNode();
		return jsonNode.ToString();
	}
}
