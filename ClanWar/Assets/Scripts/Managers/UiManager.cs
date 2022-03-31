using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;

public class UiManager : MonoBehaviour
{
	public AccountInfo info;
	[SerializeField]
	private List<GameObject> menus;
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
		info = FindObjectOfType<AccountInfo>();

	}

	private void Update()
	{
		if (info.Info == null)
			return;
		UpdateText();
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


	public void ChangeMenu(int i)
	{
		GameFunctions.ChangeMenu(menus.ToArray(), i);
	}
}
