using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AccountStats : MonoBehaviour
{

	public bool me = false;
	public bool looking = false;
	public int trophies = 0; // Match Makng Rating
	public int level = 1;
	public string levelName = "Room 1";

	private void Update()
	{
		//levelName = GameConstants.ROOM_ONE;
		if(levelName != "Room 1")
		{
			//Debug.Log("levelname: " + levelName);
			//levelManager.LoadLevel(GameConstants.GAME_SCENE);
		}
	}
	
	[PunRPC]
	public void ChangeRoomName(string roomName)
	{
		levelName = roomName;
		levelManager.LoadLevel(GameConstants.GAME_SCENE);
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			Debug.Log("Stream is writingh");
			// Me. Send my data to other players
			stream.SendNext(looking);
			stream.SendNext(trophies);
			stream.SendNext(level);
			stream.SendNext(levelName);
		}
		else
		{
			//Other network player. Receive data
			looking = (bool)stream.ReceiveNext();
			trophies = (int)stream.ReceiveNext();
			level = (int)stream.ReceiveNext();
			levelName = (string)stream.ReceiveNext();
		}
	}
}
