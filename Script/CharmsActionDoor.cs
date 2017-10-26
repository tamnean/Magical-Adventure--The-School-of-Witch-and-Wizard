using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmsActionDoor : MonoBehaviour 
{
	public GameObject door;

	[SerializeField]
	bool _WWW,_WWF,_WWL,_FFF,_FFW,_FFL,_LLL,_LLW,_LLF,_WFL;

	void Awake () 
	{
		EventManager.MagicCast.AddListener (MagicCheck);
		door.SetActive (false);
	}

	void MagicCheck(string spell)
	{
		if(spell == "WWW" && _WWW)
			door.SetActive (true);
		else if(spell == "WWF" && _WWF)
			door.SetActive (true);
		else if(spell == "WWL" && _WWL)
			door.SetActive (true);
		else if(spell == "FFF" && _FFF)
			door.SetActive (true);
		else if(spell == "FFW" && _FFW)
			door.SetActive (true);
		else if(spell == "FFL" && _FFL)
			door.SetActive (true);
		else if(spell == "LLL" && _LLL)
			door.SetActive (true);
		else if(spell == "LLW" && _LLW)
			door.SetActive (true);
		else if(spell == "LLF" && _LLF)
			door.SetActive (true);
		else if(spell == "WFL" && _WFL)
			door.SetActive (true);
	}
}
