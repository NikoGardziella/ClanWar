using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{

	[SerializeField]
	PlayerStats player;

	private void Update()
	{
		player.TextScore.text = player.Score.ToString();
	}
	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(player.Score);
		}
		else
		{
			player.Score = (int)stream.ReceiveNext();
		}
	}
}
