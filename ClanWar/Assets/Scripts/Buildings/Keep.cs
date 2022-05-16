using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keep : Structure
{
	public PlayerStats playerStats;

	void OnDestroy()
	{
		Debug.Log("keep has died");
		if (gameObject.CompareTag("Player"))
		{
			Debug.Log("Player keep has died");
			playerStats.YouLost();
		}
		else if (gameObject.CompareTag("Enemy"))
		{
			playerStats.YouWon();
			Debug.Log("Enemy keep has died");


		}

	}


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
