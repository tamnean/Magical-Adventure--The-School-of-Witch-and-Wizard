using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallDamage : MonoBehaviour 
{

	[SerializeField]
	float damage;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy" && this.gameObject != null) 
		{
			EventManager.EnemyTakeDamage.Invoke (other.gameObject, damage);
		}
	}
}
