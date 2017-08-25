using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtacleCheckDie : MonoBehaviour 
{
	bool check;

	void OnCollisionEnter2D(Collision2D other)
	{
		if ((other.gameObject.tag == "Player" || other.gameObject.tag == "ItemGoal") && !check) 
		{
			check = true;
			EventManager.PlayerDie.Invoke ();
		} 
		else if (other.gameObject.tag == "Trap") 
		{
			Destroy (other.gameObject);
		} 
		else if (other.gameObject.tag == "Enemy" ) 
		{
			EventManager.EnemyTakeDamage.Invoke (other.gameObject, 10000);
		}
	}
}
