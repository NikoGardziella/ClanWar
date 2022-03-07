using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor3D : MonoBehaviour
{
	private NavMeshAgent agent;

	public NavMeshAgent Agent
	{
		get { return agent; }

	}



	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

}
