using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Cameras;
using DG.Tweening;

public class Match : Photon.PunBehaviour {

	public string mapSceneName;
	public RectTransform rtCountDown;
	public Image imageCover;
	public Text textPing;
	public Text textMapName;
	public MatchProgressBar matchProgressBar;
	public Image imageSkillBG;
	public Button buttonSkill;
	public PetButton buttonPet;
	public RectTransform panelEnd;
	public Image imageArrow;

	//Skill
	public RectTransform rtSkill32;

	private PhotonView _photonView;
	private MatchCharacterController _myMatchCharacterController;
	private List<MatchCharacterController> _listAllMatchCharacterController = new List<MatchCharacterController>();
	private List<PhotonPlayer> _listFinalRank = new List<PhotonPlayer>();
	private List<UIMatchResultRank> _listUIRank = new List<UIMatchResultRank>();

	UnityStandardAssets.Cameras.FreeLookCam _camera;

	Transform _tranStart;
	Transform _tranEnd;
	Text _textRewardCoin;
	Text _textTotalCoin;

	int _readyCount;
	int _playerNumber = 2;

	bool _gameStart;
	float _gameTime;

	void Awake (){
		_textRewardCoin = panelEnd.FindChild("UIRankContainer/Reward/RowEarn/TextEarnAmount").GetComponent<Text>();
		_textTotalCoin = panelEnd.FindChild("UIRankContainer/Reward/RowTotal/TextTotalAmount").GetComponent<Text>();
		_camera = GameObject.Find ("FreeLookCameraRig").GetComponent<UnityStandardAssets.Cameras.FreeLookCam> ();
	}

	void Start () {
		PhotonNetwork.playerName = GameDataManager.Instance.userData.name;
		_readyCount = 0;
		_gameStart = false;
		_gameTime = 0;
		if(PhotonNetwork.connectedAndReady){
			#if UNITY_5_4_OR_NEWER
			SceneManager.sceneLoaded += OnSceneLoaded;
			#endif
			if(GameDataManager.Instance.mapName != null){
				mapSceneName = GameDataManager.Instance.mapName;
			}
			SceneManager.LoadScene(mapSceneName,LoadSceneMode.Additive);
			textMapName.text = mapSceneName;
		}else{
			#if UNITY_EDITOR
			PhotonNetwork.ConnectUsingSettings("0.1");
			#endif
		}
		rtCountDown.gameObject.SetActive(false);
		rtCountDown.GetComponent<AnimationCallback>().eventCallback = OnCountDownAnimationEvent;

		buttonSkill.gameObject.SetActive(false);
		buttonPet.value = 0;
		panelEnd.gameObject.SetActive(false);

		foreach(UIMatchResultRank uiRank in panelEnd.GetComponentsInChildren<UIMatchResultRank>()){
			_listUIRank.Add(uiRank);
			uiRank.Hide();
		}

		rtSkill32.gameObject.SetActive(false);
#if UNITY_EDITOR
		imageCover.DOFade(0,0.0f);
#endif
	}
	
	// Update is called once per frame
	void Update () {
		if(_gameStart){
			_gameTime += Time.deltaTime;
		}
		int ping = PhotonNetwork.GetPing();
		textPing.text = ping.ToString();
		if(ping > 100){
			textPing.color = Color.red;
		}else if(ping > 50){
			textPing.color = Color.yellow;
		}else{
			textPing.color = Color.green;
		}
		if(Input.GetKeyUp(KeyCode.Q)){
			PhotonNetwork.Disconnect();
			SceneManager.LoadScene("Ready", LoadSceneMode.Single);
		}else if(Input.GetKeyUp(KeyCode.X)){
			StartMatch();
		}
	}

	void InitCharacter(){
		CharacterData characterData = GameDataManager.Instance.userData.selectedCharacter;
		object[] data = new object[6];
		data[0] = characterData.GetJSONNode().ToString();//Character Body Skeleton File Name
//		data[1] = characterData.headSpineFileName;//Character Head Skeleton File Name
//		data[2] = characterData.petSpineFileName;//Pet Skeleton File Name
//		data[3] = characterData.bodySpineSkinName;//Character Body Skin Name
//		data[4] = characterData.headSpineSkinName;//Character Head Skin Name
		//		data[5] = characterData.petSpineSkinName;//Pet Skin Name
//		GameObject character = PhotonNetwork.Instantiate("Prefabs/Match/MatchCharacter", Vector3.zero, Quaternion.identity, 0, data);
		GameObject character = PhotonNetwork.Instantiate("Prefabs/Match/MatchCharacter", Vector3.zero, Quaternion.identity, 0, data);
		PhotonNetwork.Instantiate("Prefabs/Match/AIMatchCharacter", Vector3.zero, Quaternion.identity, 0, data);
//		PhotonNetwork.Instantiate("Prefabs/Match/AIMatchCharacter", Vector3.zero, Quaternion.identity, 0, data);
//		PhotonNetwork.Instantiate("Prefabs/Match/AIMatchCharacter", Vector3.zero, Quaternion.identity, 0, data);
		_photonView = character.GetComponent<PhotonView>();
		_camera.SetTarget(character.transform);
		_myMatchCharacterController = character.GetComponent<MatchCharacterController>();
		_myMatchCharacterController.callbackDrawSkill = OnDrawSkill;
		_myMatchCharacterController.callbackGetMilkBottle = OnGetMilkBottle;

		if(_myMatchCharacterController.matchCharacter.characterData.equipedPetData == null){
			buttonPet.gameObject.SetActive(false);
		}else{
			buttonPet.gameObject.SetActive(true);
			buttonPet.petData = _myMatchCharacterController.matchCharacter.characterData.equipedPetData;
		}
	}

