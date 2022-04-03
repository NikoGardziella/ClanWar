using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class StoreSlot : MonoBehaviour
{
	[SerializeField]
	private StoreItem item;

	public StoreItem Item
	{
		get { return item; }
		set { item = value; }
	}
	

	public void BuydWithCoins()
	{
		uint price = 0;
		if (item.VirtualCurrencyPrices.TryGetValue(GameConstants.COIN_CODE, out price))
		{

		} 
		PurchaseItemRequest request = new PurchaseItemRequest()
		{
			ItemId = item.ItemId,  
			VirtualCurrency = GameConstants.COIN_CODE,

		};
	}
}
