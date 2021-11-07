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
		agent.avoidancePriority = Random.Range(1, 99);
		agent.speed = agent.speed * Random.Range(0.9f, 1.1f);
		agent.acceleration = agent.acceleration * Random.Range(0.9f, 1.1f);

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
		agent.avoidancePriority = Random.Range(1, 99);

		if (moveTarget)
		{
			Vector3 targetPos = moveTarget.transform.position;
			if (!agent.hasPath || agent.isPathStale
				|| (agent.destination - targetPos).sqrMagnitude > 1.0f)
			agent.SetDestination(targetPos);
		}

		//from here it's pretty much copy-paste from tower
		if (currentTarget != null)
		{
			Debug.DrawLine(transform.position, currentTarget.transform.position, Color.white);
			if ((currentTarget.transform.position - sensor.transform.position).magnitude > attackRange)
				currentTarget = null;
		}

		if (reloadTimer > 0)
		{
			reloadTimer -= Time.deltaTime;
		} else
		{
			if (currentTarget != null)
			{
				//ToDo: particles, sound, etc
				currentTarget.Hit(1);
				reloadTimer = 1.0f;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger) return;    //ignore triggers
		Debug.Log("OTE " + this.name + "-" + other.name, other);

		Damageable target = other.GetComponentInParent<Damageable>();
		if (!target) return;    //ignore if no damageable
		if (target.team == this.team) return;   //don't attack own team
		if (!attackUnits && target is Unit) target = null;
		if (!attackTowers && target is Tower) target = null;
		if (!attackBase && target is Base) target = null;

		if (currentTarget == null)
		{
			currentTarget = target;
		}

	}

}
