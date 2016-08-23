using UnityEngine;
using System.Collections;
using SimpleJSON;

public class BaseData
{
	public int id = -1;
	//		virtual public int uniqueID{}

	public BaseData ()
	{

	}

	public BaseData (JSONNode jsonNode)
	{
		id = jsonNode ["id"].AsInt;
	}

	public virtual JSONNode GetJSONNode ()
	{
		JSONNode jsonNode = JSON.Parse ("{}");
		jsonNode ["id"].AsInt = id;
		return jsonNode;
	}
}

