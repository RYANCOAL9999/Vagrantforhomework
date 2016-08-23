using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class ReadyScene : Photon.PunBehaviour {
	public const int kMaxPlayer = 2;

	public Image imageArrow;
	public Text textinfo;
	public List<ReadySceneCharacterContainer> listReadySceneCharacterContainer;

	Dictionary<int, int> _dictPlayerIDSlot = new Dictionary<int, int>();
	List<bool> _slotAvalible = new List<bool>();

	Coroutine _coroutinCountDown = null;

	void Awake(){
		for(int i = 0; i < kMaxPlayer; i++){
			_slotAvalible.Add(true);
		}
	}

	void Start () {
		GameDataManager.Instance.dictPlayerIDSlot.Clear();
		GameDataManager.Instance.dictOwnerIDMatchCharacterController.Clear();

		if(!PhotonNetwork.connectedAndReady){
			PhotonNetwork.ConnectUsingSettings("0.1");
		}
		foreach(ReadySceneCharacterContainer container in listReadySceneCharacterContainer){
			container.Hide();
		}
		imageArrow.gameObject.SetActive(false);
		ExitGames.Client.Photon.Hashtable data = new ExitGames.Client.Photon.Hashtable();
		data.Add("MatchCharacterData", GameDataManager.Instance.userData.GetMatchJSONString());
		PhotonNetwork.SetPlayerCustomProperties(data);
	}


	void Update () {
	
	}

	public override void OnConnectedToMaster()
	{
		// when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
		PhotonNetwork.JoinRandomRoom();
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
	}

	public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.maxPlayers = kMaxPlayer;
		PhotonNetwork.CreateRoom(null, roomOptions, null);
	}

	public override void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
		UpdateAllCharacterContainer();
	}

	public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
	{
		Debug.Log("OnMasterClientSwitched");
		UpdateAllCharacterContainer();

		if(PhotonNetwork.isMasterClient){
			if(PhotonNetwork.playerList.Length != kMaxPlayer && _coroutinCountDown != null){
				StopCountDown();
				this.photonView.RPC ("OnStopCountDown", PhotonTargets.Others);
			}
		}
	}

	public override void OnDisconnectedFromPhoton()
	{
		//		SceneManager.LoadScene("Main", LoadSceneMode.Single);
		SceneManager.LoadScene("Ready", LoadSceneMode.Single);
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		Debug.Log("OnPhotonPlayerConnected: "+newPlayer.ID);
		if(PhotonNetwork.isMasterClient){
			UpdateAllCharacterContainer();
		}
	}

	public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{
		Debug.Log("OnPhotonPlayerDisconnected: "+otherPlayer.ID);
		if(PhotonNetwork.isMasterClient){
			UpdateAllCharacterContainer();
			if(PhotonNetwork.playerList.Length != kMaxPlayer && _coroutinCountDown != null){
				StopCountDown();
				this.photonView.RPC ("OnStopCountDown", PhotonTargets.Others);
			}
		}
	}

	public void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	void StartCountDown(){
		if(_coroutinCountDown == null){
			_coroutinCountDown = StartCoroutine(CountDown());
		}else{
			Debug.LogError("Error Occur!!!");
		}
	}

	void StopCountDown(){
		if(_coroutinCountDown != null){
			StopCoroutine(_coroutinCountDown);
			_coroutinCountDown = null;
		}else{
			Debug.LogError("Error Occur!!!");
		}
	}

	IEnumerator CountDown(){
		textinfo.text = "3";
		yield return new WaitForSeconds(1);
		textinfo.text = "2";
		yield return new WaitForSeconds(1);
		textinfo.text = "1";
		yield return new WaitForSeconds(1);
		PhotonNetwork.room.open = false;
		GameDataManager.Instance.dictPlayerIDSlot = _dictPlayerIDSlot;
		SceneManager.LoadScene("Match", LoadSceneMode.Single);
	}

	void UpdateAllCharacterContainer(){
		imageArrow.gameObject.SetActive(true);
//		List<PhotonPlayer> listPhotonPlayer = new List<PhotonPlayer>(PhotonNetwork.playerList);
		Dictionary<int, int> newDict = new Dictionary<int, int>();

		for(int i = 0; i < _slotAvalible.Count; i++){
			_slotAvalible[i] = true;
		}

		foreach(PhotonPlayer photonPlayer in PhotonNetwork.playerList){
			if(_dictPlayerIDSlot.ContainsKey(photonPlayer.ID)){
				int slot = _dictPlayerIDSlot[photonPlayer.ID];
				newDict.Add(photonPlayer.ID, slot);
				_slotAvalible[slot] = false;
			}
		}
		_dictPlayerIDSlot = newDict;
		foreach(PhotonPlayer photonPlayer in PhotonNetwork.playerList){
			if(!_dictPlayerIDSlot.ContainsKey(photonPlayer.ID)){
				int availableSlot = GetAvailableSlot();
				_dictPlayerIDSlot.Add(photonPlayer.ID, availableSlot);
				_slotAvalible[availableSlot] = false;
			}
			int slotIndex = _dictPlayerIDSlot[photonPlayer.ID];
			ReadySceneCharacterContainer container = listReadySceneCharacterContainer[slotIndex];
			UserData userData = GetUserDataFromCustomProperty(photonPlayer);
			container.color = Character.GetPlayerColor(slotIndex);
//			userData.name = photonPlayer.ID.ToString();
			container.Show(userData);
			if(PhotonNetwork.player.ID == photonPlayer.ID){
				imageArrow.rectTransform.position = new Vector2(container.transform.position.x, imageArrow.rectTransform.position.y);
			}
		}
		for(int i = 0; i < _slotAvalible.Count; i++){
			if(_slotAvalible[i]){
				listReadySceneCharacterContainer[i].Hide();
			}
		}
		if(PhotonNetwork.isMasterClient){
			SyncSlotData();
			if(PhotonNetwork.playerList.Length == kMaxPlayer){
				StartCountDown();
				GameDataManager.Instance.mapName = GetRandomSceneName();
				this.photonView.RPC ("OnStartCountDown", PhotonTargets.Others, GameDataManager.Instance.mapName);
			}
		}
	}

	UserData GetUserDataFromCustomProperty(PhotonPlayer photonPlayer){
		return new UserData(JSON.Parse (photonPlayer.customProperties["MatchCharacterData"].ToString()));
	}

	int GetAvailableSlot(){
		for(int i = 0; i < _slotAvalible.Count; i++){
			if(_slotAvalible[i])return i;
		}	
		Debug.LogError("Error Occur!!!");
		return -1;
	}

	void SyncSlotData(){
		string jsonDictSlotIndex = JsonUtility.ToJson(new Serialization<int, int>(_dictPlayerIDSlot));
		string jsonSlotAvailable = JsonUtility.ToJson(new Serialization<bool>(_slotAvalible));
		this.photonView.RPC ("OnSyncSlotData", PhotonTargets.OthersBuffered, jsonDictSlotIndex, jsonSlotAvailable);
	}

	string GetRandomSceneName(){
		List<string> listMapName = new List<string>();
		listMapName.Add("Map1");
		listMapName.Add("Map2");
		listMapName.Add("Map3");
		listMapName.Add("Map5");
		listMapName.Add("Map6");
		listMapName.Add("Map7");
		return listMapName[Random.Range(0, listMapName.Count)];
	}

	//Photon Callback
	[PunRPC]
	void OnSyncSlotData (string jsonDictSlotIndex, string jsonSlotAvailable, PhotonMessageInfo msgInfo)
	{
		_dictPlayerIDSlot = JsonUtility.FromJson<Serialization<int, int>>(jsonDictSlotIndex).ToDictionary();
		_slotAvalible = JsonUtility.FromJson<Serialization<bool>>(jsonSlotAvailable).ToList();
		UpdateAllCharacterContainer();
	}

	[PunRPC]
	void OnStartCountDown (string mapName, PhotonMessageInfo msgInfo)
	{
		GameDataManager.Instance.mapName = mapName;
		StartCountDown();
	}

	[PunRPC]
	void OnStopCountDown (PhotonMessageInfo msgInfo)
	{
		StopCountDown();
	}

	//UI Callback
	public void OnBackButtonClick(){
		PhotonNetwork.Disconnect();
		SceneManager.LoadScene("Main");
	}
}
