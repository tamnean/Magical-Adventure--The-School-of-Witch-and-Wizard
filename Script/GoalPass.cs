using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPass : MonoBehaviour 
{
	


	[SerializeField]
	bool instance, killAllEnemy;
	[SerializeField]
	int levelPass;



	GameObject[] enemyInScene;
	GameObject player;


	void Start()
	{
		player = GameObject.Find ("Player");
		if (instance)
			transform.gameObject.SetActive (true);
		else
			transform.gameObject.SetActive (false);

		InvokeRepeating ("CheckCondition", 1, 1);
	}

	void CheckCondition()
	{
		if (!instance) 
		{
			if (killAllEnemy)
			{
				enemyInScene = GameObject.FindGameObjectsWithTag ("Enemy");
				if (enemyInScene.Length == 0)
				{
					transform.position = player.transform.position + new Vector3 (0, 10,0);
					transform.gameObject.SetActive (true);
					CancelInvoke ("CheckCondition");
					
				}
			}

		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			if(PlayerPrefs.GetInt ("LevelPassed") < levelPass )
				PlayerPrefs.SetInt ("LevelPassed", levelPass);
			EventManager.PlayerPass.Invoke ();
			
		}
	}
}
