using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class database : MonoBehaviour
{
	private static database instance;
	[SerializeField]
	private List<CardStats> cards;
	[SerializeField]
	private List<CatalogItem> catalogCards;

	public List<CatalogItem> CatalogCards
	{
		get { return catalogCards; }
		set { catalogCards = value; }
	}


	public List<CardStats> Cards
	{
		get { return cards; }
		set { cards = value; }
	}

	public static database Instance
	{
		get { return instance; }
		set { instance = value; }
	}
	private void Awake()
	{
		if (instance != this)
			instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public static void UpdateDatabase()
	{
		GetCatalogItemsRequest request = new GetCatalogItemsRequest()
		{
			CatalogVersion = GameConstants.CATALOG_ITEMS
		};

		PlayFabClientAPI.GetCatalogItems(request, OnUpdateDatabase ,GameFunctions.OnAPIError)
	}

	static void OnUpdateDatabase(GetCatalogItemsResult result)
	{
		for (int i = 0; i < result.Catalog.Count; i++)
		{
			if (result.Catalog[i].ItemId == GameConstants.ITEM_CARDS)
			{

			}
		}
	}
}
