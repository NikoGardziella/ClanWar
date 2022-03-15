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
	private Text textScore;
	[SerializeField]
	private GameObject cardPrefab;
	[SerializeField]
	private Transform handParent;
	[SerializeField]
	private Card nextCard;
	[SerializeField]
	private bool onDragging;

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
	public float CurrResource
	{
		get { return currResource; }
		set { currResource = value; }
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
		set { playersDeck = value; }
	}

}
