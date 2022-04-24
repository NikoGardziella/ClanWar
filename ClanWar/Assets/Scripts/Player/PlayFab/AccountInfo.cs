using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;


public class AccountInfo : MonoBehaviour 
{
	public string[] deckInfo;
	private static AccountInfo instance;
	[SerializeField]
	private GetPlayerCombinedInfoResultPayload info;
	[SerializeField]
	private List<CardStats> cards = new List<CardStats>();
	[SerializeField]
	private List<CardStats> deck = new List<CardStats>();

	private string playFabId = "";
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
			DisplayName = username,
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
		Instance.playFabId = result.PlayFabId;
		GetAccountInfo(result.PlayFabId); // added: result.PlayFabId 10.4
		database.UpdateDatabase();

		GetPhotonAuthenticationTokenRequest request = new GetPhotonAuthenticationTokenRequest()
		{
			PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppID.Trim()
		};
		PlayFabClientAPI.GetPhotonAuthenticationToken(request, OnPhotonAuthSuccess, GameFunctions.OnAPIError);

		levelManager.LoadLevel(GameConstants.MAIN_SCENE);
	}

	static void OnPhotonAuthSuccess(GetPhotonAuthenticationTokenResult result)
	{
		AuthenticationValues customAuth = new AuthenticationValues();
		Debug.Log("OnPhotonAuthSuccess" + Instance.playFabId);
		customAuth.UserId = instance.playFabId;
		customAuth.AuthType = CustomAuthenticationType.Custom;
		customAuth.AddAuthParameter("username", Instance.playFabId);
		customAuth.AddAuthParameter("Token", result.PhotonCustomAuthenticationToken);
		customAuth.Token = result.PhotonCustomAuthenticationToken;

		PhotonNetwork.AuthValues = customAuth;
		PhotonNetwork.ConnectUsingSettings(GameConstants.VERSION);

	/*	PhotonNetwork.AuthValues = new AuthenticationValues
		{
			AuthType = CustomAuthenticationType.Custom,
			UserId = Instance.Info.AccountInfo.PlayFabId
		};
		PhotonNetwork.AuthValues.AddAuthParameter("username", Instance.Info.AccountInfo.PlayFabId);
		PhotonNetwork.AuthValues.AddAuthParameter("Token", result.PhotonCustomAuthenticationToken);
		PhotonNetwork.ConnectUsingSettings(GameConstants.VERSION); */
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

	public static void UpdateLeaderBoards(string key)
	{
		GetLeaderboardRequest request = new GetLeaderboardRequest()
		{
			StatisticName = key,
			StartPosition = 0
		};
		PlayFabClientAPI.GetLeaderboard(request, GotLeaderBoards, GameFunctions.OnAPIError);

	}



	public static void GotLeaderBoards(GetLeaderboardResult result)
	{
		UiManager.LeaderBoardEntries = result.Leaderboard;
	}


	public static void AddToDeck()
	{
		string deckContents = "";
		foreach  (CardStats item in Deck)
		{
			deckContents += item.Name + ",";
		}
		Dictionary<string, string> data = new Dictionary<string, string>
		{
			{ GameConstants.DATA_DECK, deckContents }
		};

		UpdateUserDataRequest request = new UpdateUserDataRequest()
		{
			Data = data

		};

		PlayFabClientAPI.UpdateUserData(request, GotData, GameFunctions.OnAPIError);

	}

	private static void GotData(UpdateUserDataResult result)
	{
		Debug.Log("Updated data!");
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
			if (Instance.Info.UserInventory[i].ItemClass == GameConstants.ITEM_CARDS)
			{
				Debug.Log("Add Cards" + Instance.Info.UserInventory[i]);
				Cards.Add(database.GetCardInfo(Instance.Info.UserInventory[i], i));
			}
		}
		UserDataRecord temp;
		if(Instance.info.UserData.TryGetValue(GameConstants.DATA_DECK, out temp))
		{
			Instance.deckInfo = temp.Value.Split(',');
			for (int i = 0; i < Instance.deckInfo.Length - 1; i++)
			{
				for (int j = 0; j < Instance.Info.UserInventory.Count; j++)
				{
					if(Instance.deckInfo[i] == Instance.Info.UserInventory[j].ItemId)
					{
						Deck.Add(database.GetCardInfo(Instance.Info.UserInventory[j], j));
						break ;
					}

				}
			}
		}


	}

}

