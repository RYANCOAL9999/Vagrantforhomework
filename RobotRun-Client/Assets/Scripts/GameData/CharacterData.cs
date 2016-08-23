using UnityEngine;
using SimpleJSON;
using Spine.Unity;

public class CharacterData : BaseData {
	public const string prefix = "C";

	public string name;
	public string attackMethod;
	public float speed;
	public float jump;
	public float doubleJump;
	public int luck;
	public int recovery;
	public int rewardCoin;
	public int lv;
	public bool unlocked = true;

	public int equipedClothID;
	public int equipedHeadID;
	public int equipedPetID;

	private string spineFileName{
		get{
			return prefix + id.ToString ("000");
		}
	}

	public SkeletonDataAsset headSkeletonDataAsset{
		get{
			return Resources.Load("Spine/character/"+this.spineFileName+"/"+this.spineFileName+"Head_SkeletonData") as SkeletonDataAsset;
		}
	}

	public SkeletonDataAsset bodySkeletonDataAsset{
		get{
			return Resources.Load("Spine/character/"+this.spineFileName+"/"+this.spineFileName+"Body_SkeletonData") as SkeletonDataAsset;
		}
	}

	public SkeletonDataAsset petSkeletonDataAsset{
		get{
			if(equipedPetData != null){
				return equipedPetData.skeletonDataAsset;
			}
			return null;
		}
	}

//	private string petSpineFileName{
//		get{
//			return GameDataManager.Instance.GetPetData (equipedPetID).spineFileName;
//		}
//	}

	public string bodySpineSkinName{
		get{
			if(equipedClothData != null){
				return equipedClothData.skinName;
			}
			return "";
		}
	}

	public string headSpineSkinName{
		get{
			if(equipedHeadItemData != null){
				return equipedHeadItemData.skinName;
			}
			return "H000";
		}
	}

	public string petSpineSkinName{
		get{
			if(equipedPetData != null){
				return equipedPetData.skinName;
			}
			return "";
		}
	}

	public ClothData equipedClothData{
		get{
			return GameDataManager.Instance.GetClothData(equipedClothID);
		}
	}

	public HeadItemData equipedHeadItemData{
		get{
			return GameDataManager.Instance.GetHeadItemData(equipedHeadID);
		}
	}

	public PetData equipedPetData{
		get{
			return GameDataManager.Instance.GetPetData(equipedPetID);
		}
	}

	public float matchSpeed{
		get{
			return totalSpeed * 0.25f;
		}
	}

	public float matchPetSpeed{
		get{
			return (totalSpeed + (equipedPetData == null ? 0 : equipedPetData.speed)) * 0.25f;
		}
	}

	public float matchJump{
		get{
			return totalJump * 0.025f;
		}
	}

	public float matchDoubleJump{
		get{
			return totalDoubleJump * 0.025f;
		}
	}

	public float matchRecovery{
		get{
			return 1;
		}
	}

	public float totalSpeed{
		get{
			return speed + (equipedClothData == null ? 0 : equipedClothData.speed) + (equipedHeadItemData == null ? 0 : equipedHeadItemData.speed);
		}
	}

	public float totalPetSpeed{
		get{
			return totalSpeed + (equipedPetData == null ? 0 : equipedPetData.speed);
		}
	}

	public float totalJump{
		get{
			return jump + (equipedClothData == null ? 0 : equipedClothData.jump) + (equipedHeadItemData == null ? 0 : equipedHeadItemData.jump);
		}
	}

	public float totalDoubleJump{
		get{
			return doubleJump + (equipedClothData == null ? 0 : equipedClothData.jump) + (equipedHeadItemData == null ? 0 : equipedHeadItemData.jump);
		}
	}

	public float totalRecovery{
		get{
			return recovery + (equipedClothData == null ? 0 : equipedClothData.recovery) + (equipedHeadItemData == null ? 0 : equipedHeadItemData.recovery);
		}
	}

	public CharacterData(CharacterData characterData){
		id = characterData.id;
		name = characterData.name;
		attackMethod = characterData.attackMethod;
		speed = characterData.speed;
		jump = characterData.jump;
		doubleJump = characterData.doubleJump;
		luck = characterData.luck;
		recovery = characterData.recovery;
		rewardCoin = characterData.rewardCoin;
		lv = characterData.lv;
		unlocked = characterData.unlocked;
		equipedClothID = characterData.equipedClothID;
		equipedHeadID = characterData.equipedHeadID;
		equipedPetID = characterData.equipedPetID;
		//toDo
	}

	public CharacterData(object[] data){
//		bodyName = data[0].ToString();
//		headName = data[1].ToString();
//		petName = data[2].ToString();
		//bodySkinName = data[3].ToString();
//		headSkinName = data[3].ToString();
//		petSkinName = data[4].ToString();
	}

	public CharacterData (JSONNode jsonNode):base(jsonNode)
	{ 
		name = jsonNode ["name"];
		attackMethod = jsonNode ["attack_method"];
		speed = jsonNode ["speed"].AsFloat;
		jump = jsonNode ["jump"].AsFloat;
		doubleJump = jsonNode ["double_jump"].AsFloat;
		luck = jsonNode ["luck"].AsInt;
		recovery = jsonNode ["recovery"].AsInt;
		rewardCoin = jsonNode ["reward_coin"].AsInt;
		lv = jsonNode ["lv"].AsInt;
		unlocked = jsonNode ["unlocked"].AsBool;
		equipedClothID = jsonNode["equiped_cloth_id"].AsInt;
		equipedHeadID = jsonNode["equiped_head_item_id"].AsInt;
		equipedPetID = jsonNode["equiped_pet_id"].AsInt;
	}

	public override JSONNode GetJSONNode ()
	{
		JSONNode jsonNode = base.GetJSONNode();
		jsonNode ["name"] = name;
		jsonNode ["attack_method"] = attackMethod;
		jsonNode ["speed"].AsFloat = speed;
		jsonNode ["jump"].AsFloat = jump;
		jsonNode ["double_jump"].AsFloat = doubleJump;
		jsonNode ["luck"].AsInt = luck;
		jsonNode ["recovery"].AsInt = recovery;
		jsonNode ["reward_coin"].AsInt = rewardCoin;
		jsonNode ["lv"].AsInt = lv;
		jsonNode ["unlocked"].AsBool = unlocked;
		jsonNode ["equiped_cloth_id"].AsInt = equipedClothID;
		jsonNode ["equiped_head_item_id"].AsInt = equipedHeadID;
		jsonNode ["equiped_pet_id"].AsInt = equipedPetID;
		return jsonNode;
	}
}
