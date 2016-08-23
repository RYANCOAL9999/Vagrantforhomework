using UnityEngine;
using System.Collections;
using SimpleJSON;

public class IconData : BaseData {

	Sprite _spriteIcon = null;
	public Sprite spriteIcon{
		get{
			if(_spriteIcon == null){
				_spriteIcon = Resources.Load<Sprite>(iconPath);
			}
			return _spriteIcon;
		}
	}

	public virtual string prefix{
		get{
			return "";
		}
	}

	public virtual string iconPath{
		get{
			return "Texture/Icon/";
		}
	}

	public IconData(JSONNode jsonNode):base(jsonNode){
		
	}
}
