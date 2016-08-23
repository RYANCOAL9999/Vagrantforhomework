using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class GameDataManager : Singleton<GameDataManager> {
	public UserData userData;

	public List<ClothData> listClothData = new List<ClothData>();
	public List<HeadItemData> listHeadItemData = new List<HeadItemData>();
	public List<PetData> listPetData = new List<PetData>();
	public List<SkillData> listSkillData = new List<SkillData>();

	List<List<float>> _listDrawChance = new List<List<float>>();

	Dictionary<int, PetData> _dictPetData = new Dictionary<int, PetData>();
	Dictionary<int, ClothData> _dictClothData = new Dictionary<int, ClothData>();
	Dictionary<int, HeadItemData> _dictHeadItemData = new Dictionary<int, HeadItemData>();
	Dictionary<int, SkillData> _dictSkillData = new Dictionary<int, SkillData>();

	//For Multiplay
	public string mapName = null;
	public Dictionary<int, int> dictPlayerIDSlot = new Dictionary<int, int>();
	public Dictionary<int, MatchCharacterController> dictOwnerIDMatchCharacterController = new Dictionary<int, MatchCharacterController>();

	void Awake ()
	{
		TextAsset tempData = Resources.Load<TextAsset> ("TempData/TempData");
		Debug.Log("tempData: "+tempData);
		JSONNode jsonNode = JSON.Parse (tempData.text);
		JSONNode data = jsonNode ["data"];
		userData = new UserData (data ["user"]);
		foreach (JSONNode node in data["cloth_arr"].AsArray) {
			ClothData clothData = new ClothData (node);
			listClothData.Add (clothData);
			_dictClothData.Add(clothData.id, clothData);
		}
		foreach (JSONNode node in data["pet_arr"].AsArray) {
			PetData petData = new PetData (node);
			listPetData.Add (petData);
			_dictPetData.Add(petData.id, petData);
		}
		foreach (JSONNode node in data["head_item_arr"].AsArray) {
			HeadItemData headItemData = new HeadItemData (node);
			listHeadItemData.Add (headItemData);
			_dictHeadItemData.Add(headItemData.id, headItemData);
		}
		foreach (JSONNode node in data["skill_arr"].AsArray) {
			SkillData skillData = new SkillData (node);
			listSkillData.Add (skillData);
			_dictSkillData.Add(skillData.id, skillData);
		}

		foreach (JSONNode node in data["skill_chance"].AsArray) {
			List<float> listChance = new List<float>();
			foreach (JSONNode nodeChance in node.AsArray) {
				listChance.Add(nodeChance.AsFloat);
			}
			_listDrawChance.Add(listChance);
		}
	}

	public void UpdateClothData(JSONNode data){
		listClothData.Clear();
		_dictClothData.Clear();
		foreach (JSONNode node in data.AsArray) {
			ClothData clothData = new ClothData (node);
			listClothData.Add (clothData);
			_dictClothData.Add(clothData.id, clothData);
		}
	}

	public void UpdateHeadItemData(JSONNode data){
		listHeadItemData.Clear();
		_dictHeadItemData.Clear();
		foreach (JSONNode node in data.AsArray) {
			HeadItemData headItemData = new HeadItemData (node);
			listHeadItemData.Add (headItemData);
			_dictHeadItemData.Add(headItemData.id, headItemData);
		}
	}

	public void UpdatePetData(JSONNode data){
		listPetData.Clear();
		_dictPetData.Clear();
		foreach (JSONNode node in data.AsArray) {
			PetData petData = new PetData (node);
			listPetData.Add (petData);
			_dictPetData.Add(petData.id, petData);
		}
	}

	public void UpdateSkillData(JSONNode data){
		listSkillData.Clear();
		_dictSkillData.Clear();
		foreach (JSONNode node in data.AsArray) {
			SkillData skillData = new SkillData (node);
			listSkillData.Add (skillData);
			_dictSkillData.Add(skillData.id, skillData);
		}
	}

	public ClothData GetClothData(int id){
		if(_dictClothData.ContainsKey(id)){
			return _dictClothData[id];
		}
		return null;
	}

	public HeadItemData GetHeadItemData(int id){
		if(_dictHeadItemData.ContainsKey(id)){
			return _dictHeadItemData[id];
		}
		return null;
	}

	public PetData GetPetData(int id){
		if(_dictPetData.ContainsKey(id)){
			return _dictPetData[id];
		}
		return null;
	}

	public SkillData GetSkillData(int id){
		if(_dictSkillData.ContainsKey(id)){
			return _dictSkillData[id];
		}
		return null;
	}

	public int GetRandSkillIndex(int rank){
		float rand = Random.Range(0.0f, 1.0f);
		float acc = 0;
		int idx = rank - 1;
		for(int i = 0; i < _listDrawChance[idx].Count; i++){
			acc += _listDrawChance[idx][i];
			if(rand <= acc){
				return i;
			}
		}
		Debug.LogError("Error Occur!!!");
		return -1;
	}

	public int GetRankRewardCoin(int rank){
		switch(rank){
		case 1:
			return 50;
		case 2:
			return 30;
		case 3:
			return 10;
		}
		return 0;
	}
}