	void ShowRankResult(int rank){
		if(rank < 1 || rank > 4){
			Debug.LogError("Error Occured!!!");
			return;
		}
	}

	//SceneManager Callbacks
	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		if(scene.name == mapSceneName){
			//remove any camera
			foreach(FreeLookCam freeLookCam in GameObject.FindObjectsOfType<FreeLookCam>()){
				if(freeLookCam.gameObject.scene == scene){
  					freeLookCam.gameObject.SetActive(false);
				}
			}
			InitCharacter();
			_tranStart = GameObject.Find("start").GetComponent<Transform>();
			_tranEnd = GameObject.Find("end").GetComponent<Transform>();
			matchProgressBar.SetStartEndTransform(_tranStart, _tranEnd);
			if(_tranStart){
				Vector2 rayOrigin = new Vector2(_tranStart.position.x, _tranStart.position.y);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, float.MaxValue, MatchConfig.layerMaskGround);
				if(hit){
					_photonView.transform.position = hit.point;
					foreach(ScrollBackground scrollBackground in GameObject.FindObjectsOfType<ScrollBackground>()){
						scrollBackground.SetCameraStartPos(_photonView.transform.position);
					}
				}
			}
			this.photonView.RPC ("PlayerReady", PhotonTargets.AllBufferedViaServer);
			#if UNITY_5_4_OR_NEWER
			SceneManager.sceneLoaded -= OnSceneLoaded;
			#endif
		}
	}

	void StartMatch(){
		_gameStart = true;
		rtCountDown.gameObject.SetActive(false);
		foreach(MatchCharacterController controller in _listAllMatchCharacterController){
			if(controller == _myMatchCharacterController || controller.GetType() == typeof(AIMatchCharacterController)){
				controller.StartMatch();
			}
		}
	}

	//Photon Callbacks
	[PunRPC]
	void PlayerReady (PhotonMessageInfo msgInfo)
	{
		_readyCount++;
		if(_readyCount == _playerNumber){
			imageCover.DOFade(0,0.1f).Play();
			rtCountDown.gameObject.SetActive(true);
		}
	}
		
	public override void OnDisconnectedFromPhoton()
	{
		PhotonNetwork.LeaveRoom();
		SceneManager.LoadScene("Ready", LoadSceneMode.Single);
	}

	public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{
//		PhotonNetwork.LeaveRoom();
//		SceneManager.LoadScene("Ready", LoadSceneMode.Single);
		_listAllMatchCharacterController.Remove(GameDataManager.Instance.dictOwnerIDMatchCharacterController[otherPlayer.ID]);
	}

	public void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	//UI Callback
	public void OnTriggerJump(){
		if(_myMatchCharacterController != null){
			_myMatchCharacterController.Jump();
		}
	}

	public void OnTriggerSkill(){
		if(_myMatchCharacterController.matchCharacter.holdingSkillData != null){
			if(_myMatchCharacterController.matchCharacter.holdingSkillData.id == 32){
				this.photonView.RPC ("OnTriggerSkill32", PhotonTargets.Others);
			}
			_myMatchCharacterController.Throw();
			buttonSkill.gameObject.SetActive(false);
		}
	}

	public void OnTriggerPet(){
		if(_myMatchCharacterController.canRide && _myMatchCharacterController != null){
			_myMatchCharacterController.Ride();
			buttonPet.value = 0;
			buttonPet.StartDecrease();
		}
	}

	public void OnLeaveBtnClick(){
		PhotonNetwork.Disconnect();
		SceneManager.LoadScene("Main", LoadSceneMode.Single);
	}

	//Match Character Controller Callback
	public void OnPhotonPlayerArriveGoal(PhotonPlayer player){
		_listFinalRank.Add(player);
		if(_myMatchCharacterController.photonView.owner == player){
			panelEnd.gameObject.SetActive(true);
			int rank = _listFinalRank.Count;
			int rewardCoin = GameDataManager.Instance.GetRankRewardCoin(rank);
			_textRewardCoin.text = rewardCoin.ToString();
			_textTotalCoin.text = GameDataManager.Instance.userData.coin + rewardCoin.ToString();
			GameDataManager.Instance.userData.coin += rewardCoin;//Temp should sync from server
			if(_listFinalRank.Count <= _listUIRank.Count){
				UIMatchResultRank uiRank = _listUIRank[_listFinalRank.Count-1];
				RectTransform rtUIRank = uiRank.GetComponent<RectTransform>();
				imageArrow.rectTransform.position = new Vector2(imageArrow.rectTransform.position.x, rtUIRank.position.y);
				imageArrow.gameObject.SetActive(true);
				buttonPet.gameObject.SetActive(false);
				buttonSkill.gameObject.SetActive(false);
				imageSkillBG.gameObject.SetActive(false);
			}
			if(_camera != null){
				_camera.enabled = false;
			}
		}
		if(panelEnd.gameObject.activeSelf){
			for(int i = 0; i < _listFinalRank.Count; i++){
				PhotonPlayer photonPlayer = _listFinalRank[i];
				if(i < _listUIRank.Count){
					UIMatchResultRank uiRank = _listUIRank[i];
					if(uiRank.isHide){
						uiRank.playerName = photonPlayer.name;
						uiRank.Show();
						RectTransform rtUIRank = uiRank.GetComponent<RectTransform>();
						uiRank.rtContent.position = new Vector2(panelEnd.rect.width + uiRank.rtContent.sizeDelta.x, uiRank.rtContent.position.y);
						uiRank.rtContent.DOAnchorPosX(0, 0.5f);

						if(photonPlayer == PhotonNetwork.player){
							imageArrow.GetComponent<RectTransform>().SetParent(uiRank.rtContent,false);
						}
					}
				}

			}
		}
	}

