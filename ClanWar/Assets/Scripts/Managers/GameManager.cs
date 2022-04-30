using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	[SerializeField]
	private List<GameObject> objects;
	[SerializeField]
	private List<PlayerStats> players;

	public List<PlayerStats> Players
	{
		get { return players; }
		set { players = value; }
	}

	public List<GameObject> Objects
	{
		get { return objects; }
	}



	public static GameManager Instance {get { return instance; } }

	private void Awake()
	{
		if (instance != this)
			instance = this;


		GameObject go = PhotonNetwork.Instantiate(GameConstants.GAME_PLAYER, Vector3.zero, Quaternion.identity, 0);
		Debug.Log("Ingameplayer instantiated");
		go.transform.position = GameConstants.PLAYER_START;
		go.transform.rotation = GameConstants.PLAYER_ROT;
		go.tag = GameConstants.PLAYER_TAG;
		PlayerStats ps = go.GetComponent<PlayerStats>();
		ps.enabled = true;
		go.transform.GetChild(1).gameObject.SetActive(true);
		go.transform.GetChild(2).gameObject.SetActive(true);
		ps.PlayersDeck.Cards = AccountInfo.Deck;
		Debug.Log("Players Deck: " + ps.PlayersDeck.Cards);

		PhotonNetwork.Instantiate(GameConstants.PLAYER_KEEP, , , 0)

		//////Players.Add(ps);
		//////Objects.Add(go.transform.GetChild(3).GetChild(0).gameObject);
		//////Objects.Add(go.transform.GetChild(3).GetChild(1).gameObject);
		//////Objects.Add(go.transform.GetChild(3).GetChild(2).gameObject);

	}

	public static void RemoveObjectFromList(GameObject go)
	{
		foreach  (GameObject g in Instance.Objects)
		{
			Component component = g.GetComponent(typeof(IDamageable));
			if (component)
			{
				if((component as IDamageable).HitTargets.Contains(go))
				{
					(component as IDamageable).HitTargets.Remove(go);
					if((component as IDamageable).Target == go)
					{
						(component as IDamageable).Target = null;
					}
				}
			}
		}

		Instance.Objects.Remove(go);

	}
	public static void RemoveObjectFromList(GameObject go, bool leftTower)
	{
		foreach (GameObject g in Instance.Objects)
		{
			Component component = g.GetComponent(typeof(IDamageable));
			if (component)
			{
				if ((component as IDamageable).HitTargets.Contains(go))
				{
					(component as IDamageable).HitTargets.Remove(go);
					if ((component as IDamageable).Target == go)
					{
						(component as IDamageable).Target = null;
					}
				}
			}
		}
		if (!go.CompareTag(GameConstants.PLAYER_TAG))
		{
			if (leftTower)
			{
				instance.players[0].LeftZone = true;
			}
			else
			{
				instance.players[0].RightZone = true;
			}
			instance.players[0].Score++;
		}
		else
		{
			if (leftTower)
			{
				instance.players[1].LeftZone = true;
			}
			else
			{
				instance.players[1].RightZone = true;
			}
			instance.players[1].Score++;
		}
		Instance.Objects.Remove(go);

	}

	public static List<GameObject> GetAllEnemies(Vector3 pos, List<GameObject> objects, string tag, float range)
	{
		List<GameObject> sentObjects = new List<GameObject>();
		foreach (GameObject g in objects)
		{
			if(!g.CompareTag(tag) && Vector3.Distance(pos, g.transform.position) <= range)
			{
				sentObjects.Add(g);
			}
		}
		
		return sentObjects;
	}

	public static List<GameObject> GetAllEnemies(Vector3 pos, List<GameObject> objects, string tag)
	{
		List<GameObject> sentObjects = new List<GameObject>();
		foreach (GameObject g in objects)
		{
			if (!g.CompareTag(tag))
			{
				sentObjects.Add(g);
			}
		}

		return sentObjects;
	}

	public static void AddObject(GameObject go)
	{
		Instance.Objects.Add(go);
	}

	public void GoToLogin()
	{
		levelManager.LoadLevel(0);
	}
}
