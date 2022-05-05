using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor2D : MonoBehaviour
{
	[SerializeField]
	GameObject followTarget;
	[SerializeField]
	Animator anim;
	[SerializeField]
	NavMeshAgent agent;
	[SerializeField]
	bool isFlying;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		agent = followTarget.GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		if (!isFlying)
		{
			anim.SetBool("isWalking", agent.velocity == Vector3.zero ? false : true);
			if(Mathf.Abs(agent.velocity.z) >= Mathf.Abs(agent.velocity.x))
			{
				anim.SetFloat("TargetZ", -agent.velocity.z);
				anim.SetFloat("TargetX", 0);
			}
			else if(Mathf.Abs(agent.velocity.z) >= Mathf.Abs(agent.velocity.x))
			{
				anim.SetFloat("TargetX", agent.velocity.x);
				anim.SetFloat("TargetZ", 0);
			}
		}
	}


}
