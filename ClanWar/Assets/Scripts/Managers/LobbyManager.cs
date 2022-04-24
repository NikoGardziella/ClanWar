using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;


public class LobbyManager : MonoBehaviour, IPunCallbacks
{
	[SerializeField]
	bool firstLoad = false;
	[SerializeField]
	List<AccountStats> players = new List<AccountStats>();

	void Start()
	{

	}

	private void Update()
	{
		if (!firstLoad)
		{
			Debug.Log(PhotonNetwork.connectionState);
			Debug.Log(PhotonNetwork.insideLobby);
			if (PhotonNetwork.connectionState == ConnectionState.Connected && PhotonNetwork.insideLobby)
			{
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
			}
		}
	}



	void OnJoinLobby()
	{

	}

	void OnPhotonJoinRoomFailed()
	{
		Debug.Log("OnPhotonRoomFailed");
		PhotonNetwork.CreateRoom(GameConstants.ROOM_ONE);
	}
	
	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		firstLoad = true;
		GameObject go = PhotonNetwork.Instantiate(GameConstants.PHOTON_PLAYER, Vector3.zero, Quaternion.identity, 0);
		AccountStats stats = go.GetComponent<AccountStats>();
		go.name = AccountInfo.Instance.Info.AccountInfo.PlayFabId;
		stats.me = true;
		stats.trophies = AccountInfo.Instance.Info.PlayerStatistics[0].Value;

		UserDataRecord record = new UserDataRecord();
		if (AccountInfo.Instance.Info.UserData.TryGetValue(GameConstants.DATA_LEVEL, out record))
		{
			stats.level = int.Parse(record.Value);
		}

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
		throw new System.NotImplementedException();
	}

	public void OnPhotonJoinRoomFailed(object[] codeAndMsg)
	{
		throw new System.NotImplementedException();
	}

	public void OnCreatedRoom()
	{
		throw new System.NotImplementedException();
	}

	public void OnJoinedLobby()
	{
		throw new System.NotImplementedException();
	}

	public void OnLeftLobby()
	{
		throw new System.NotImplementedException();
	}

	public void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		throw new System.NotImplementedException();
	}

	public void OnConnectionFail(DisconnectCause cause)
	{
		throw new System.NotImplementedException();
	}

	public void OnDisconnectedFromPhoton()
	{
		throw new System.NotImplementedException();
	}

	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		throw new System.NotImplementedException();
	}

	public void OnReceivedRoomListUpdate()
	{
		throw new System.NotImplementedException();
	}

	/*public void IPunCallbacks.OnJoinedRoom()
	{

		throw new System.NotImplementedException();
	} */

	public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		throw new System.NotImplementedException();
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
		throw new System.NotImplementedException();
	}

	public void OnPhotonMaxCccuReached()
	{
		throw new System.NotImplementedException();
	}

	public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
	{
		throw new System.NotImplementedException();
	}

	public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		throw new System.NotImplementedException();
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
		throw new System.NotImplementedException();
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
