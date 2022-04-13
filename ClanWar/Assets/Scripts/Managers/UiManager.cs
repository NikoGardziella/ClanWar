using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;

public class UiManager : MonoBehaviour
{
	//profile menu
	
	public AccountInfo info;
	[SerializeField]
	private List<GameObject> menus;
	[SerializeField]
	private static UiManager instance;
	[SerializeField]
	private Text level;
	[SerializeField]
	private Text gems;
	[SerializeField]
	private Text coins;
	[SerializeField]
	private Text playerName;
	[SerializeField]
	private Text trophies;
	[SerializeField]
	private Image exp;
	[SerializeField]
	private List<Toggle> menuToggle;

	public List<Toggle> MenuToggle
	{
		get { return Instance.menuToggle; }
		set { Instance.menuToggle = value; }
	}


	//Shot menu
	[SerializeField]
	private List<GameObject> storeContents;


	//shop menu	
	[SerializeField]
	private List<GameObject> inventoryContents;
	[SerializeField]
	private List<GameObject> deckContents;
	[SerializeField]
	private Text avgCost;
	[SerializeField]
	private int currentCost;

	public static List<GameObject> StoreContents
	{
		get { return Instance.storeContents; }
		set { Instance.storeContents = value; }
	}
	public static List<GameObject> InventoryContents
	{
		get { return Instance.inventoryContents; }
		set { Instance.inventoryContents = value; }
	}
	public static List<GameObject> DeckContents
	{
		get { return Instance.deckContents; }
		set { Instance.deckContents = value; }
	}
	public static Text AvgCost
	{
		get { return Instance.avgCost; }
		set { Instance.avgCost = value; }
	}
	public static int CurrentCost
	{
		get
		{
			return Instance.currentCost;
		}
		set
		{
			Instance.currentCost = value;
		}
	}


	public static Text Level
	{
		get { return Instance.level; }
		set { Instance.level = value; }
	}
	public static Text Gems
	{
		get { return Instance.gems; }
		set { Instance.gems = value; }
	}
	public static Text Coins
	{
		get { return Instance.coins; }
		set { Instance.coins = value; }
	}
	public static Text PlayerName
	{
		get { return Instance.playerName; }
		set { Instance.playerName = value; }
	}
	public static Text Trophies
	{
		get { return Instance.trophies; }
		set { Instance.trophies = value; }
	}
	public static Image Exp
	{
		get { return Instance.exp; }
		set { Instance.exp = value; }
	}


	public static UiManager Instance
	{
		get { return instance; }
		set { instance = value; }
	}

	private void Awake()
	{
		if (instance != this)
			instance = this;
		info = FindObjectOfType<AccountInfo>(); // 10.4 removed GameObject.
	}

	private void Update()
	{
		if (info.Info == null)
			return;
		UpdateText();
		UpdateToggles(menuToggle, menus.ToArray());

		if (menus[GameConstants.MENU_SHOP].activeInHierarchy)
		{
			UpdateShopInfo(); // error
		}
		else if (menus[GameConstants.MENU_DECK].activeInHierarchy)
		{
			UpdateInventoryInfo();
		}
	}

	private void UpdateInventoryInfo()
	{
		//Debug.Log("Updateinventory: InventoryContents.Count" + InventoryContents.Count);
	//	Debug.Log("AccountInfo.Instance.Info.UserInventory.Count" + AccountInfo.Instance.Info.UserInventory.Count);

		for (int i = 0; i < InventoryContents.Count; i++)  // error?
		{
			if (i < AccountInfo.Instance.Info.UserInventory.Count)
			{
				if (AccountInfo.Instance.Info.UserInventory[i].ItemClass == GameConstants.ITEM_CARDS)
				{
					InventoryContents[i].GetComponent<ItemSlot>().Card = AccountInfo.Cards[i];
					InventoryContents[i].SetActive(true);
					InventoryContents[i].GetComponent<Button>().interactable = true;
					InventoryContents[i].transform.GetChild(0).GetComponent<Image>().sprite = AccountInfo.Cards[i].Icon;
					InventoryContents[i].transform.GetChild(1).GetComponent<Text>().text = AccountInfo.Cards[i].Name;
					InventoryContents[i].transform.GetChild(2).GetComponent<Text>().text = string.Format("Cost: {0}", AccountInfo.Cards[i].Cost);
				}
				else
				{
					InventoryContents[i].SetActive(false);
				}
			}
			else
			{
				InventoryContents[i].SetActive(false);
			}
		}
	}

	public static void UpdateDeckinfo(int i, CardStats card)
	{
		DeckContents[i].GetComponent<DeckSlot>().Card = card;
	}

	private void UpdateShopInfo()
	{
		for (int j = 0; j < storeContents[0].transform.GetChild(0).childCount; j++)
		{
			if(j < database.Instance.CardStoreItems.Count) // ERROR
			{
				storeContents[0].transform.GetChild(0).GetChild(j).GetComponent<StoreSlot>().Item = database.Instance.CardStoreItems[j];
				storeContents[0].transform.GetChild(0).GetChild(j).gameObject.SetActive(true);
				storeContents[0].transform.GetChild(0).GetChild(j).GetChild(1).GetComponent<Text>().text = database.Instance.CardStoreItems[j].ItemId;
			}
			else
			{
				storeContents[0].transform.GetChild(0).GetChild(j).gameObject.SetActive(false);
			}
		}

	}

	void UpdateText()
	{
		if(info != null)
		{
			playerName.text = info.Info.AccountInfo.Username;

			int g = -1;
			if(info.Info.UserVirtualCurrency != null)
			{
				if(info.Info.UserVirtualCurrency.TryGetValue(GameConstants.COIN_CODE, out g))
				{
					coins.text = g.ToString();
				}
				if (info.Info.UserVirtualCurrency.TryGetValue(GameConstants.GEM_CODE, out g))
				{
					gems.text = g.ToString();
				}
			}
			UserDataRecord record = new UserDataRecord();
			float min = -1;
			float max = -1;

			if(info.Info.UserData != null)
			{
				if (info.Info.UserData.TryGetValue(GameConstants.DATA_EXP, out record))
				{
					min = float.Parse(record.Value);
				}
				if (info.Info.UserData.TryGetValue(GameConstants.DATA_LEVEL, out record))
				{
					level.text = record.Value;
				}
				if (info.Info.UserData.TryGetValue(GameConstants.DATA_MAX_EXP, out record))
				{
					max = float.Parse(record.Value);
				}

				exp.fillAmount = min / max;
			}

			List<StatisticValue> stats = info.Info.PlayerStatistics;

			foreach (StatisticValue item in stats)
			{
				if(item.StatisticName == GameConstants.STAT_TROPHIES)
				{
					trophies.text = item.Value.ToString();
				}
			}
		}

	}


	public void ChangeMenu(int i, GameObject[] m)
	{
		GameFunctions.ChangeMenu(menus.ToArray(), i);
	}

	void UpdateToggles(List<Toggle> togs,GameObject[] m)
	{
		for (int i = 0; i < togs.Count; i++)
		{
			if (togs[i].isOn)
			{
				ChangeMenu(i,m);
			}
		} 
	}
}
