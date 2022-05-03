using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keep : Structure
{
	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(Stats.CurrentHealth);
			stream.SendNext(Stats.HealthBar.fillAmount);
			stream.SendNext(LeftTower);
		}
		else
		{
			Stats.CurrentHealth = (float)stream.ReceiveNext();
			Stats.HealthBar.fillAmount = (float)stream.ReceiveNext();
			LeftTower = (bool)stream.ReceiveNext();
		}
	}

}
