using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustWalkCharacter : MonoBehaviour {

	private enum State
	{
		WalkLeft,WalkRight
	}

	[SerializeField]
	float speed, leftClamp , rightClamp;

	Rigidbody2D rigid;
	State state;

	void Awake()
	{
		rigid = GetComponent<Rigidbody2D> ();
		float x = Random.Range (0, 2);
		if (x < 1) 
		{
			state = State.WalkLeft;
			transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		else
		{
			state = State.WalkRight;
			transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x) *-1, transform.localScale.y, transform.localScale.z);
		}
			
	}

	void Update()
	{
		if (state == State.WalkLeft) 
		{

			rigid.velocity = new Vector2 (-1, 0) * speed;
			if (transform.position.x <= leftClamp) 
			{
				state = State.WalkRight;
				transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x)*-1, transform.localScale.y, transform.localScale.z);
			} 
		} 
		else if (state == State.WalkRight) 
		{
			rigid.velocity = new Vector2 (1, 0) * speed;
			if (transform.position.x >= rightClamp) 
			{
				state = State.WalkLeft;
				transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
			} 
		} 
	}
}
