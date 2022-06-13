using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;


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

	public static GameObject GetNearestTarget(List<GameObject> hitTargets, SphereCollider mySc, string tag, float range) //FOR BUILDING
	{
		if (hitTargets.Count > 0)
		{
			GameObject go = hitTargets[0];
			if (go == null)
				return (null);
			Component targetComponent = hitTargets[0].GetComponent(typeof(IDamageable));
			if (targetComponent == null)
				return (null);
			SphereCollider targetSc = (targetComponent as IDamageable).Stats.DetectionObject;

			float dist = Vector3.Distance(mySc.transform.position, targetSc.transform.position);

			foreach (GameObject ht in hitTargets)
			{
				targetComponent = ht.GetComponent(typeof(IDamageable));
				//Debug.Log("found ht target:" + ht);
				if (targetComponent)
				{
					targetSc = (targetComponent as IDamageable).Stats.DetectionObject;

					float newDist = Vector3.Distance(mySc.transform.position, targetSc.transform.position);
					//Debug.Log("mypos:" + mySc.transform.position + " target pos" + targetSc.transform.position);
					//Debug.Log("newdist" + newDist);
					//Debug.Log("range" + range);
					//Debug.Log("dist" + dist);

					if (dist >= newDist && newDist <= range) // if (dist > newDist && newDist <= range)
					{
						if (!ht.CompareTag(tag))
						{
							//Debug.Log("Structure: go = ht");
							dist = newDist;
							go = ht;
						}
						else
							Debug.Log("wrong tag");
					}
					//else
						//Debug.Log("not in range");
				}
				//else
				//	Debug.Log("no target component");
			}
			return go;
		}
		else
			Debug.Log("no ht targets");
		return null;
	}

	public static GameObject GetNearestTarget(List<GameObject> hitTargets, SphereCollider mySc, string tag) // FOR UNIT
	{
		//bool EnemyInZone = false;

		if (hitTargets.Count > 0)
		{
			/*	foreach (GameObject ht in hitTargets)
				{
					Debug.Log("list hitTargets: "+ ht);
				}
				// hitTargets.Sort(Prioritise);


				foreach (GameObject ht in hitTargets)
				{
					Debug.Log("list after sort: " + ht);
					Debug.Log(ht);
				} */

			/*	Debug.Log("ht before:" + hitTargets[0]);

				for (int i = 0; i < hitTargets.Count; i++)
				{
					if (hitTargets[i].CompareTag(GameConstants.NEUTRAL_TAG))
						i++;
					else
					{
						GameObject temp;
						temp = hitTargets[0];
						hitTargets[0] = hitTargets[i]; // swap?
						hitTargets[i] = temp;
						break;
					}
				}
				Debug.Log("ht after:" + hitTargets[0]); */
			Collider[] hitColliders = Physics.OverlapBox(mySc.transform.position, mySc.transform.localScale / 2, Quaternion.identity);
			foreach (var ht in hitColliders)
			{
				if (ht.CompareTag(GameConstants.ENEMY_TAG))
				{
					Debug.Log("Found enemy near");
					return ht.gameObject;
				}
			}

			GameObject go = hitTargets[0];


			Component targetComponent = hitTargets[0].GetComponent(typeof(IDamageable));
			SphereCollider targetSc = (targetComponent as IDamageable).Stats.DetectionObject;

			float dist = Vector3.Distance(mySc.transform.position, targetSc.transform.position);
			

			foreach (GameObject ht in hitTargets)
			{
				/*if (ht.CompareTag(GameConstants.ENEMY_TAG))
				{
					targetSc = (targetComponent as IDamageable).Stats.DetectionObject; // change to units box colllider ??
					float newDist = Vector3.Distance(mySc.transform.position, targetSc.transform.position);
					if (dist >= newDist)
					{
						//EnemyInZone = true;
					}
				}*/
				//Debug.Log(ht.GetComponent<unit>().priority);

				targetComponent = ht.GetComponent(typeof(IDamageable));

				if (targetComponent)
				{
					targetSc = (targetComponent as IDamageable).Stats.DetectionObject; // change to units box colllider ??

					float newDist = Vector3.Distance(mySc.transform.position, targetSc.transform.position);

					if (dist >= newDist) // >=
					{
						if (!ht.CompareTag(tag))
						{
							dist = newDist;
							go = ht;
						//	Debug.Log("UNIT:" + go);
						}
					}
				}
				/*else if (ht.CompareTag(GameConstants.NEUTRAL_TAG))
				{
					go = ht;
				} */
			}
			Debug.Log("return: Unit Go" + go);
		//	if(!go.CompareTag(tag))
				return go;
		}
		return null;
	}

	private static int Prioritise(GameObject x, GameObject y)
	{
		if (x.GetComponent<unit>().Priority > y.GetComponent<unit>().Priority)
			return 1;
		else
			return -1;
	}



	public static Transform GetCanvas()
	{
		return GameObject.Find(GameConstants.HUD_CANVAS).transform;
	}

	public static void SpawnUnit(string prefab, Transform parent, Vector3 pos) // GameFunctions.SpawnUnit(cardInfo.Prefab.name, playerInfo.UnitTransform, pos);
	{
	//	pos.x = pos.x - 2f;
	//	pos.z = pos.z + 2f;
		Vector3 newPos = new Vector3(pos.x, pos.y, pos.z);
		Debug.Log(newPos);
		GameObject go = PhotonNetwork.Instantiate(prefab, newPos, Quaternion.identity, 0); // 10.5 Vector3.zero to pos
		go.GetComponent<unit>().enabled = true;
		go.tag = GameConstants.PLAYER_TAG;
		go.transform.SetParent(parent, true); // 9.5 commented
		go.transform.position = new Vector3(pos.x, pos.y, pos.z);
		GameManager.AddObject(go);
		//go.GetComponent<BoxCollider>().enabled = true;
	}

	public static AccountStats FoundPlayer(int mmr, AccountStats[] accountStats)
	{
		foreach (AccountStats acc in accountStats)
		{
			if(acc.looking && !acc.me)
			{
				if (mmr <= acc.trophies + 50 || mmr >= acc.trophies - 50) // try removng this
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
