using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
	public GameObject moveTarget;
	public NavMeshAgent agent;

	void Start ()
	{
		moveTarget = GameObject.Find("Base");
		agent = GetComponent<NavMeshAgent>();

	}
	
	void Update ()
	{
		if (agent && moveTarget)
		{
			Vector3 targetPos = moveTarget.transform.position;
			if (!agent.hasPath || agent.isPathStale
				|| (agent.destination - targetPos).sqrMagnitude > 1.0f)
			agent.SetDestination(targetPos);
		}
	}
}
