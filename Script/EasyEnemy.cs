using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemy : MonoBehaviour 
{
	private enum State
	{
		WalkToPlayer,AttackNearPlayer,Idle,Die
	}

	[SerializeField]
	float hp, speed, detectRange, AttackRange;

	bool checkTrigger;
	Animator anim;
	GameObject player;
	Rigidbody2D rigid;
	State state = State.Idle;


	void Awake () 
	{
		EventManager.EnemyTakeDamage.AddListener (OnTakeDamage);
		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("Player");
	
	}

	void Update()
	{
		Vector3 distance = player.transform.position - transform.position;
		float range = distance.magnitude;


		if (state == State.Idle && hp > 0) 
		{
			anim.SetBool ("Idle", true);
			anim.SetBool ("Walk", false);
			anim.SetBool ("Attack", false);
			anim.applyRootMotion = false;

			if (range <= detectRange) 
			{
				state = State.WalkToPlayer;
			} 
		} 
		else if (state == State.WalkToPlayer && hp > 0) 
		{
			anim.SetBool ("Walk", true);
			anim.SetBool ("Idle", false);
			anim.SetBool ("Attack", false);

			anim.applyRootMotion = false;

			rigid.velocity = distance.normalized * speed;

			if (distance.x > 0 && hp > 0 )
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
			
			anim.SetBool ("Attack", true);
			anim.SetBool ("Idle", false);
			anim.SetBool ("Walk", false);

			anim.applyRootMotion = true;
			state = State.WalkToPlayer ;
		} 
			
	}

	void OnTakeDamage(GameObject self,float damage)
	{
		if(transform.gameObject == self)
		{
			hp = hp - damage;
			if (hp <= 0) 
			{
				anim.SetBool ("Walk", false);
				anim.SetBool ("Idle", false);
				anim.SetBool ("Attack", false);
				anim.SetTrigger ("Die");
				rigid.constraints = RigidbodyConstraints2D.FreezeAll;
				GetComponent<Collider2D> ().enabled = false;
				Destroy (gameObject, 4);
			} 
			else 
			{
				anim.SetTrigger ("Hurt");
				anim.applyRootMotion = true;
			}
		}
	}
	


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player" && !checkTrigger) 
		{
			checkTrigger = true;
			EventManager.PlayerDie.Invoke ();
		}
	}


}
