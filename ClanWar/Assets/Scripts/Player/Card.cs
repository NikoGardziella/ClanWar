using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
	[SerializeField]
	private PlayerStats playerInfo;
	[SerializeField]
	private CardStats cardInfo;
	[SerializeField]
	private Image icon;
	[SerializeField]
	private Text cardName;
	[SerializeField]
	private Text cost;
	[SerializeField]
	private bool canDrag;

	public bool CanDrag
	{

		get { return canDrag; }
		set { canDrag = value; }
	}



	public Text Cost
	{
		get { return cost; }
		set { cost = value; }
	}

	public Text CardName
	{
		get { return cardName; }
		set { cardName = value; }
	}

	public Image Icon
	{
		get { return icon; }
		set { icon = value; }
	}

	public CardStats CardInfo
	{
		get { return cardInfo; }
		set { cardInfo = value; }
	}

	public PlayerStats PlayerInfo
	{
		get { return playerInfo; }
		set { playerInfo = value; }
	}

	private void Update()
	{
		icon.sprite = cardInfo.Icon;
		cardName.text = cardInfo.Name;
		cost.text = cardInfo.Cost.ToString();
	}
}
