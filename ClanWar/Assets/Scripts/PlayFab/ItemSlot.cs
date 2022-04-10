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
		for (int i = 0; i < AccountInfo.Deck.Count; i++)
		{
			if(AccountInfo.Deck[i].Name == "")
			{
				AccountInfo.Deck.Add(card);
				GetComponent<Button>().interactable = false;
				UiManager.UpdateDeckinfo(i,card);
				return ;
			}

		}
	}
}
