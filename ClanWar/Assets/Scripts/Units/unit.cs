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



	baseStats IDamageable.Stats => throw new System.NotImplementedException();

	List<GameObject> IDamageable.HitTargets => throw new System.NotImplementedException();

	GameObject IDamageable.Target { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

	void IDamageable.TakeDamage(float amount)
	{
		stats.CurrentHealth -= amount;
	}

	private void Update()
	{
		if (stats.CurrentHealth > 0)
		{
			agent.Agent.speed = stats.MovementSpeed;
			stats.UpdateStats();
			if (target != null)
				agent.Agent.SetDestination(target.transform.position);
		}
	}
}
