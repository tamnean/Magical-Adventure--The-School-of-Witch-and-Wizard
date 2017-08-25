using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCharmsTestScript : MonoBehaviour 
{
	GameObject RightImage,XImage,WWW,WWF,WWL,FFF,FFW,FFL,LLL,LLW,LLF,WFL;
	bool _WWW,_WWF,_WWL,_FFF,_FFW,_FFL,_LLL,_LLW,_LLF,_WFL;

	[SerializeField]
	int sceneLevel;

	int WFLCount=0;

	void Awake () 
	{
		XImage = GameObject.Find ("XImage");
		RightImage = GameObject.Find ("RightImage");
		WWW = GameObject.Find ("WWW");
		WWF = GameObject.Find ("WWF");
		WWL = GameObject.Find ("WWL");
		FFF = GameObject.Find ("FFF");
		FFW = GameObject.Find ("FFW");
		FFL = GameObject.Find ("FFL");
		LLL = GameObject.Find ("LLL");
		LLW = GameObject.Find ("LLW");
		LLF = GameObject.Find ("LLF");
		WFL = GameObject.Find ("WFL");
		RightImage.SetActive (false);
		XImage.SetActive (false);
		WWW.SetActive(false);
		WWF.SetActive(false);
		WWL.SetActive(false);
		FFF.SetActive(false);
		FFW.SetActive(false);
		FFL.SetActive(false);
		LLL.SetActive(false);
		LLW.SetActive(false);
		LLF.SetActive(false);
		WFL.SetActive(false);
	}

	void Start()
	{
		EventManager.MagicCast.AddListener (OnCast);
	}

	void OnCast(string spell)
	{
		if (spell == "WWW" && !_WWW) 
		{
			_WWW = true;
			WWW.SetActive(true);
		} 
		else if (spell == "WWF" && !_WWF) 
		{
			_WWF = true;
			WWF.SetActive(true);
		}
		else if (spell == "WWL" && !_WWL)
		{
			_WWL = true;
			WWL.SetActive(true);
		}
		else if (spell == "FFF" && !_FFF) 
		{
			_FFF = true;
			FFF.SetActive(true);
		} 
		else if (spell == "FFW" && !_FFW) 
		{
			_FFW = true;
			FFW.SetActive(true);
		} 
		else if (spell == "FFL" && !_FFL) 
		{
			_FFL = true;
			FFL.SetActive(true);
		}
		else if (spell == "LLL" && !_LLL) 
		{
			_LLL = true;
			LLL.SetActive(true);
		}
		else if (spell == "LLW" && !_LLW) 
		{
			_LLW = true;
			LLW.SetActive(true);
		}
		else if (spell == "LLF" && !_LLF)
		{
			_LLF = true;
			LLF.SetActive(true);
		} 
		else if (spell == "WFL" && !_WFL) 
		{
			if (WFLCount < 1)
				WFLCount++;
			else 
			{
				_WFL = true;
				WFL.SetActive(true);
			}
		} 
		else 
		{
			XImage.SetActive (true);
			Invoke ("OnCastWrong",3);
		}

		StartCoroutine (OnPassCheck ());
	}


	void OnCastWrong()
	{
		EventManager.PlayerDie.Invoke ();
	}

	IEnumerator OnPassCheck()
	{
		if (_WWW && _WWF && _WWL && _FFF && _FFW && _FFL && _LLL && _LLW && _LLF && _WFL)
		{
			RightImage.SetActive (true);
			yield return new WaitForSeconds (3);
			EventManager.PlayerPass.Invoke ();
			if(PlayerPrefs.GetInt ("LevelPassed") < sceneLevel )
				PlayerPrefs.SetInt ("LevelPassed", sceneLevel);
		}
			
	}


	

}
