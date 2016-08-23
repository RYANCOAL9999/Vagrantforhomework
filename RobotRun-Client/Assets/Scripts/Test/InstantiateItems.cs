using UnityEngine;
using System.Collections;

public class InstantiateItems : Photon.PunBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnJoinedRoom()
	{
		if(PhotonNetwork.isMasterClient){
//			PhotonNetwork.Instantiate("SpringPlatform", new Vector3(13,0.5f, 0), Quaternion.identity, 0);
//			PhotonNetwork.Instantiate("Brick", new Vector3(18,0.12f, 0), Quaternion.identity, 0);
//			PhotonNetwork.Instantiate("Brick", new Vector3(23,0.12f, 0), Quaternion.identity, 0);
//			PhotonNetwork.Instantiate("Brick", new Vector3(28,0.12f, 0), Quaternion.identity, 0);
//			PhotonNetwork.Instantiate("Brick", new Vector3(33,0.12f, 0), Quaternion.identity, 0);
//			PhotonNetwork.Instantiate("Hurt Brick", new Vector3(36,0.12f, 0), Quaternion.identity, 0);
		}
	}
}
