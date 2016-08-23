using UnityEngine;
using SimpleJSON;
using Spine.Unity;

public class PetData : IconData {
	public const string prefix = "P";

	public override string iconPath{
		get{
			return base.iconPath + "Pet/"+prefix + id.ToString("000");
		}
	}

	public string skinName {
		get {
			return "";
		}
	}
	
	public string name;
	public string feature;
	public int duration;
	public float speed;
	public float attackDuration;
	public int requiredMilk;

	private string spineFileName {
		get {
			return prefix + id.ToString ("000");
		}
	}

	public SkeletonDataAsset skeletonDataAsset{
		get{
			return Resources.Load("Spine/pet/"+this.spineFileName+"/"+this.spineFileName+"_SkeletonData") as SkeletonDataAsset;
		}
	}

	public PetData (JSONNode jsonNode):base(jsonNode)
	{ 
		name = jsonNode ["name"];
		feature = jsonNode ["feature"];
		duration = jsonNode ["duration"].AsInt;
		speed = jsonNode ["speed"].AsFloat;
		attackDuration = jsonNode ["attack_duration"].AsFloat;
		requiredMilk = jsonNode ["required_milk"].AsInt;
	}
}
