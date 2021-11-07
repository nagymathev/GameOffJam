using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
	public int team;
	public int health = 100;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		//ToDo: display current health overhead
	}

	public void Hit(int damage)
	{
		//ToDo: particles, sound, etc
		health -= damage;

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		//ToDo: particles, sound, etc
		Destroy(this.gameObject);
	}
}
