using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class baseStats
{
	private float currentHealth;
	private float maxHealth;
	private float range;
	private float baseDamage;
	private float attackDelay;
	private float currentAttackDelay;
	private float movementSpeed;
	private Image healthBar;

	public Image HealthBar
	{
		get { return healthBar; }
		set { healthBar = value; }
	}


	public float MovementSpeed
	{
		get { return movementSpeed; }
		set { movementSpeed = value; }
	}


	public float CurrentAttackDelay
	{
		get { return currentAttackDelay; }
		set { currentAttackDelay = value; }
	}



	public float AttackDelay
	{
		get { return attackDelay; }
		set { attackDelay = value; }
	}


	public  float BaseDamage
	{
		get { return baseDamage; }
		set { baseDamage = value; }
	}


	public float Range
	{
		get { return range; }
		set { range = value; }
	}


	public float MaxHealth
	{
		get { return maxHealth; }
		set { maxHealth = value; }
	}


	public float CurrentHealth
	{
		get { return currentHealth; }
		set { currentHealth = value; }
	}

}


