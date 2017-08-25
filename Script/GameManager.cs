using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour 
{
	
	[SerializeField]
	float timeMax;
	[SerializeField]
	bool WWW_Windy, WWF_float, WWL_Windrun, FFF_Meteorite, FFW_FireWall, FFL_FireBall, LLL_Thunder, LLW_Blink, LLF_Bolt, WFL_LittleBig;

	float playerSpeed = 5 ;
	bool moveLeftClick;
	bool moveRightClick;
	bool invokeClick;
	float coolDownInvoke;
	bool small;


	GameObject walkEffect;
	GameObject stunEffect;
	Transform blinkPoint;

	GameObject player;
	Rigidbody2D playerRigid;
	Animator playerAnim;
	List<string> orbControl;
	int currentIndexOrb=0;
	GameObject windOrb_1;
	GameObject windOrb_2;
	GameObject windOrb_3;
	GameObject fireOrb_1;
	GameObject fireOrb_2;
	GameObject fireOrb_3;
	GameObject electricOrb_1;
	GameObject electricOrb_2;
	GameObject electricOrb_3;

	Button invokeButton;
	Button windButton;
	Button fireButton;
	Button electricButton;
	Text spellText;
	GameObject textBox;
	GUIAnim textBoxGuiAnim;
	Text timeText;

	GameObject pausePanel;
	GameObject failPanel;
	GameObject clearPanel;
	GameObject pauseMenu;
	GameObject bookWWW_1;
	GameObject bookWWF_2;
	GameObject bookWWL_3;
	GameObject bookFFF_4;
	GameObject bookFFW_5;
	GameObject bookFFL_6;
	GameObject bookLLL_7;
	GameObject bookLLW_8;
	GameObject bookLLF_9;
	GameObject bookWFL_10;

	AudioSource audioSource;
	AudioSource runAudioSource;
	AudioSource themeAudioSource;


	void Awake()
	{
		Time.timeScale = 1;
		EventManager.PlayerDie.AddListener (OnPlayerDie);
		EventManager.PlayerPass.AddListener (OnPlayerPass);
		EventManager.GoalCheckPoint.AddListener (OnChangeMenuClick);

		player = GameObject.FindGameObjectWithTag("Player");
		playerRigid = player.GetComponent<Rigidbody2D> ();
		playerAnim = player.GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> (); 
		runAudioSource = GameObject.Find ("RunSound").GetComponent<AudioSource>();
		themeAudioSource = GameObject.Find ("ThemeSong").GetComponent<AudioSource> ();


		orbControl = new List<string>();
		orbControl.Add ("x");		
		orbControl.Add ("x");
		orbControl.Add ("x");

		windOrb_1 = player.transform.FindChild ("WindOrb_1").gameObject;
		windOrb_2 = player.transform.FindChild ("WindOrb_2").gameObject;
		windOrb_3 = player.transform.FindChild ("WindOrb_3").gameObject;
		fireOrb_1 = player.transform.FindChild ("FireOrb_1").gameObject;
		fireOrb_2 = player.transform.FindChild ("FireOrb_2").gameObject;
		fireOrb_3 = player.transform.FindChild  ("FireOrb_3").gameObject;
		electricOrb_1 = player.transform.FindChild  ("ElectricOrb_1").gameObject;
		electricOrb_2 = player.transform.FindChild  ("ElectricOrb_2").gameObject;
		electricOrb_3 = player.transform.FindChild ("ElectricOrb_3").gameObject;

		walkEffect = player.transform.FindChild ("SmokeWalk").gameObject;
		stunEffect = player.transform.FindChild ("StunEffect").gameObject;
		blinkPoint = player.transform.FindChild ("BlinkPoint").transform;

		invokeButton = GameObject.Find ("ButtonInvoke").GetComponent<Button> ();
		windButton = GameObject.Find ("ButtonWind").GetComponent<Button> ();
		fireButton = GameObject.Find ("ButtonFire").GetComponent<Button> ();
		electricButton =  GameObject.Find ("ButtonElectric").GetComponent<Button> ();
		textBox = GameObject.Find ("TextBox");
		spellText = GameObject.Find ("SpellText").GetComponent<Text> ();
		textBoxGuiAnim = textBox.GetComponent<GUIAnim> ();
		textBox.SetActive (false);
		timeText = GameObject.Find ("TimeText").GetComponent<Text>();
		timeText.text = "" + timeMax;

		pausePanel = GameObject.Find ("PausePanel");
		failPanel =  GameObject.Find ("FailPanel");
		clearPanel = GameObject.Find ("ClearPanel");
		pauseMenu = GameObject.Find ("PauseMenu");
		bookWWW_1 = GameObject.Find ("BookWWW_1");
		bookWWF_2 = GameObject.Find ("BookWWF_2");
		bookWWL_3 = GameObject.Find ("BookWWL_3");
		bookFFF_4 = GameObject.Find ("BookFFF_4");
		bookFFW_5 = GameObject.Find ("BookFFW_5");
		bookFFL_6 = GameObject.Find ("BookFFL_6");
		bookLLL_7 = GameObject.Find ("BookLLL_7");
		bookLLW_8 = GameObject.Find ("BookLLW_8");
		bookLLF_9 = GameObject.Find ("BookLLF_9");
		bookWFL_10 = GameObject.Find ("BookWFL_10");
		pauseMenu.SetActive (false);
		pausePanel.SetActive (false);
		failPanel.SetActive (false);
		clearPanel.SetActive (false);
		bookWWW_1.SetActive (false);
		bookWWF_2.SetActive (false);
		bookWWL_3.SetActive (false);
		bookFFF_4.SetActive (false);
		bookFFW_5.SetActive (false);
		bookFFL_6.SetActive (false);
		bookLLL_7.SetActive (false);
		bookLLW_8.SetActive (false);
		bookLLF_9.SetActive (false);
		bookWFL_10.SetActive (false);



		InvokeRepeating ("TimeCount" ,1, 1);
	}


	void FixedUpdate()
	{
		if (moveLeftClick) 
		{
			playerRigid.velocity += new Vector2 (-playerSpeed * Time.deltaTime ,0);
		} 
		else if (moveRightClick) 
		{
			playerRigid.velocity += new Vector2 (playerSpeed * Time.deltaTime ,0);
		} 

	}

	void TimeCount()
	{
		if (timeMax <= 0) 
		{
			OnPlayerDie ();
			CancelInvoke ("TimeCount");
		}
		timeText.text = "" + timeMax;
		timeMax--;

	}


	public void OnMoveLeftButtonClick()
	{
		moveLeftClick = true;
		playerAnim.SetBool ("Idle", false);
		playerAnim.SetBool ("Run", true);
		player.transform.localScale  = new Vector3( Mathf.Abs(player.transform.localScale.x) ,player.transform.localScale.y,-1) ;
		walkEffect.transform.localEulerAngles = new Vector2 (90, 0);
		walkEffect.SetActive (true);
		runAudioSource.Play ();
	}

	public void OnMoveLeftButtonRelease()
	{
		moveLeftClick = false;
		playerAnim.SetBool ("Idle", true);
		playerAnim.SetBool ("Run", false);
		walkEffect.SetActive (false);
		runAudioSource.Stop ();

	}

	public void OnMoveRightButtonClick()
	{
		moveRightClick = true;
		playerAnim.SetBool ("Idle", false);
		playerAnim.SetBool ("Run", true);
		player.transform.localScale  = new Vector3( Mathf.Abs(player.transform.localScale.x)*-1, player.transform.localScale.y, -1) ;
		walkEffect.transform.localEulerAngles = new Vector2 (90, 180);
		walkEffect.SetActive (true);
		runAudioSource.Play ();
	}

	public void OnMoveRightButtonRelease()
	{
		moveRightClick = false;
		playerAnim.SetBool ("Idle", true);
		playerAnim.SetBool ("Run", false);
		walkEffect.SetActive (false);
		runAudioSource.Stop ();
	}

	public void OnInvokeMagicButtonClicked()
	{
		playerAnim.SetTrigger ("Attack 0");

		if (!invokeClick) 
		{
			StartCoroutine (OnInvokeMagic ());
		}

	}


	IEnumerator OnInvokeMagic()
	{
		audioSource.PlayOneShot ((AudioClip)Resources.Load ("Sound/_Invoke"));

		invokeClick = true;

		stunEffect.SetActive (false);
		invokeButton.interactable = false;
		windButton.interactable = false;
		fireButton.interactable = false;
		electricButton.interactable = false;

		coolDownInvoke = 2;

		GameObject clone1 = (GameObject)Instantiate (Resources.Load("_InvokeCastEffect"), player.transform.position + new Vector3 (0, 0, 0), Quaternion.Euler(70,0,0));
		clone1.transform.SetParent (player.transform);

		if ( (orbControl [0] == "W" && orbControl [1] == "W" && orbControl [2] == "W") && WWW_Windy) 
		{
			EventManager.MagicCast.Invoke ("WWW");
			spellText.text = "Let the storm rage on!";
			SpellTextEnable ();
			if (player.transform.localScale.x < 0)
				Instantiate (Resources.Load ("WWW_Windy"), player.transform.position + new Vector3 (-18, 5, 0), Quaternion.Euler (180, 89, 90));
			else if (player.transform.localScale.x > 0) 
			{
				GameObject clone =  (GameObject)Instantiate (Resources.Load ("WWW_Windy"), player.transform.position + new Vector3 (18, 5, 0), Quaternion.Euler (180, 269, 90));
				clone.GetComponent<AreaEffector2D> ().forceAngle = 270;
			}
		}
		else if( ( (orbControl [0] == "W" && orbControl [1] == "W" && orbControl [2] == "F")||
			(orbControl [0] == "W" && orbControl [1] == "F" && orbControl [2] == "W") ||
			(orbControl [0] == "F" && orbControl [1] == "W" && orbControl [2] == "W") ) && WWF_float )
		{
			EventManager.MagicCast.Invoke ("WWF");
			spellText.text = "I believe I can fly!";
			SpellTextEnable ();
			if(player.transform.localScale.x < 0)
				Instantiate (Resources.Load("WWF_Float"), player.transform.position + new Vector3 (5.5f, 0, -1), Quaternion.Euler(70,0,0));
			else if(player.transform.localScale.x > 0)
				Instantiate (Resources.Load("WWF_Float"), player.transform.position + new Vector3 (-5.5f, 0, -1), Quaternion.Euler(70,0,0));
		}
		else if( ( (orbControl [0] == "W" && orbControl [1] == "W" && orbControl [2] == "L")||
			(orbControl [0] == "W" && orbControl [1] == "L" && orbControl [2] == "W") ||
			(orbControl [0] == "L" && orbControl [1] == "W" && orbControl [2] == "W")) && WWL_Windrun )
		{
			EventManager.MagicCast.Invoke ("WWL");
			spellText.text = "May the force be with us!";
			SpellTextEnable ();
			if (playerSpeed <= 5) 
			{
				GameObject clone = (GameObject)Instantiate (Resources.Load("WWL_WindRun"), player.transform.position , Quaternion.Euler(180,0,0));
				playerSpeed = 10;
				clone.transform.SetParent (player.transform);
				Invoke ("SpeedDecrease", 8);
			}
			else
				spellText.text = "Effect remain";
		}
		else if( (orbControl [0] == "F" && orbControl [1] == "F" && orbControl [2] == "F") && FFF_Meteorite )
		{
			EventManager.MagicCast.Invoke ("FFF");
			spellText.text = "My foes will all fall!";
			SpellTextEnable ();
			float playerScale = player.transform.localScale.x;
			if (playerScale < 0) 
			{
				for (int i = 1; i <= 7 ; i++) 
				{
					GameObject clone =  (GameObject)Instantiate (Resources.Load("FFF_MeteoriteR"), player.transform.position + new Vector3(Random.Range(-15,-7),15,0), Quaternion.identity);
					clone.GetComponent<Rigidbody2D> ().AddForce (new Vector2(2f,-1) * 500);
					yield return new WaitForSeconds (0.25f);
				}
			} 
			else if (playerScale > 0)
			{
				for (int i = 1; i <= 7 ; i++) 
				{
					GameObject clone =  (GameObject)Instantiate (Resources.Load("FFF_MeteoriteL"), player.transform.position  + new Vector3(Random.Range(7,15),15,0), Quaternion.identity);
					clone.GetComponent<Rigidbody2D> ().AddForce (new Vector2(-2f,-1) * 500);
					yield return new WaitForSeconds (0.25f);
				}
			}					
		}
		else if( ((orbControl [0] == "F" && orbControl [1] == "F" && orbControl [2] == "W")||
			(orbControl [0] == "F" && orbControl [1] == "W" && orbControl [2] == "F") ||
			(orbControl [0] == "W" && orbControl [1] == "F" && orbControl [2] == "F") ) && FFW_FireWall	)
		{
			EventManager.MagicCast.Invoke ("FFW");
			spellText.text =  "Now, burn baby burn!";
			SpellTextEnable ();
			if(player.transform.localScale.x < 0)
				Instantiate (Resources.Load("FFW_FireWall") ,player.transform.position + new Vector3 (6, 2, 0) , Quaternion.Euler(-90,0,0) );
			else if(player.transform.localScale.x > 0)
				Instantiate (Resources.Load("FFW_FireWall") ,player.transform.position + new Vector3 (-6, 2, 0), Quaternion.Euler(-90,0,0) );
		}
		else if( ((orbControl [0] == "F" && orbControl [1] == "F" && orbControl [2] == "L")||
			(orbControl [0] == "F" && orbControl [1] == "L" && orbControl [2] == "F") ||
			(orbControl [0] == "L" && orbControl [1] == "F" && orbControl [2] == "F")) && FFL_FireBall	)
		{
			EventManager.MagicCast.Invoke ("FFL");
			spellText.text = "This will be a bit hot!";
			SpellTextEnable ();
			if (player.transform.localScale.x < 0) 
			{
				GameObject clone = (GameObject)Instantiate (Resources.Load("FFL_FireBall") ,player.transform.position + new Vector3 (0, 3, 0) , Quaternion.identity );
				clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (4, 0);
			}
			else if(player.transform.localScale.x > 0)
			{
				GameObject clone = (GameObject)Instantiate (Resources.Load("FFL_FireBall") ,player.transform.position + new Vector3 (0, 3, 0) , Quaternion.identity );
				clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-4, 0);
			}
		}
		else if( (orbControl [0] == "L" && orbControl [1] == "L" && orbControl [2] == "L") && LLL_Thunder )
		{
			EventManager.MagicCast.Invoke ("LLL");
			spellText.text = "Thunderous applause!";
			SpellTextEnable ();

			for (int i = 1; i <= 24; i++) 
			{
				GameObject clone = (GameObject)Instantiate (Resources.Load("ThunderBolt"), player.transform.position +new Vector3(Random.Range(-12,12),7.5f,0) , Quaternion.Euler(0,0,180) );
				Destroy (clone, 1);
				yield return new WaitForSeconds (0.1f);
			}
		}
		else if( ((orbControl [0] == "L" && orbControl [1] == "L" && orbControl [2] == "W")||
			(orbControl [0] == "L" && orbControl [1] == "W" && orbControl [2] == "L") ||
			(orbControl [0] == "W" && orbControl [1] == "L" && orbControl [2] == "L")) && LLW_Blink 	)
		{
			EventManager.MagicCast.Invoke ("LLW");
			spellText.text = "Catch me if you can";
			SpellTextEnable ();
			Invoke ("BlinkSkill", 0.5f);
		}
		else if( ((orbControl [0] == "L" && orbControl [1] == "L" && orbControl [2] == "F")||
			(orbControl [0] == "L" && orbControl [1] == "F" && orbControl [2] == "L") ||
			(orbControl [0] == "F" && orbControl [1] == "L" && orbControl [2] == "L")) && LLF_Bolt )
		{
			EventManager.MagicCast.Invoke ("LLF");
			spellText.text = "A gift from Thunder God!";
			SpellTextEnable ();
			if (player.transform.localScale.x < 0)
			{
				GameObject clone = (GameObject)Instantiate (Resources.Load ("Bolt"), player.transform.position + new Vector3 (4, 1, 0), Quaternion.identity);
				Destroy (clone,1);
				Instantiate (Resources.Load ("LLF_LightningBolt"), player.transform.position + new Vector3 (8, 1, 0), Quaternion.Euler (180, -90, -90));
				yield return new WaitForSeconds (0.5f);
				Instantiate (Resources.Load ("LLF_LightningBolt"), player.transform.position + new Vector3 (8, 1, 0), Quaternion.Euler (180, -90, -90));
			} 
			else if (player.transform.localScale.x > 0) 
			{
				GameObject clone = (GameObject)Instantiate (Resources.Load ("Bolt"), player.transform.position + new Vector3 (-4, 1, 0), Quaternion.identity);
				Destroy (clone,1);
				Instantiate (Resources.Load ("LLF_LightningBolt"), player.transform.position + new Vector3 (-8, 1, 0), Quaternion.Euler (0,-90,-90) );
				yield return new WaitForSeconds (0.5f);
				Instantiate (Resources.Load ("LLF_LightningBolt"), player.transform.position + new Vector3 (-8, 1, 0), Quaternion.Euler (0,-90,-90) );
			}
				
		}
		else if( ((orbControl [0] == "W" && orbControl [1] == "F" && orbControl [2] == "L") ||
			(orbControl [0] == "W" && orbControl [1] == "L" && orbControl [2] == "F") ||
			(orbControl [0] == "F" && orbControl [1] == "W" && orbControl [2] == "L") ||
			(orbControl [0] == "F" && orbControl [1] == "L" && orbControl [2] == "W") ||
			(orbControl [0] == "L" && orbControl [1] == "W" && orbControl [2] == "F") ||
			(orbControl [0] == "L" && orbControl [1] == "F" && orbControl [2] == "W")) && WFL_LittleBig	)
		{
			EventManager.MagicCast.Invoke ("WFL");
			if (Mathf.Abs(player.transform.localScale.x) >= 2) 
			{
				spellText.text = "When you are too big!";
				SpellTextEnable ();
				yield return new WaitForSeconds (0.3f);
				player.transform.localScale = new Vector3 (player.transform.localScale.x/3, player.transform.localScale.y/3, player.transform.localScale.z);
				Instantiate (Resources.Load("WFL_BigSmall"), player.transform.position + new Vector3(0,0.5f,0) , Quaternion.Euler(90,0,0) );
				if (playerSpeed != 10)
					playerSpeed = 4;
			}
			else if(Mathf.Abs(player.transform.localScale.x) <= 2) 
			{
				spellText.text = "Aren't you too small?";
				SpellTextEnable ();
				yield return new WaitForSeconds (0.3f);
				player.transform.localScale = new Vector3 (player.transform.localScale.x*3, player.transform.localScale.y*3, player.transform.localScale.z);
				Instantiate (Resources.Load("WFL_BigSmall"), player.transform.position + new Vector3(0,0.5f,0) , Quaternion.Euler(90,0,0) );
				if (playerSpeed != 10)
					playerSpeed = 5;
			}
		}
		else
		{
			EventManager.MagicCast.Invoke ("???");
			spellText.text = "??????";
			SpellTextEnable ();
			invokeButton.interactable = false;
			windButton.interactable = false;
			fireButton.interactable = false;
			electricButton.interactable = false;

			coolDownInvoke = 5;
			stunEffect.SetActive (true);
		}

		Invoke ("OnCooldownReleaseInvoke", coolDownInvoke);
	
		OnOrbClear ();	

		yield return null;
	}

	public void OnWindButtonClicked()
	{
		audioSource.PlayOneShot ((AudioClip)Resources.Load ("Sound/_WindOrb"));

		invokeButton.interactable = false;
		windButton.interactable = false;
		fireButton.interactable = false;
		electricButton.interactable = false;
		Invoke ("OnCooldownReleaseWFL", 0.5f);

		GameObject clone = (GameObject)Instantiate (Resources.Load("_WindCastEffect"), player.transform.position + new Vector3 (0, 0, 0), Quaternion.Euler(70,0,0));
		clone.transform.SetParent (player.transform);

		if (currentIndexOrb > 2) 
		{
			currentIndexOrb = 0;
		}

		if (currentIndexOrb == 0) 
		{
			windOrb_1.SetActive (true);
			fireOrb_1.SetActive (false);
			electricOrb_1.SetActive (false);
		}
		else if (currentIndexOrb == 1)
		{
			windOrb_2.SetActive (true);
			fireOrb_2.SetActive (false);
			electricOrb_2.SetActive (false);
		} 
		else if (currentIndexOrb == 2) 
		{
			windOrb_3.SetActive (true);
			fireOrb_3.SetActive (false);
			electricOrb_3.SetActive (false);
		}
		orbControl [currentIndexOrb] = "W";
		currentIndexOrb++;
	}

	public void OnFireButtonClicked()
	{
		audioSource.PlayOneShot ((AudioClip)Resources.Load ("Sound/_FireOrb"));

		invokeButton.interactable = false;
		windButton.interactable = false;
		fireButton.interactable = false;
		electricButton.interactable = false;

		Invoke ("OnCooldownReleaseWFL", 0.5f);

		GameObject clone = (GameObject)Instantiate (Resources.Load("_FireCastEffect"), player.transform.position + new Vector3 (0, 0, 0), Quaternion.Euler(70,0,0));
		clone.transform.SetParent (player.transform);

		if (currentIndexOrb > 2) 
		{
			currentIndexOrb = 0;
		}

		if (currentIndexOrb == 0) 
		{
			fireOrb_1.SetActive (true);
			windOrb_1.SetActive (false);
			electricOrb_1.SetActive (false);
		}
		else if (currentIndexOrb == 1)
		{
			fireOrb_2.SetActive (true);
			windOrb_2.SetActive (false);
			electricOrb_2.SetActive (false);
		} 
		else if (currentIndexOrb == 2) 
		{
			fireOrb_3.SetActive (true);
			windOrb_3.SetActive (false);
			electricOrb_3.SetActive (false);
		}

		orbControl [currentIndexOrb] = "F";
		currentIndexOrb++;
	}

	public void OnElectricButtonClicked()
	{
		audioSource.PlayOneShot ((AudioClip)Resources.Load ("Sound/_LightningOrb"));

		invokeButton.interactable = false;
		windButton.interactable = false;
		fireButton.interactable = false;
		electricButton.interactable = false;

		Invoke ("OnCooldownReleaseWFL", 0.5f);

		GameObject clone = (GameObject)Instantiate (Resources.Load("_ElectricCastEffect"), player.transform.position + new Vector3 (0, 0, 0), Quaternion.Euler(70,0,0));
		clone.transform.SetParent (player.transform);

		if (currentIndexOrb > 2) 
		{
			currentIndexOrb = 0;
		}

		if (currentIndexOrb == 0) 
		{
			electricOrb_1.SetActive (true);
			windOrb_1.SetActive (false);
			fireOrb_1.SetActive (false);
		}
		else if (currentIndexOrb == 1)
		{
			electricOrb_2.SetActive (true);
			windOrb_2.SetActive (false);
			fireOrb_2.SetActive (false);
		} 
		else if (currentIndexOrb == 2) 
		{
			electricOrb_3.SetActive (true);
			windOrb_3.SetActive (false);
			fireOrb_3.SetActive (false);
		}

		orbControl [currentIndexOrb] = "L";
		currentIndexOrb++;
	}


	void OnCooldownReleaseInvoke()
	{
		invokeButton.interactable = true;
		windButton.interactable = true;
		fireButton.interactable = true;
		electricButton.interactable = true;

		invokeClick = false;

	}

	void OnCooldownReleaseWFL()
	{
		invokeButton.interactable = true;
		windButton.interactable = true;
		fireButton.interactable = true;
		electricButton.interactable = true;
	}

	void OnOrbClear()
	{
		orbControl [0] = "x";
		orbControl [1] = "x";
		orbControl [2] = "x";

		windOrb_1.SetActive (false);
		fireOrb_1.SetActive (false);
		electricOrb_1.SetActive (false);
		windOrb_2.SetActive (false);
		fireOrb_2.SetActive (false);
		electricOrb_2.SetActive (false);
		windOrb_3.SetActive (false);
		fireOrb_3.SetActive (false);
		electricOrb_3.SetActive (false);

		currentIndexOrb = 0;
	}

	void BlinkSkill()
	{
		Instantiate(Resources.Load("LLW_Blink"), player.transform.position, Quaternion.Euler(0,0,0));
		if (player.transform.localScale.x < 0) 
		{
			player.transform.position = new Vector3 ( blinkPoint.transform.position.x,
				blinkPoint.transform.position.y , blinkPoint.transform.position.z);
		}
		else if (player.transform.localScale.x > 0) 
		{
			player.transform.position = new Vector3 (blinkPoint.transform.position.x ,
				blinkPoint.transform.position.y , blinkPoint.transform.position.z);
		}
		Instantiate(Resources.Load("LLW_Blink"), player.transform.position, Quaternion.Euler(90,0,0));
	}

	void SpeedDecrease()
	{
		if (small)
			playerSpeed = 4;
		else
			playerSpeed = 5;
	}

	void SpellTextEnable()
	{
		textBox.SetActive (true);
		textBox.transform.position = new Vector3 (player.transform.position.x + 3, player.transform.position.y + 3, -2);
		textBoxGuiAnim.Anim_Idle_FadeLoop (0);
		Invoke ("SpellTextDis", 2);
	}
		
	void SpellTextDis()
	{
		textBox.SetActive (false);
		textBoxGuiAnim.Anim_Idle_StopFadeLoop ();
	}


	void OnPlayerDie()
	{
		themeAudioSource.Stop ();
		audioSource.PlayOneShot ((AudioClip)Resources.Load ("Sound/_FailSound"));
		playerAnim.SetBool ("Dead", true);
		Invoke ("OnDiedMenu", 0.5f);

	}

	void OnDiedMenu()
	{
		failPanel.SetActive (true);
		failPanel.transform.localPosition = new Vector3 (0, -25, 0);
		Time.timeScale = 0;
	}

	void OnPlayerPass()
	{
		playerRigid.velocity = Vector2.zero;
		themeAudioSource.Stop ();
		audioSource.PlayOneShot ((AudioClip)Resources.Load ("Sound/_WonSound"));
		clearPanel.SetActive (true);
		clearPanel.transform.localPosition = new Vector3 (0, -25, 0);
		Time.timeScale = 0;
	}

	public void OnChangeMenuClick(string name)
	{
		SceneManager.LoadScene (name);
	}

	public void OnPauseClick()
	{
		pausePanel.SetActive (true);
		pausePanel.transform.localPosition = new Vector3 (0, 0, 0);
		pauseMenu.SetActive (true);
		pauseMenu.transform.localPosition = new Vector3 (0, 0, 0);
		Time.timeScale = 0;
	}

	public void OnCharmClick(string bookName)
	{
		pausePanel.SetActive (true);
		pausePanel.transform.localPosition = new Vector3 (0, 0, 0);
		pauseMenu.SetActive (false);
		bookWWW_1.SetActive (false);
		bookWWF_2.SetActive (false);
		bookWWL_3.SetActive (false);
		bookFFF_4.SetActive (false);
		bookFFW_5.SetActive (false);
		bookFFL_6.SetActive (false);
		bookLLL_7.SetActive (false);
		bookLLW_8.SetActive (false);
		bookLLF_9.SetActive (false);
		bookWFL_10.SetActive (false);
		bookWWW_1.transform.localPosition = new Vector3 (0, 0, 0);
		bookWWF_2.transform.localPosition = new Vector3 (0, 0, 0);
		bookWWL_3.transform.localPosition = new Vector3 (0, 0, 0);
		bookFFF_4.transform.localPosition = new Vector3 (0, 0, 0);
		bookFFW_5.transform.localPosition = new Vector3 (0, 0, 0);
		bookFFL_6.transform.localPosition = new Vector3 (0, 0, 0);
		bookLLL_7.transform.localPosition = new Vector3 (0, 0, 0);
		bookLLW_8.transform.localPosition = new Vector3 (0, 0, 0);
		bookLLF_9.transform.localPosition = new Vector3 (0, 0, 0);
		bookWFL_10.transform.localPosition = new Vector3 (0, 0, 0);

		if (bookName == "WWW")
			bookWWW_1.SetActive (true);
		else if (bookName == "WWF")
			bookWWF_2.SetActive (true);
		else if (bookName == "WWL")
			bookWWL_3.SetActive (true);
		else if (bookName == "FFF")
			bookFFF_4.SetActive (true);
		else if (bookName == "FFW")
			bookFFW_5.SetActive (true);
		else if (bookName == "FFL")
			bookFFL_6.SetActive (true);
		else if (bookName == "LLL")
			bookLLL_7.SetActive (true);
		else if (bookName == "LLW")
			bookLLW_8.SetActive (true);
		else if (bookName == "LLF")
			bookLLF_9.SetActive (true);
		else if (bookName == "WFL")
			bookWFL_10.SetActive (true);
		else if (failPanel.activeInHierarchy)
			pausePanel.SetActive (false);
		else
			pauseMenu.SetActive (true);
	}

	public void OnUnPauseClick()
	{
		pausePanel.SetActive (false);
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}

}
