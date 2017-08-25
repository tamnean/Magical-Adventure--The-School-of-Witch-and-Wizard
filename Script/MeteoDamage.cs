using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoDamage : MonoBehaviour 
{
	[SerializeField]
	float damage;

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy" && this.gameObject != null) 
		{
			EventManager.EnemyTakeDamage.Invoke (other.gameObject, damage);
		}
		GetComponent<Collider2D> ().enabled = false;
	}
}
