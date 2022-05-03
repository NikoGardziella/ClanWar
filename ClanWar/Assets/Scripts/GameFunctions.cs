using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public static class GameFunctions
{

	public static void ChangeMenu(GameObject[] menus, int id)
	{
		for (int i = 0; i < menus.Length; i++)
		{
			menus[i].SetActive(i == id ? true : false);
		}
	}
	public static bool  CanAttack(string playerTag, string enemyTag, Component damageable, baseStats stats)
	{
		if (damageable)
		{
			if (playerTag != enemyTag)
			{
				if ((stats.ObjectAttackable == GameConstants.OBJECT_ATTACKABLE.BOTH))
					return true;
				else
				{
					if ((stats.ObjectAttackable == GameConstants.OBJECT_ATTACKABLE.GROUND && (damageable as IDamageable).Stats.ObjectType == GameConstants.OBJECT_TYPE.GROUND))
						return true;
					else if ((stats.ObjectAttackable == GameConstants.OBJECT_ATTACKABLE.FLYING && (damageable as IDamageable).Stats.ObjectType == GameConstants.OBJECT_TYPE.FLYING))
						return true;
				}
			}
		}
		return false;
	}

	public static void Attack(Component damageable, float baseDamage)
	{
		if(damageable)
		{
			(damageable as IDamageable).TakeDamage(baseDamage);
		}
	}

	public static GameObject GetNearestTarget(List<GameObject> hitTargets, SphereCollider mySc, string tag, float range)
	{
		if(hitTargets.Count > 0)
		{
			GameObject go = hitTargets[0];

			Component targetComponent = hitTargets[0].GetComponent(typeof(IDamageable));
			SphereCollider targetSc = (targetComponent as IDamageable).Stats.DetectionObject;

			float dist = Vector3.Distance(mySc.transform.position, targetSc.transform.position);

			foreach (GameObject ht in hitTargets)
			{
				targetComponent = ht.GetComponent(typeof(IDamageable));

				if (targetComponent)
				{
					targetSc = (targetComponent as IDamageable).Stats.DetectionObject;

					float newDist = Vector3.Distance(mySc.transform.position, targetSc.transform.position);

					if(dist > newDist && newDist <= range)
					{
						if (!ht.CompareTag(tag))
						{
							dist = newDist;
							go = ht;
						}
					}
				}
			}
			return go;
		}
		return null;
	}

	public static GameObject GetNearestTarget(List<GameObject> hitTargets, SphereCollider mySc, string tag)
	{
		if (hitTargets.Count > 0)
		{
			GameObject go = hitTargets[0];

			Component targetComponent = hitTargets[0].GetComponent(typeof(IDamageable));
			SphereCollider targetSc = (targetComponent as IDamageable).Stats.DetectionObject;

			float dist = Vector3.Distance(mySc.transform.position, targetSc.transform.position);

			foreach (GameObject ht in hitTargets)
			{
				targetComponent = ht.GetComponent(typeof(IDamageable));

				if (targetComponent)
				{
					targetSc = (targetComponent as IDamageable).Stats.DetectionObject;

					float newDist = Vector3.Distance(mySc.transform.position, targetSc.transform.position);

					if (dist > newDist)
					{
						if (!ht.CompareTag(tag))
						{
							dist = newDist;
							go = ht;
						}
					}
				}
			}
			return go;
		}
		return null;
	}

	public static Transform GetCanvas()
	{
		return GameObject.Find(GameConstants.HUD_CANVAS).transform;
	}

	public static void SpawnUnit(string prefab, Transform parent, Vector3 pos)
	{
		GameObject go = PhotonNetwork.Instantiate(prefab, Vector3.zero, Quaternion.identity, 0);
		go.GetComponent<unit>().enabled = true;
		go.tag = GameConstants.PLAYER_TAG;
		go.transform.SetParent(parent, false);
		go.transform.position = new Vector3(pos.x, 0, pos.z);
		GameManager.AddObject(go);
	}

	public static AccountStats FoundPlayer(int mmr, AccountStats[] accountStats)
	{
		foreach (AccountStats acc in accountStats)
		{
			if(acc.looking && !acc.me)
			{
				if (mmr <= acc.trophies + 50 || mmr >= acc.trophies - 50)
					return acc;
			}
		}

		return null;
	}

	public static CardStats CreateCard(CatalogItem item, int i)
	{
		Debug.Log("CreateCard" + item);
		Sprite icon = Resources.Load(GetCatalogCustomData(GameConstants.ITEM_ICON, item), typeof(Sprite)) as Sprite;
		GameObject prefab = Resources.Load(GetCatalogCustomData(GameConstants.ITEM_PREFAB, item), typeof(GameObject)) as GameObject;
		Debug.Log("CreateCard prefab: " + prefab);
		// wrong? add () ? ERROR
		CardStats cs = new CardStats()
		{

			Index = i,
			Name = item.DisplayName,
			Cost = int.Parse(GetCatalogCustomData(GameConstants.ITEM_COST, item)),
			Icon = icon,
			Prefab = prefab,
			Count = int.Parse(GetCatalogCustomData(GameConstants.ITEM_COUNT, item)),
			InDeck = int.Parse(GetCatalogCustomData(GameConstants.ITEM_IN_DECK, item))
		};
		Debug.Log("cs.Index:" + cs.Index + "cs.Name" + cs.Name + "cs.Cost: " + cs.Cost + "cs.Icon: " + cs.Icon + "cs.Prefab:" + cs.Prefab + "cs.Count:" + cs.Count + "cs.InDeck:" + cs.InDeck);
		return cs;
	}



	public static string GetCatalogCustomData(int i, CatalogItem item)
	{
		//Debug.Log(item.CustomData);
		string cDataTemp = item.CustomData.Trim();
		cDataTemp = cDataTemp.TrimStart('{');
		cDataTemp = cDataTemp.TrimEnd('}');
		string[] newCData;
		newCData = cDataTemp.Split(',', ':');

		for (int s = 0; s < newCData.Length; s++)
		{
			if (i == s)
			{
				newCData[s] = newCData[s].Trim();
				newCData[s] = newCData[s].TrimStart('"');
				newCData[s] = newCData[s].TrimEnd('"');
				newCData[s] = newCData[s].Trim();
				return newCData[s];
			}

		}
		Debug.Log(string.Format("GetCatalogCustomdata - could not find ID: {0} in {1}", i, item.DisplayName));
		return "ERROR";
	}


	public static void OnAPIError(PlayFabError error)
	{
		Debug.LogError(error);
	}
}
