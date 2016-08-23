using UnityEngine;
using System.Collections;
using SimpleJSON;

public class ClothData : IconData {

	public override string prefix{
		get{
			return "E";
		}
	}

	public override string iconPath{
		get{
			return base.iconPath + "Cloth/"+prefix + id.ToString("000");
		}
	}

	public string skinName {
		get {
			return prefix + id.ToString ("000");
		}
	}

	public string name;
	public int speed;
	public int jump;
	public int luck;
	public int recovery;
	public int rewardCoin;
	public int priceCoin;
	public int priceRealCoin;

	public int skill1ID;
	public int skill2ID;
	public int skill3ID;
	public int skill4ID;
	public int skill5ID;
	public int skill6ID;

	public ClothData (JSONNode jsonNode):base(jsonNode)
	{ 
		name = jsonNode ["name"];
		speed = jsonNode ["speed"].AsInt;
		jump = jsonNode ["jump"].AsInt;
		luck = jsonNode ["luck"].AsInt;
		recovery = jsonNode ["recovery"].AsInt;
		rewardCoin = jsonNode ["reward_coin"].AsInt;
		priceCoin = jsonNode ["price_coin"].AsInt;
		priceRealCoin = jsonNode ["price_real_coin"].AsInt;
		skill1ID = jsonNode ["skill1_id"].AsInt;
		skill2ID = jsonNode ["skill2_id"].AsInt;
		skill3ID = jsonNode ["skill3_id"].AsInt;
		skill4ID = jsonNode ["skill4_id"].AsInt;
		skill5ID = jsonNode ["skill5_id"].AsInt;
		skill6ID = jsonNode ["skill6_id"].AsInt;
	}

	public SkillData GetSkillData(int index){
		switch(index){
		case 0:
			return GameDataManager.Instance.GetSkillData(skill1ID);
		case 1:
			return GameDataManager.Instance.GetSkillData(skill2ID);
		case 2:
			return GameDataManager.Instance.GetSkillData(skill3ID);
		case 3:
			return GameDataManager.Instance.GetSkillData(skill4ID);
		case 4:
			return GameDataManager.Instance.GetSkillData(skill5ID);
		case 5:
			return GameDataManager.Instance.GetSkillData(skill6ID);
		}
		return null;
	}
}
