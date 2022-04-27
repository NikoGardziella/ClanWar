using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AccountStats : MonoBehaviour
{
	public string levelName = GameConstants.ROOM_ONE;
	public bool me = false;
	public bool looking = false;
	public int trophies = 0; // Match Makng Rating
	public int level = 1;

	private void LateUpdate()
	{
		if(levelName != GameConstants.ROOM_ONE)
		{
			RoomOptions ro = new RoomOptions()
			{
				IsVisible = true,
				MaxPlayers = 2
			};
			PhotonNetwork.JoinOrCreateRoom(levelName, ro, TypedLobby.Default);
			//levelManager.LoadLevel(GameConstants.GAME_SCENE);
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// Me. Send my data to other players
			stream.SendNext(looking);
			stream.SendNext(trophies);
			stream.SendNext(level);
		}
		else
		{
			//Other network player. Receive data
			looking = (bool)stream.ReceiveNext();
			trophies = (int)stream.ReceiveNext();
			level = (int)stream.ReceiveNext();
		}
	}
}
