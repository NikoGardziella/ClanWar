using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUiManager : MonoBehaviour
{
	private static InGameUiManager instance;

	public InGameUiManager Instance
	{
		get { return instance; }
		set { instance = value; }
	}
		
	[SerializeField]
	private Text currResource;
	[SerializeField]
	private Text maxResource;
	[SerializeField]
	private Text score;
	[SerializeField]
	private Transform handParent;
	[SerializeField]
	private Card nextCard;
	[SerializeField]
	private Transform handparent;


	public static Text CurrResource
	{
		get { return instance.currResource; }
	}
	public static Text MaxResource
	{
		get { return instance.maxResource; }
	}
	public static Text Score
	{
		get { return instance.score; }
	}
	public static Transform Handparent
	{
		get { return instance.handparent; }
	}
	public static Card NextCard
	{
		get { return instance.nextCard; }
	}


	public static void PlayerSetup(PlayerStats ps)
	{
		ps.TextCurrResource = CurrResource;
		ps.TextCurrResource = MaxResource;
		ps.TextCurrResource = Score;
		ps.HandParent = Handparent;
		ps.NextCard = NextCard;
	}




}
