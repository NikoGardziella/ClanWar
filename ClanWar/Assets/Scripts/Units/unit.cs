using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : MonoBehaviour, IDamageable
{
	[SerializeField]
	private Actor3D agent;
	[SerializeField]
	private Actor2D unitSprite;
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
	public Actor2D UnitSprite
	{
		get { return unitSprite; }
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
	List<GameObject> IDamageable.HitTargets => throw new System.NotImplementedException();

	GameObject IDamageable.Target { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

	void IDamageable.TakeDamage(float amount)
	{
		stats.CurrentHealth -= amount;
	}


	private void Start()
	{
		List<GameObject> objects = GameManager.Instance.Objects;
		objects = GameManager.GetAllEnemies(transform.position, objects, gameObject.tag);
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
	}

	void Attack()
	{
		if (target != null)
		{
			if(stats.CurrentAttackDelay >= stats.AttackDelay)
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
			if(hitTargets.Count > 0)
			{
				GameObject go = GameFunctions.GetNearestTarget(hitTargets, stats.DetectionObject, gameObject.tag, stats.Range);
				
				if(go != null)
				{
					target = go;
				}
			}
		}
	}
}
