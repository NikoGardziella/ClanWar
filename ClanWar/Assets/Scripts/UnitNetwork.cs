using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNetwork : UnityEngine.MonoBehaviour
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
		//if(newUnitPosition != unitLocation.transform.position)
		//{
		//	unitLocation.transform.position  = Vector3.Lerp(unitLocation.transform.position, newUnitPosition, .1f);
		//}
		if (oldAgentPosition != agentLocation.transform.localPosition)
		{
			agentLocation.transform.localPosition = Vector3.Lerp(agentLocation.transform.localPosition, oldAgentPosition, .1f);
		}
	}



	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo winfo)
	{
		if (stream.isWriting)
		{
			stream.SendNext(unitLocation.transform.position);
			stream.SendNext(agentLocation.transform.localPosition);
		}
		else
		{
			newUnitPosition = (Vector3)stream.ReceiveNext();
			oldAgentPosition = (Vector3)stream.ReceiveNext();
		}
	}
}
