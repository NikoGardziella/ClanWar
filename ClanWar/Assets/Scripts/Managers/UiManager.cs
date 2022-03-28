using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
	private static UiManager instance;

	public static UiManager Instance
	{
		get { return instance; }
		set { instance = value; }
	}

	private void Awake() 
	{
		if (instance != this)
			instance = this;
	}

}
