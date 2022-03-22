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

	public GetPlayerCombinedInfoResultPayload Info
	{
		get { return info; }
		set { info = value; }
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
		Debug.Log("Registered with:" + result.PlayFabId);
	}
	static void OnLogin(LoginResult result)
	{
		GetAccountInfo(result.PlayFabId);
		Debug.Log("Login with:" + result.PlayFabId);
		levelManager.LoadLevel(GameConstants.MAIN_SCENE);
	} 

	public static void GetAccountInfo(string playFabId)
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

		GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest()
		{
			PlayFabId = playFabId,
			InfoRequestParameters = paramInfo
		};
		PlayFabClientAPI.GetPlayerCombinedInfo(request,OnAccountInfo ,GameFunctions.OnAPIError);
	}

	static void OnAccountInfo(GetPlayerCombinedInfoResult result)
	{
		Debug.Log("Updated account info");
		instance.Info = result.InfoResultPayload;
	}

}

