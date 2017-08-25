using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour 
{
	public Text textMain;

	[SerializeField]
	string[] text;

	int i=0;

	void Start () 
	{
		InvokeRepeating ("NPCTalk", 0, 4);
	}

	void NPCTalk()
	{
		text[i] = text [i].Replace ("\\n", "\n");
		textMain.text = ""+text [i];
		i++;
		if (i > text.Length-1)
			i = 0;
	}


}
