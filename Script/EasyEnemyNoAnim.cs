using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemyNoAnim : MonoBehaviour 
{
	private enum State
	{
		WalkToPlayer,AttackNearPlayer,Idle,Die
	}

	[SerializeField]
	float hp, speed, detectRange, AttackRange;

	bool checkTrigger;
	GameObject player;
	Rigidbody2D rigid;
	State state = State.Idle;


	void Awake () 
	{
		EventManager.EnemyTakeDamage.AddListener (OnTakeDamage);
		rigid = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("Player");

	}

	void Update()
	{
		Vector3 distance = player.transform.position - transform.position;
		float range = distance.magnitude;

		if (state == State.Idle && hp > 0) 
		{
			if (range <= detectRange) 
			{
				state = State.WalkToPlayer;
			} 
		} 
		else if (state == State.WalkToPlayer && hp > 0) 
		{

			rigid.velocity = distance.normalized * speed;				
				
			if (distance.x > 0 )
				transform.localScale = new Vector3(Mathf.Abs (transform.localScale.x)*-1 , transform.localScale.y, transform.localScale.z) ;
			else if(distance.x < 0 && hp > 0)
				transform.localScale = new Vector3(Mathf.Abs (transform.localScale.x) , transform.localScale.y, transform.localScale.z) ;

			if (range <= AttackRange) 
			{
				state = State.AttackNearPlayer;
			} 
			else if(range > detectRange) 
			{
				state = State.Idle;
			}
		}
		else if (state == State.AttackNearPlayer && hp > 0) 
		{
			if (distance.x > 0 && hp > 0)
				transform.localScale = new Vector3(Mathf.Abs (transform.localScale.x)*-1 , transform.localScale.y, transform.localScale.z) ;
			else if (distance.x < 0 && hp > 0)
				transform.localScale = new Vector3(Mathf.Abs (transform.localScale.x) , transform.localScale.y, transform.localScale.z) ;
			state = State.WalkToPlayer ;
		} 

	}

	void OnTakeDamage(GameObject self,float damage)
	{
		if(transform.gameObject == self)
		{
			hp = hp - damage;
			bool dieCheck = true;
			if (hp <= 0 && dieCheck) 
			{
				dieCheck = false;
				rigid.constraints = RigidbodyConstraints2D.FreezeAll;
				Instantiate(Resources.Load("EnemyDeathGhost"),transform.position - new Vector3(0,0,-1), Quaternion.Euler(new Vector3(-90,0,0)) );
				GetComponent<Collider2D> ().enabled = false;
				Destroy (gameObject, 1);
			} 
		}
	}
		

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && !checkTrigger) 
		{
			checkTrigger = true;
			EventManager.PlayerDie.Invoke ();
		}			
	}


}
