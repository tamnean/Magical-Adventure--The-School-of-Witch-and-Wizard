using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteScript : MonoBehaviour 
{

	bool trigger;

	void OnTriggerEnter2D(Collider2D other)
	{
		if ( (other.gameObject.tag == "Ground" ) && gameObject != null && trigger ==false)
		{
			trigger = true;
			Instantiate (Resources.Load("_FMeteoriteBomb"),transform.position,Quaternion.identity);
			Destroy (gameObject);

		}
	}
}
