using UnityEngine;

public class MyRandomMatchmaker : Photon.PunBehaviour
{
	private PhotonView myPhotonView;

	// Use this for initialization
	public void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	public override void OnJoinedLobby()
	{
		Debug.Log("JoinRandom");
		PhotonNetwork.JoinRandomRoom();
	}

	public override void OnConnectedToMaster()
	{
		// when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
		PhotonNetwork.JoinRandomRoom();
	}

	public void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom(null);
	}

	public override void OnJoinedRoom()
	{
		object[] data = new object[4];
		data[0] = "Spaceman";//Character Skeleton File Name
		data[1] = "cow";//Pet Skeleton File Name
		data[2] = Random.Range(0,2)==0?"spaceman":"BB";//Character Skin Name
		data[3] = "";//Pet Skin Name

		GameObject monster = PhotonNetwork.Instantiate("Prefabs/Match/Character", Vector3.zero, Quaternion.identity, 0, data);
		//        monster.GetComponent<myThirdPersonController>().isControllable = true;
		myPhotonView = monster.GetComponent<PhotonView>();
	}

	public void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
}
