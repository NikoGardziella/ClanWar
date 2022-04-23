using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
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
		if(firstLoad)
	}
}
