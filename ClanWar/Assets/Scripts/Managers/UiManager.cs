using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;

public class UiManager : MonoBehaviour
{
	private GetPlayerCombinedInfoResultPayload info;
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
		info = GameObject.FindObjectOfType<AccountInfo>().Info;

	}

	private void Update()
	{
		if (info == null)
			return;
		UpdateText();
	}

	void UpdateText()
	{
		if(info != null)
		{
			playerName.text = info.AccountInfo.Username;

			int g = -1;
			if(info.UserVirtualCurrency != null)
			{
				if(info.UserVirtualCurrency.TryGetValue(GameConstants.COIN_CODE, out g))
				{
					coins.text = g.ToString();
				}
				if (info.UserVirtualCurrency.TryGetValue(GameConstants.GEM_CODE, out g))
				{
					gems.text = g.ToString();
				}
			}
			float min = -1;
			float max = -1;


			if(info.UserData != null)
			{
				if (info.UserVirtualCurrency.TryGetValue(GameConstants.DATA_EXP, out g))
				{
					min = g;
				}
				if (info.UserVirtualCurrency.TryGetValue(GameConstants.DATA_LEVEL, out g))
				{
					max = g;
				}
				if (info.UserVirtualCurrency.TryGetValue(GameConstants.DATA_MAX_EXP, out g))
				{
					level.text = g.ToString();
				}

				exp.fillAmount = min / max;
			}

			List<StatisticValue> stats = info.PlayerStatistics;

			foreach (StatisticValue item in stats)
			{
				if(item.StatisticName == GameConstants.STAT_TROPHIES)
				{
					trophies.text = item.Value.ToString();
				}
			}
		}

	}


	public void ChangeMenu(int i)
	{
		GameFunctions.ChangeMenu()
	}
}
