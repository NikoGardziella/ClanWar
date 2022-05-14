using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

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

	public void AddToDeck()
	{
		
		AccountInfo.Deck.Add(card);
		GetComponent<Button>().interactable = false;
		UiManager.UpdateDeckinfo(UiManager.CurrentCount, card);
		UiManager.CurrentCount++;
		UiManager.CurrentCost += card.Cost;
	}
}
