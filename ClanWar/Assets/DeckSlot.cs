using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSlot : MonoBehaviour
{
	[SerializeField]
	Image icon;
	[SerializeField]
	Text id, cost;
	[SerializeField]
	CardStats card;

	public CardStats Card
	{
		get
		{
			return card;
		}
		set
		{
			card = value;
		}
	}

	private void Update()
	{
		if(card != null)
		{
			icon.sprite = card.Icon;
			id.text = card.Name;
			cost.text = string.Format("Cost: {0}", card.Cost);
		}
	}

}
