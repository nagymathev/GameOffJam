using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : Damageable
{
	public bool attackUnits = true;
	public bool attackTowers = true;
	public bool attackBase = true;

	public float attackRange = 5.0f;
	public SphereCollider sensor;

	public NavMeshAgent agent;
	public GameObject moveTarget;

	public float reloadTimer;
	public Damageable currentTarget;


	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();

		//must find the base of the OPPOSITE team
		//ToDo: potentially find different (move)targets depending on what kind of unit this is
		Base[] bases = GameObject.FindObjectsOfType<Base>();
		foreach (Base b in bases)
		{
			if (b.team != this.team)
			{
				moveTarget = b.gameObject;
				break;
			}
		}

		if (sensor == null)
		{
			sensor = GetComponentInChildren<SphereCollider>();
		}
		if (sensor)
		{
			attackRange = sensor.radius + 1.0f;
		}
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

	void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger) return;    //ignore triggers
		Debug.Log("OTE " + this.name + "-" + other.name, other);

		Unit otherUnit = other.GetComponentInParent<Unit>();
		Tower otherTower = other.GetComponentInParent<Tower>();
		Base otherBase = other.GetComponentInParent<Base>();

		if (currentTarget == null)
		{
			currentTarget = otherUnit;
		}

	}

}
