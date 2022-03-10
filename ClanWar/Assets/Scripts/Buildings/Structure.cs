using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour, IDamageable
{
	[SerializeField]
	private baseStats stats;
	[SerializeField]
	private List<GameObject> hitTargets;
	[SerializeField]
	GameObject target;
	public baseStats Stats
	{
		get
		{
			return stats;
		}
	}

	public List<GameObject> HitTargets
	{
		get
		{
			return hitTargets;
		}
	}

	public GameObject Target
	{
		get
		{
			return target;
		}
		set
		{
			target = value;
		}
	}

	void IDamageable.TakeDamage(float amount)
	{
		stats.CurrentHealth -= amount;
	}

	void Update()
	{
		if (stats.CurrentHealth > 0)
		{
			stats.UpdateStats();
			Attack();
		}
	}
	void Attack()
	{
		if (target != null)
		{
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
					}
				}
			}
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (!other.transform.parent.parent.CompareTag(gameObject.tag))
		{
			Component damageable = other.transform.parent.parent.gameObject.GetComponent(typeof(IDamageable));
			if (damageable)
			{
				if (!hitTargets.Contains(damageable.gameObject))
				{
					hitTargets.Add(damageable.gameObject);
				}
			}
		}
	}

	public void OnTriggerStay(Collider other)
	{
		if (!other.gameObject.CompareTag(gameObject.tag))
		{
			if (hitTargets.Count > 0)
			{
				GameObject go = GameFunctions.GetNearestTarget(hitTargets, stats.DetectionObject, gameObject.tag, stats.Range);

				if (go != null)
				{
					target = go;
				}
			}
		}
	}
}
