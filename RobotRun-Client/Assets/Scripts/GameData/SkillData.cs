   using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class SkillData : IconData {
	public enum SkillType{
		Projectile,
		Horizontal,
		GroundBoom,
		GroundDecreaseSpeed,
		GroundSheepBall,
		OthersInstantHit,
		OtherScreenAffect,
		Shoot,
		SelfIncreaseRunSpeed,
		SelfIncreaseFlySpeed,
		SelfHitOther,
		SelfDefend,
		SelfHorizontalFly
	}

	public override string prefix{
		get{
			return "SK";
		}
	}

	public override string iconPath{
		get{
			return base.iconPath + "Skill/"+prefix + id.ToString("000");
		}
	}

	public string name;
	public int requiredClothID;
	public SkillType skillType;
	public int value1;
	public int value2;
	public int value3;

	private Dictionary<string, int> _dictData = null;
//	public Dictionary<string, int> dictData{
//		get{
//			if(_dictData == null){
//				_dictData = new Dictionary<string, int>();
//			}
//		}
//	}

	public string prefabPath{
		get{
			return "Prefabs/Match/Item/"+prefix+id.ToString("000");
		}
	}

//	Sprite _spriteIcon = null;
//	public Sprite spriteIcon{
//		get{
//			if(_spriteIcon == null){
//				_spriteIcon = Resources.Load<Sprite>("Texture/Icon/Skill/"+prefix+id.ToString("000"));
//			}
//			return _spriteIcon;
//		}
//	}

	Object _prefab = null;
	public Object prefab{
		get{
			if(_prefab == null){
				_prefab = Resources.Load(prefabPath);
			}
			return _prefab;
		}
	}

	public SkillData (JSONNode jsonNode):base(jsonNode)
	{ 
		name = jsonNode ["name"];
		requiredClothID = jsonNode ["requiredClothID"].AsInt;
		skillType = (SkillType)jsonNode ["skillType"].AsInt;
		value1 = jsonNode ["value1"].AsInt;
		value2 = jsonNode ["value2"].AsInt;
		value3 = jsonNode ["value3"].AsInt;
	}
}
