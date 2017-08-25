using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmTestEach : MonoBehaviour 
{
	public GameObject door;
	[SerializeField]
	bool WWW_Windy, WWF_Float, WWL_Windrun, FFF_Meteorite, FFW_FireWall, FFL_FireBall, LLL_Thunder, LLW_Blink, LLF_Bolt, WFL_LittleBig;

	void Awake () 
	{
		EventManager.MagicCast.AddListener (MagicCheck);
		door.SetActive (false);
	}

	void MagicCheck(string spell)
	{
		if (spell == "WWW" && WWW_Windy)
			door.SetActive (true);
		else if (spell == "WWF" && WWF_Float)
			door.SetActive (true);
		else if (spell == "WWL" && WWL_Windrun)
			door.SetActive (true);
		else if (spell == "FFF" && FFF_Meteorite)
			door.SetActive (true);
		else if (spell == "FFW" && FFW_FireWall)
			door.SetActive (true);
		else if (spell == "FFL" && FFL_FireBall)
			door.SetActive (true);
		else if (spell == "LLL" && LLL_Thunder)
			door.SetActive (true);
		else if (spell == "LLW" && LLW_Blink)
			door.SetActive (true);
		else if (spell == "LLF" && LLF_Bolt)
			door.SetActive (true);
		else if (spell == "WFL" && WFL_LittleBig)
			door.SetActive (true);
		
	}
	


}