//	public void OnMatchCharacterControllerArriveGoal(MatchCharacterController matchCharacterController){
//		if(this._myMatchCharacterController == matchCharacterController){
//			if(_camera != null){
//				_camera.enabled = false;
//			}
//		}
//	}

	public void OnMatchCharacterControllerTriggerDropArea(MatchCharacterController matchCharacterController){
		if(this._myMatchCharacterController == matchCharacterController){
			if(_camera != null){
				_camera.enabled = false;
			}
		}
	}

	public void OnMatchCharacterControllerReborn(MatchCharacterController matchCharacterController){
		if(this._myMatchCharacterController == matchCharacterController){
			if(_camera != null){
				_camera.enabled = true;
			}
		}
	}

	public void AddMatchCharacterController(MatchCharacterController controller){
		_listAllMatchCharacterController.Add(controller);
	}

	public int GetCurrentRank(MatchCharacterController controller){
		int rank = 1;
		foreach(MatchCharacterController c in _listAllMatchCharacterController){
			if(c != null && controller != c && c.transform.position.x > controller.transform.position.x){
				rank++;
			}
		}
		return rank;
	}

	[PunRPC]
	void OnTriggerSkill32(){
		rtSkill32.gameObject.SetActive(true);
		rtSkill32.GetComponent<AnimationCallback>().eventCallback = OnSkill32AniFinish;
	}

	void OnSkill32AniFinish(string para){
		if(para == "finish"){
			rtSkill32.gameObject.SetActive(false);
		}
	}

	public void OnResultNextButtonClick(){
		PhotonNetwork.LeaveRoom();
		SceneManager.LoadScene("Ready", LoadSceneMode.Single);
	}

	//MatchCharacterController Callback
	public void OnDrawSkill(SkillData skillData){
		buttonSkill.gameObject.SetActive(true);
		Image buttonSkillImage = buttonSkill.transform.FindChild("ImageSkill").GetComponent<Image>();
		buttonSkillImage.sprite = skillData.spriteIcon;
		buttonSkillImage.SetNativeSize();
	}

	public void OnGetMilkBottle(int num){
//		buttonPet.value += 100;
		buttonPet.value += num;
	}

	//Animation Callbacks
	void OnCountDownAnimationEvent(string param){
		if(param == "finish"){
			StartMatch();
		}
	}

	//For Devel Use
	public override void OnConnectedToMaster()
	{
		// when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
		PhotonNetwork.JoinRandomRoom();
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		#if UNITY_5_4_OR_NEWER
		SceneManager.sceneLoaded += OnSceneLoaded;
		#endif
		SceneManager.LoadScene(mapSceneName,LoadSceneMode.Additive);
	}

	public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
	{
		PhotonNetwork.CreateRoom(null);
	}

	public override void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
	}
}
