using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : Photon.MonoBehaviour, IDamageable
{
	[SerializeField]
	private Actor3D agent;
	[SerializeField]
	private GameObject target;
	[SerializeField]
	private baseStats stats;
	[SerializeField]
	private List<GameObject> hitTargets;

	public List<GameObject> HitTargets
	{
		get { return hitTargets; }
	}


	public baseStats Stats
	{
		get { return stats; }
	}


	public Actor3D Agent
	{
		get { return agent; }
	}
	public GameObject Target
	{
		get { return target; }
		set { target = value; }
	}



	baseStats IDamageable.Stats
	{
		get { return stats; } // changed

	}

	void IDamageable.TakeDamage(float amount)
	{
		stats.CurrentHealth -= amount;
	}


	private void Start()
	{
		List<GameObject> objects = GameManager.Instance.Objects;
		objects = GameManager.GetAllEnemies(transform.position, objects, gameObject.tag);
		target = GameFunctions.GetNearestTarget(objects, stats.DetectionObject, gameObject.tag);
	}
	private void Update()
	{
		if (stats.CurrentHealth > 0)
		{
			agent.Agent.speed = stats.MovementSpeed;
			stats.UpdateStats();
			Attack();
			if (target != null)
				agent.Agent.SetDestination(target.transform.position);
		}
		else
		{
			print(gameObject.name + "has Died");
			GameManager.RemoveObjectFromList(gameObject);
			Destroy(gameObject);
		}
	}

	void Attack()
	{
		if (target != null)
		{
			List<GameObject> objects = GameManager.Instance.Objects;
			objects = GameManager.GetAllEnemies(transform.position, objects, gameObject.tag);
			target = GameFunctions.GetNearestTarget(objects, stats.DetectionObject, gameObject.tag); // error
			if (target == null)
				Debug.LogError("target null");
			if (stats.CurrentAttackDelay >= stats.AttackDelay)
			{
				Component damageable = target.GetComponent(typeof(IDamageable));

				if (damageable)
				{
					if (hitTargets.Contains(target))
					{
						if (GameFunctions.CanAttack(gameObject.tag, target.tag, damageable, stats))
						{
							GameFunctions.Attack(damageable, stats.BaseDamage);
							stats.CurrentAttackDelay = 0;
						}
						Debug.Log("CanT Attack");
					}
					//else
					//	Debug.Log("hitTargets does not Contains(target):" + target);
				}
			}

		}
		else
		{
			List<GameObject> objects = GameManager.Instance.Objects;
			objects = GameManager.GetAllEnemies(transform.position, objects, gameObject.tag);
			target = GameFunctions.GetNearestTarget(objects, stats.DetectionObject, gameObject.tag);
		}
	}

	public void OnTriggerEnter(Collider other) // 13.5 does not work
	{
		Debug.Log("OnTriggerEnter : " + other);
		if (!other.CompareTag(gameObject.tag)) // 10.5  if (!other.transform.parent.parent.CompareTag(gameObject.tag))
		{
			Component damageable = other.gameObject.GetComponent(typeof(IDamageable)); // Component damageable = other.transform.parent.parent.gameObject.GetComponent(typeof(IDamageable));
			if (damageable)
			{
				if (!hitTargets.Contains(damageable.gameObject))
				{
					hitTargets.Add(damageable.gameObject);
				}
				else
					Debug.Log("hitTargets.Contains(damageable.gameObject");
			}
			else
				Debug.Log("not damageable");
		}
	}

	public void OnTriggerStay(Collider other)
	{
		Debug.Log("OnTriggerStay" + other);
		if (!other.gameObject.CompareTag(gameObject.tag))
		{
			if (hitTargets.Count > 0)
			{
				GameObject go = GameFunctions.GetNearestTarget(hitTargets, stats.DetectionObject, gameObject.tag, stats.Range);

				if (go != null)
				{
					target = go;
				}
				else
					Debug.Log("go is null");
			}
			else
				Debug.Log("not hit targets");
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			if (photonView.isMine)
			{
			stream.SendNext(stats.CurrentHealth);
			stream.SendNext(stats.HealthBar.fillAmount);
			}
		}
		else
		{
		//	if (!photonView.isMine)
			//{
			//	stats.CurrentHealth = (float)stream.ReceiveNext();
			//	stats.HealthBar.fillAmount = (float)stream.ReceiveNext();
			//}

		}
	}

}
