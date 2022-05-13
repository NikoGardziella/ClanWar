using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;


public class LobbyManager : MonoBehaviour , IPunCallbacks
{

	private static  LobbyManager instance;

	public static LobbyManager Instance
	{
		get { return  instance; }
		set { instance = value; }
	}


	[SerializeField]
	bool firstLoad = false;
	[SerializeField]
	float currTime = 0;
	[SerializeField]
	AccountStats myPlayer;
	[SerializeField]
	List<AccountStats> players = new List<AccountStats>();


	public static List<AccountStats> Players
	{
		get { return Instance.players; }
		set { Instance.players = value; }
	}

	private void Awake()
	{
		if (instance != this)
			instance = this;
	}

	List<AccountStats> GetPlayers()
	{
		List<AccountStats> gotPlayers = new List<AccountStats>();

		GameObject[] gos = GameObject.FindGameObjectsWithTag(GameConstants.PHOTON_PLAYER);
		foreach (GameObject go in gos)
		{
			gotPlayers.Add(go.GetComponent<AccountStats>());
		}

		return gotPlayers;
	}

	private void Update()
	{
		if (!firstLoad)
		{

			
			if (PhotonNetwork.connectionState == ConnectionState.Connected && PhotonNetwork.insideLobby)
			{
				Debug.Log("connection state: " + PhotonNetwork.connectionState);
				RoomOptions roomOptions = new RoomOptions()
				{
					IsVisible = true,
					MaxPlayers = 20
				};

				PhotonNetwork.JoinOrCreateRoom(GameConstants.ROOM_ONE, roomOptions, TypedLobby.Default);
			}
			else if(PhotonNetwork.connectionState == ConnectionState.Connected)
			{
				PhotonNetwork.JoinLobby();
				//Debug.Log("is in lobby: " + PhotonNetwork.insideLobby);
			}
		}
		else
		{
			players = GetPlayers();
			if (myPlayer.looking)
			{
				if (currTime < GameConstants.LOOKING_TIMER)
				{
					currTime += Time.deltaTime;
					AccountStats acc = (GameFunctions.FoundPlayer(myPlayer.trophies, players.ToArray()));

					if (acc != null)
					{
						string roomName = "GameArea";
						//Debug.Log("roomName: " + roomName);
						acc.gameObject.GetComponent<PhotonView>().RPC("ChangeRoomName", PhotonTargets.All, roomName);
						myPlayer.levelName = roomName; // move hgher
						//levelManager.LoadLevel(GameConstants.GAME_SCENE);
					}
				}
				else
				{
					myPlayer.looking = false;
					currTime = 0;
				}
			}
		}
	}





	void OnPhotonJoinRoomFailed()
	{
		//Debug.Log("OnPhotonRoomFailed");
		PhotonNetwork.CreateRoom(GameConstants.ROOM_ONE);
	}
	
	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		firstLoad = true;
		GameObject go = PhotonNetwork.Instantiate(GameConstants.PHOTON_PLAYER, Vector3.zero, Quaternion.identity, 0);
		AccountStats stats = go.GetComponent<AccountStats>();
		stats.levelName = GameConstants.ROOM_ONE;
		Debug.Log("onjoined levelname: " + stats.levelName);
		go.name = AccountInfo.Instance.Info.AccountInfo.PlayFabId;
		stats.me = true;
		stats.trophies = AccountInfo.Instance.Info.PlayerStatistics[0].Value;

		UserDataRecord record = new UserDataRecord();
		if (AccountInfo.Instance.Info.UserData.TryGetValue(GameConstants.DATA_LEVEL, out record))
		{
			stats.level = int.Parse(record.Value);
		}

		myPlayer = stats;

		players.Add(stats);
	}

	public void OnConnectedToPhoton()
	{
		Debug.Log("OnConnectedToPhoton");
	}

	public void OnLeftRoom()
	{
		throw new System.NotImplementedException();
	}

	public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
	{
		throw new System.NotImplementedException();
	}

	public void OnPhotonCreateRoomFailed(object[] codeAndMsg)
	{
		Debug.Log("OnPhotonCreateRoomFailed");
		
	}

	public void OnPhotonJoinRoomFailed(object[] codeAndMsg)
	{
		throw new System.NotImplementedException();
	}

	public void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
	}

	public void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby");
		//OnJoinedRoom();
	}

	public void OnLeftLobby()
	{
		Debug.Log("OnLeftLobby");
	}

	public void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		Debug.Log("OnFailedToConnectToPhoton:" + cause);
		GetPlayers();
	}

	public void OnConnectionFail(DisconnectCause cause)
	{
		Debug.Log("OnConnectionFail");
	}

	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("OnDisconnectedFromPhoton");
	}

	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate:" + info);
	}

	public void OnReceivedRoomListUpdate()
	{
		Debug.Log("OnReceivedRoomListUpdate");
	}

	/*public void IPunCallbacks.OnJoinedRoom()
	{
		Debug.Log("IPunCallbacks. OnJoinedRoom"); /
	} */

	public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		Debug.Log("OnPhotonPlayerConnected:" + newPlayer);
	}

	public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{
		throw new System.NotImplementedException();
	}

	public void OnPhotonRandomJoinFailed(object[] codeAndMsg)
	{
		throw new System.NotImplementedException();
	}

	public void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster");
	}

	public void OnPhotonMaxCccuReached()
	{
		throw new System.NotImplementedException();
	}

	public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
	{
		Debug.Log("OnPhotonCustomRoomPropertiesChanged");
	}

	public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		Debug.Log("OnPhotonPlayerPropertiesChanged");
	}

	public void OnUpdatedFriendList()
	{
		throw new System.NotImplementedException();
	}

	public void OnCustomAuthenticationFailed(string debugMessage)
	{
		throw new System.NotImplementedException();
	}

	public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
	{
		throw new System.NotImplementedException();
	}

	public void OnWebRpcResponse(OperationResponse response)
	{
		throw new System.NotImplementedException();
	}

	public void OnOwnershipRequest(object[] viewAndPlayer)
	{
		throw new System.NotImplementedException();
	}

	public void OnLobbyStatisticsUpdate()
	{
		Debug.Log("OnLobbyStatisticsUpdate");
	}

	public void OnPhotonPlayerActivityChanged(PhotonPlayer otherPlayer)
	{
		throw new System.NotImplementedException();
	}

	public void OnOwnershipTransfered(object[] viewAndPlayers)
	{
		throw new System.NotImplementedException();
	} 
}
