using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSongDelay : MonoBehaviour 
{

	AudioSource audioSource;

	void Start () 
	{
		audioSource = GetComponent<AudioSource> ();
		audioSource.PlayDelayed (2f);
		
	}

}
