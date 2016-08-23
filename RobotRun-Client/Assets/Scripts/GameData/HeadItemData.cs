using UnityEngine;
using System.Collections;
using SimpleJSON;

public class HeadItemData : IconData {
	public override string prefix{
		get{
			return "H";
		}
	}

	public override string iconPath{
		get{
			return base.iconPath + "HeadItem/"+prefix + id.ToString("000");
		}
	}

	public string skinName {
		get {
			return prefix + id.ToString ("000");
		}
	}

	public string name;
	public string feature;
	public int speed;
	public int jump;
	public int luck;
	public int recovery;
	public int rewardCoin;
	public int priceCoin;
	public int priceRealCoin;


	public HeadItemData (JSONNode jsonNode):base(jsonNode)
	{ 
		name = jsonNode ["name"];
		speed = jsonNode ["speed"].AsInt;
		jump = jsonNode ["jump"].AsInt;
		luck = jsonNode ["luck"].AsInt;
		recovery = jsonNode ["recovery"].AsInt;
		rewardCoin = jsonNode ["reward_coin"].AsInt;
		priceCoin = jsonNode ["price_coin"].AsInt;
		priceRealCoin = jsonNode ["price_real_coin"].AsInt;
	}
}
