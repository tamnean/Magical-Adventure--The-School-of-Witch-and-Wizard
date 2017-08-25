using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockManager : MonoBehaviour {


	public Button[] levelButton;

	public GameObject[] ground;

	void Start () 
	{
		int levelPass = PlayerPrefs.GetInt ("LevelPassed");  // pass 0 then 1 unlock

		for (int i = 0; i < levelButton.Length ; i++) 
		{
			ground[i].SetActive (true);
			if (i > levelPass) 
			{	
				levelButton[i].interactable = false;
				ground [i].GetComponent<Image> ().color = Color.gray;
			}
		}

	}
	

}
