using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMoveScript : MonoBehaviour 
{

	GameObject player;
	Rigidbody2D playerRigid;
	float speed = 5;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerRigid = player.GetComponent<Rigidbody2D> ();
	}

	void OnMouseDrag()
	{
		playerRigid.velocity = new Vector2(-1,0) * speed *Time.deltaTime*100;
	}
}
