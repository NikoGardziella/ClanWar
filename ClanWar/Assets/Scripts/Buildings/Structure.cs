using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour, IDamageable
{
	public baseStats Stats
	{
		get
		{
			throw new System.NotImplementedException();
		}
	}

	public List<GameObject> HitTargets
	{
		get 
		{
			throw new System.NotImplementedException();
		}
	}

	public GameObject Target
	{
		get
		{
			throw new System.NotImplementedException();
		}
		set
		{
			throw new System.NotImplementedException();
		}
	}

	public void TakeDamage(float amount)
	{
		throw new System.NotImplementedException();
	}
}
