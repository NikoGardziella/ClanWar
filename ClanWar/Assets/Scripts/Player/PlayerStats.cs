using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerStats : MonoBehaviour
{
	[SerializeField]
	private Deck playersDeck;
	[SerializeField]
	private List<Image> resources;
	[SerializeField]
	private int score;
	[SerializeField]
	private float currResource;
	[SerializeField]
	private Text textMaxResource;
	[SerializeField]
	private Text textCurrResource;
	[SerializeField]
	private Text textScore;
	[SerializeField]
	private GameObject cardPrefab;
	[SerializeField]
	private Transform handParent;
	[SerializeField]
	private Card nextCard;
	[SerializeField]
	private bool onDragging;
	[SerializeField]
	private Transform unitTransform;
	[SerializeField]
	private bool spawnZone;
	[SerializeField]
	private bool leftZone;
	[SerializeField]
	private bool rightZone;
	[SerializeField]
	private GameObject leftArea;
	[SerializeField]
	private GameObject rightArea;

	public GameObject RightArea
	{
		get { return rightArea; }
		set { rightArea = value; }
	}
	public GameObject LeftArea
	{
		get { return leftArea; }
		set { leftArea = value; }
	}
	public bool LeftZone
	{
		get { return leftZone; }
		set { leftZone = value; }
	}
	public bool RightZone
	{
		get { return rightZone; }
		set { rightZone = value; }
	}

	public bool SpawnZone
	{
		get { return spawnZone; }
		set { spawnZone = value; }
	}

	public Transform UnitTransform
	{
		get { return unitTransform; }
		set { unitTransform = value; }
	}
	public bool OnDragging
	{
		get { return onDragging; }
		set { onDragging = value; }
	}

	public Card NextCard
	{
		get { return nextCard; }
		set { nextCard = value; }
	}
	public Transform HandParent
	{
		get { return handParent; }
		set { handParent = value; }
	}
	public GameObject CardPrefab
	{
		get { return cardPrefab; }
		set { cardPrefab = value; }
	}
	public Text TextScore
	{
		get { return textScore; }
		set { textScore = value; }
	}
	public Text TextMaxResource
	{
		get { return textMaxResource; }
		set { textMaxResource = value; }
	}
	public Text TextCurrResource
	{
		get { return textCurrResource; }
		set { textCurrResource = value; }
	}
	public float CurrResource
	{
		get
		{
			return currResource;
		}
		set
		{
			currResource = value;
		}
	}
	public int Score
	{
		get { return score; }
		set { score = value; }
	}
	public List<Image> Resources
	{
		get { return resources; }
		set { resources = value; }
	}
	public Deck PlayersDeck
	{
		get { return playersDeck; }
	} 

	public int GetCurrResource
	{
		get
		{
			return (int)currResource;
		}
	}

	private void Start()
	{
		playersDeck.Start();
	}

	private void Update()
	{
		if(GetCurrResource < GameConstants.RESOURCE_MAX + 1)
		{
			resources[GetCurrResource].fillAmount = currResource - GetCurrResource;
			currResource += Time.deltaTime * GameConstants.RESOURCE_SPEED;
		}

		if (spawnZone)
		{
			leftArea.SetActive(!leftZone ? true : false);
			rightArea.SetActive(!rightZone ? true : false);
		}
		else
		{
			leftArea.SetActive(false);
			rightArea.SetActive(false);
		}

		UpdateText();
		UpdateDeck();

		
	}

	void UpdateText()
	{
		textCurrResource.text = GetCurrResource.ToString();
		textMaxResource.text = (GameConstants.RESOURCE_MAX + 1).ToString();
		textScore.text = score.ToString();
	}
	
	void UpdateDeck()
	{
		if(playersDeck.Hand.Count < GameConstants.MAX_HAND_SIZE)
		{
			CardStats cs = playersDeck.DrawCard();
			GameObject go = Instantiate(cardPrefab, handParent);
			Card c = go.GetComponent<Card>();
			c.PlayerInfo = this;
			c.CardInfo = cs;
		}

		nextCard.CardInfo = playersDeck.NextCard;
		nextCard.PlayerInfo = this;
	}

	public void RemoveResource(int cost)
	{
		currResource -= cost;
		for (int i = 0; i < resources.Count; i++)
		{
			resources[i].fillAmount = 0;
			if(i <= GetCurrResource)
			{
				resources[i].fillAmount = 1;
			}
		}
	}


}
