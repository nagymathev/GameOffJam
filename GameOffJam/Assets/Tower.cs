using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Damageable
{
	public float attackRange = 5.0f;
	public SphereCollider sensor;

	public float reloadTimer;
	public Damageable currentTarget;

	void Start ()
	{
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
				currentTarget.Hit(10);
				reloadTimer = 1.0f;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger) return;	//ignore triggers

		Debug.Log("OTE " + this.name + "-" + other.name, other);

		Damageable target = other.GetComponentInParent<Damageable>();
		if (!target) return;    //ignore if no damageable
		if (target.team == this.team) return;	//don't attack own team

		if (currentTarget == null)
		{
			currentTarget = target;
		}
	}

}
