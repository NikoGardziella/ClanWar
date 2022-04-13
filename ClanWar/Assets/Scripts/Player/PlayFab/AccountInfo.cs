using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class AccountInfo : MonoBehaviour
{
	private static AccountInfo instance;
	[SerializeField]
	private GetPlayerCombinedInfoResultPayload info;
	[SerializeField]
	private List<CardStats> cards = new List<CardStats>();
	[SerializeField]
	private List<CardStats> deck = new List<CardStats>();
	public GetPlayerCombinedInfoResultPayload Info
	{
		get { return info; }
		set { info = value; }
	}

	public static List<CardStats> Cards
	{
		get { return Instance.cards; }
		set { Instance.cards = value; }
	}
	public static List<CardStats> Deck
	{
		get { return Instance.deck; }
		set { Instance.deck = value; }
	}


	public static AccountInfo Instance
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

	private void Update()
	{
		if(Info != null && instance.cards.Count == 0 && database.Updated) // 12.4 changed from Cards.Count
		{
			
			AddCards();
		}
	}
	public static void Register(string username, string email, string password)
	{
		RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
		{
			TitleId = PlayFabSettings.TitleId,
			Email = email,
			Username = username,
			Password = password,

		};

		PlayFabClientAPI.RegisterPlayFabUser(request, OnRegister, GameFunctions.OnAPIError);
	}

	public static void Login(string username, string password)
	{
		LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
		{
			TitleId = PlayFabSettings.TitleId,
			Username = username,
			Password = password,

		};

		PlayFabClientAPI.LoginWithPlayFab(request, OnLogin, GameFunctions.OnAPIError);
	}
	static void OnRegister(RegisterPlayFabUserResult result)
	{
		Instance.SetUpAccount();
		Debug.Log("Registered with:" + result.PlayFabId);
	}
	static void OnLogin(LoginResult result)
	{
		Debug.Log("Login with:" + result.PlayFabId);
		GetAccountInfo(result.PlayFabId); // added: result.PlayFabId 10.4
		database.UpdateDatabase();
		levelManager.LoadLevel(GameConstants.MAIN_SCENE);
	}

	public static void GetAccountInfo()
	{
		GetPlayerCombinedInfoRequestParams paramInfo = new GetPlayerCombinedInfoRequestParams()
		{
			GetTitleData = true,
			GetUserInventory = true,
			GetUserAccountInfo = true,
			GetUserVirtualCurrency = true,
			GetPlayerProfile = true,
			GetPlayerStatistics = true,
			GetUserData = true,
			GetUserReadOnlyData = true
		};
		Debug.Log("GetAccountinfo");
		GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest()
		{
			PlayFabId = Instance.info.AccountInfo.PlayFabId,
			InfoRequestParameters = paramInfo
		};
		PlayFabClientAPI.GetPlayerCombinedInfo(request, OnAccountInfo, GameFunctions.OnAPIError);
	}
	public static void GetAccountInfo(string playfabId)
	{
		GetPlayerCombinedInfoRequestParams paramInfo = new GetPlayerCombinedInfoRequestParams()
		{
			GetTitleData = true,
			GetUserInventory = true,
			GetUserAccountInfo = true,
			GetUserVirtualCurrency = true,
			GetPlayerProfile = true,
			GetPlayerStatistics = true,
			GetUserData = true,
			GetUserReadOnlyData = true
		};
		Debug.Log("GetAccountinfo");
		GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest()
		{
			PlayFabId = playfabId,
			InfoRequestParameters = paramInfo
		};
		PlayFabClientAPI.GetPlayerCombinedInfo(request, OnAccountInfo, GameFunctions.OnAPIError);
	}
	static void OnAccountInfo(GetPlayerCombinedInfoResult result)
	{
		Instance.Info = result.InfoResultPayload;
		AddCards();
		Debug.Log("Updated account info");
	}

	void SetUpAccount()
	{
		Dictionary<string, string> data = new Dictionary<string, string>
		{
			{ GameConstants.DATA_EXP, "1" },
			{ GameConstants.DATA_MAX_EXP, "100" },
			{ GameConstants.DATA_LEVEL, "1" }
		};


		UpdateUserDataRequest request = new UpdateUserDataRequest()
		{
			Data = data	
		};
		PlayFabClientAPI.UpdateUserData(request, UpdateDataInfo, GameFunctions.OnAPIError);
	}

	void UpdateDataInfo(UpdateUserDataResult result)
	{
		Debug.Log("UpdateDataInfo");

		List<StatisticUpdate> stats = new List<StatisticUpdate>();
		StatisticUpdate trophies = new StatisticUpdate
		{
			StatisticName = GameConstants.STAT_TROPHIES,
			Value = 0
		};
		stats.Add(trophies);

		UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest()
		{
			Statistics = stats
		};

		PlayFabClientAPI.UpdatePlayerStatistics(request, UpdateStatInfo, GameFunctions.OnAPIError);
	}

	void UpdateStatInfo(UpdatePlayerStatisticsResult result)
	{
		Debug.Log("UpdateStatInfo");
	}

	static void AddCards()
	{
		for (int i = 0; i < Instance.Info.UserInventory.Count; i++)
		{
			Debug.Log("Add Cards" + Instance.Info.UserInventory[i]);
			if (Instance.Info.UserInventory[i].ItemClass == GameConstants.ITEM_CARDS)
			{
				Debug.Log("Add Cards" + Instance.Info.UserInventory[i]);
				Cards.Add(database.GetCardInfo(Instance.Info.UserInventory[i], i));
			}
		}
	}

}

