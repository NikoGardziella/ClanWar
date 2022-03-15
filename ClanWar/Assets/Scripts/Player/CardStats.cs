using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardStats
{
	[SerializeField]
	private int index;
	[SerializeField]
	private string name;
	[SerializeField]
	private Sprite icon;
	[SerializeField]
	private int cost;
	[SerializeField]
	private GameObject prefab;


	public GameObject Prefab
	{
		get { return prefab; }
		set { prefab = value; }
	}
	public int Cost
	{
		get { return cost; }
		set { cost = value; }
	}
	public int Index
	{
		get { return index; }
		set { index = value; }
	}


	public Sprite Icon
	{
		get { return icon; }
		set { icon = value; }
	}


	public string Name
	{
		get { return name; }
		set { name = value; }
	}


}
