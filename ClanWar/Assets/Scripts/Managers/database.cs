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
	[SerializeField]
	private List<StoreItem> cardStoreItems;
	[SerializeField]
	private List<StoreItem> chestStoreItems;


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
		Debug.Log("UpdateDatabase");
		PlayFabClientAPI.GetCatalogItems(request, OnUpdateDatabase, GameFunctions.OnAPIError);
	}

	static void OnUpdateDatabase(GetCatalogItemsResult result)
	{
		Debug.Log("OnUpdateDatabase");
		for (int i = 0; i < result.Catalog.Count; i++)
		{

			if (result.Catalog[i].ItemClass == GameConstants.ITEM_CARDS)
			{
				Instance.CatalogCards.Add(result.Catalog[i]);
				Instance.cards.Add(GameFunctions.CreateCard(result.Catalog[i], i));
			}
		}

		GetStoreItems(GameConstants.STORE_CHEST);
		GetStoreItems(GameConstants.STORE_CARDS);
	}


	static void GetStoreItems(string id)
	{
		GetStoreItemsRequest request = new GetStoreItemsRequest()
		{
			CatalogVersion = GameConstants.CATALOG_ITEMS,
			StoreId = id
		};

		PlayFabClientAPI.GetStoreItems(request,GotStoreItem , GameFunctions.OnAPIError);
	}

	static void GotStoreItem(GetStoreItemsResult result)
	{
		if(result.StoreId == GameConstants.STORE_CARDS)
		{
			Instance.cardStoreItems = result.Store;
		}
		else if(result.StoreId == GameConstants.STORE_CHEST)
		{
			Instance.chestStoreItems = result.Store;
		}
	}
}
