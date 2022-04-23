using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AccountStats : MonoBehaviour
{
	public bool me = false;
	public int count = 0;
	public bool ready = false;
	public int trophies = 0; // Match Makng Rating
	public int level = 1;

	private void OnSerializeNetworkView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// Me. Send my data to other players
			stream.SendNext(count);
			stream.SendNext(ready);
			stream.SendNext(trophies);
			stream.SendNext(level);
		}
		else
		{
			//Other network player. Receive data
			count = (int)stream.ReceiveNext();
			ready = (bool)stream.ReceiveNext();
			trophies = (int)stream.ReceiveNext();
			level = (int)stream.ReceiveNext();
		}
	}
}
