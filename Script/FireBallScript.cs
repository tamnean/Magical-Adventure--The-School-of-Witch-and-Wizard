﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour 
{
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if ( (other.gameObject.tag == "Enemy"  ||  other.gameObject.tag == "Ground" ) && gameObject != null )
		{
			Instantiate (Resources.Load("_FirezBallBomb"), transform.position , Quaternion.identity);
			Destroy (gameObject);
		}
	}

		
}
