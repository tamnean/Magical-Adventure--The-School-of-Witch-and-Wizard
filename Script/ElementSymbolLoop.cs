using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSymbolLoop : MonoBehaviour 
{
	public GameObject[] elementSymbol;
	public float intervalTime;
	int i=-1;

	void Start () 
	{
		InvokeRepeating ("RunLoop", 0, intervalTime);
	}

	void RunLoop()
	{
		if(i>=0)
			elementSymbol [i].SetActive (false);
		
		i++;
		if (i >= elementSymbol.Length)
			i = 0;
		
		if(i < elementSymbol.Length) 
		{
			elementSymbol [i].SetActive (true);


		}
	}


}
