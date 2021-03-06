using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField]
	private PlayerStats playerInfo;
	[SerializeField]
	private CardStats cardInfo;
	[SerializeField]
	private Image icon;
	[SerializeField]
	private Text cardName;
	[SerializeField]
	private Text cost;
	[SerializeField]
	private bool canDrag;

	public bool CanDrag
	{

		get { return canDrag; }
		set { canDrag = value; }
	}


	public Text Cost
	{
		get { return cost; }
	}

	public Text CardName
	{
		get { return cardName; }
		set { cardName = value; }
	}

	public Image Icon
	{
		get { return icon; }
		set { icon = value; }
	}

	public CardStats CardInfo
	{
		get { return cardInfo; }
		set { cardInfo = value; }
	}

	public PlayerStats PlayerInfo
	{
		get { return playerInfo; }
		set { playerInfo = value; }
	}

	private void Update()
	{
		icon.sprite = cardInfo.Icon;
		cardName.text = cardInfo.Name;
		cost.text = cardInfo.Cost.ToString();
	}

	public void Start()
	{
	}

	private void SpawnUnit()
	{
		if(playerInfo.GetCurrResource >= cardInfo.Cost)
		{
			playerInfo.PlayersDeck.RemoveHand(cardInfo.Index);
			playerInfo.RemoveResource(cardInfo.Cost);

			Vector3 mousePos = Input.mousePosition;
			Vector3 wordPos;
			Ray ray = Camera.main.ScreenPointToRay(mousePos);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000f))
			{
				wordPos = hit.point;
			}
			else
			{
				wordPos = Camera.main.ScreenToWorldPoint(mousePos);
			}

			//or for tandom rotarion use Quaternion.LookRotation(Random.insideUnitSphere)

			/*Vector3 mousePos = Input.mousePosition;
			mousePos.z = Camera.main.nearClipPlane;

			Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos); // 24.5 changed from screentoworldpoint */
			GameFunctions.SpawnUnit(cardInfo.Prefab.name, playerInfo.UnitTransform, wordPos); 
			Destroy(gameObject);
		}
		else
		{
			transform.SetParent(playerInfo.HandParent);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!playerInfo.OnDragging)
		{
			if (canDrag)
			{
				GetComponent<CanvasGroup>().blocksRaycasts = false;
				playerInfo.OnDragging = true;
				playerInfo.SpawnZone = true;
				transform.SetParent(GameFunctions.GetCanvas());
			}
		}
	}


	public void OnDrag(PointerEventData eventData)
	{
		if (playerInfo.OnDragging)
		{
			transform.position = Input.mousePosition;
		} 
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		GameObject go = eventData.pointerCurrentRaycast.gameObject;

		if(go != null)
		{
			if(go == playerInfo.LeftArea && playerInfo.LeftZone)
			{
				SpawnUnit();
			}
			else if (go == playerInfo.RightArea && playerInfo.RightZone)
			{
				SpawnUnit();
			}
		}
		else
		{
			SpawnUnit();
		}
		transform.SetParent(playerInfo.HandParent);
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		playerInfo.OnDragging = false;
		playerInfo.SpawnZone = false;
	}
}
