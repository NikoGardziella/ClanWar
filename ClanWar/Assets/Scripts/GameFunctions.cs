using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFunctions
{
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
}
