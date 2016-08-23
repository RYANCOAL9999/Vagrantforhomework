using UnityEngine;
using System.Collections;
using SimpleJSON;
using Koocell.Network;

public class RobotRunRequest : Request<RobotRunRequest>{

	public override void UpdateGameData (JSONNode jsonNode)
	{
		if (jsonNode ["user"] != null) {
			GameDataManager.Instance.userData = new UserData (jsonNode ["user"]);
		}

		if (jsonNode ["head_item_arr"] != null) {
			GameDataManager.Instance.UpdateHeadItemData(jsonNode ["head_item_arr"]);
		}

		if (jsonNode ["cloth_arr"] != null) {
			GameDataManager.Instance.UpdateClothData(jsonNode ["cloth_arr"]);
		}

		if (jsonNode ["pet_arr"] != null) {
			GameDataManager.Instance.UpdatePetData(jsonNode ["pet_arr"]);
		}
		if (jsonNode ["skill_arr"] != null) {
			GameDataManager.Instance.UpdateSkillData(jsonNode ["skill_arr"]);
		}
	}
}
