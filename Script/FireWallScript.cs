using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallScript : MonoBehaviour 
{

	bool trigger;

	void OnTriggerStay2D(Collider2D other)
	{
		if ( (other.gameObject.tag == "Enemy" ) && gameObject != null && trigger ==false)
		{
			trigger = true;
			Instantiate (Resources.Load("_FireWallBomb"), transform.position , Quaternion.identity);
			Invoke ("ReloadFireWall", 0.5f);
		}
	}

	void ReloadFireWall()
	{
		trigger = false;
	}
	

}
