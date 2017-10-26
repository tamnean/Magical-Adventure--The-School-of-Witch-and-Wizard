using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockManager : MonoBehaviour 
{
	public Button[] levelButton;

	void Awake()
	{
		int levelPass = PlayerPrefs.GetInt ("LevelPassed",0);  // pass 0 then 1 unlock

		for (int i = 0; i < levelButton.Length ; i++) 
		{
			if (i > levelPass) 
			{	
				levelButton[i].interactable = false;
			}
		}

	}
	

}
