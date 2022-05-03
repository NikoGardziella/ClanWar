using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNetwork : MonoBehaviour
{

	[SerializeField]
	GameObject unitLocation;
	[SerializeField]
	GameObject agentLocation;
	[SerializeField]
	Vector3 newUnitPosition;
	[SerializeField]
	Vector3 oldAgentPosition;

	private void Update()
	{
		if(newUnitPosition != unitLocation.transform.position)
		{
			Vector3.Lerp(unitLocation.transform.position, newUnitPosition, 1);
		}
		if (oldAgentPosition != agentLocation.transform.position)
		{
			Vector3.Lerp(agentLocation.transform.position, oldAgentPosition, 1);
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(unitLocation.transform.position);
			stream.SendNext(agentLocation.transform.position);
		}
		else
		{
			newUnitPosition = (Vector3)stream.ReceiveNext();
			oldAgentPosition = (Vector3)stream.ReceiveNext();
		}
	}
}
