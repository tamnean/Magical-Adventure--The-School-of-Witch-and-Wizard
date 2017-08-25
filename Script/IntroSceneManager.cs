using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour 
{
	bool clickableDelay;

	void Awake()
	{
		Time.timeScale = 1;
	}

	void Start()
	{
		PlayerPrefs.GetInt ("LevelPassed", 0);
		Invoke ("ClickableDelay", 0.5f);
	}

	void ClickableDelay()
	{
		clickableDelay = true;
	}

	public void OnChangeScene(string sceneName)
	{
		if(clickableDelay)
			SceneManager.LoadScene (sceneName);	
	}
		

	public void OnSceneExit()
	{
		Application.Quit ();
	}


}
