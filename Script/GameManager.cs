using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour 
{
	float playerSpeed = 5 ;
	bool moveLeftClick;
	bool moveRightClick;
	bool invokeClick;
	float coolDownInvoke;
	bool playerSmall;

	GameObject player;
	Rigidbody2D playerRigid;
	Animator playerAnim;
	AudioSource runAudioSource;

	GameObject walkEffect;
	GameObject stunEffect;
	Transform blinkPoint;

	bool WWW, WWF, WWL, FFF, FFW, FFL, LLL, LLW, LLF, WFL;

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

	GameObject spellBox;
	Text spellText;
	GUIAnim spellBoxGuiAnim;

	int lifeNumber;
	int keyNumber;
	Text lifeNumberText;
	Text keyNumberText;
	Text wonKeyText;
	GameObject winVideoButton;
	Button spendKeyButton;
	Button lvSkipButton;
	GameObject uiCanvas;
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
	AudioSource themeAudioSource;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);

		EventManager.PlayerDie.AddListener (OnPlayerDie);
		EventManager.PlayerPass.AddListener (OnPlayerPass);
		EventManager.AfterRewardAd.AddListener (OnRewardWatch);
		EventManager.AfterRelifeAd.AddListener (OnRelife);

		audioSource = GetComponent<AudioSource> (); 

		invokeButton = GameObject.Find ("ButtonInvoke").GetComponent<Button> ();
		windButton = GameObject.Find ("ButtonWind").GetComponent<Button> ();
		fireButton = GameObject.Find ("ButtonFire").GetComponent<Button> ();
		electricButton =  GameObject.Find ("ButtonElectric").GetComponent<Button> ();

		spellBox = GameObject.Find ("SpellBox");
		spellText = GameObject.Find ("SpellText").GetComponent<Text> ();
		spellBoxGuiAnim = spellBox.GetComponent<GUIAnim> ();
		spellBox.SetActive (false);

		lifeNumberText = GameObject.Find ("LifeNumber").GetComponent<Text> ();
		keyNumberText = GameObject.Find("KeyNumber").GetComponent<Text>();
		wonKeyText = GameObject.Find ("WonKeyText").GetComponent<Text> ();
		winVideoButton = GameObject.Find ("WinVideoButton");
		spendKeyButton = GameObject.Find ("SpendKeyButton").GetComponent<Button>();
		lvSkipButton = GameObject.Find ("LvSkipButton").GetComponent<Button>();
		uiCanvas = GameObject.Find ("UI_Canvas");
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
		uiCanvas.SetActive (false);
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
	}

	void OnLevelWasLoaded()
	{	
		Time.timeScale = 1;

		lifeNumber = PlayerPrefs.GetInt ("Life", 3);
		keyNumber = PlayerPrefs.GetInt ("Key", 0);
		lifeNumberText.text = "" + lifeNumber;
		keyNumberText.text = "" + keyNumber;
		wonKeyText.text = "2";
		string sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == "MainMenu" || sceneName == "Map" || sceneName == "Intro" || sceneName == "Shop" || sceneName =="Credit") 
		{
			uiCanvas.SetActive (false);
		}
		else 
		{
			uiCanvas.SetActive (true);

			OnUIAppeared ();
			WhichMagicAllow ();
			playerSpeed = 5;
		}
	}

	void OnUIAppeared()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerRigid = player.GetComponent<Rigidbody2D> ();
		playerAnim = player.GetComponent<Animator> ();
		runAudioSource = GameObject.Find ("RunSound").GetComponent<AudioSource>();
		themeAudioSource = GameObject.Find ("ThemeSong").GetComponent<AudioSource> ();

		walkEffect = player.transform.FindChild ("SmokeWalk").gameObject;
		stunEffect = player.transform.FindChild ("StunEffect").gameObject;
		blinkPoint = player.transform.FindChild ("BlinkPoint").transform;

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
	}

	void WhichMagicAllow()
	{
		string currentSceneName = SceneManager.GetActiveScene ().name;
		switch (currentSceneName) 
		{
		case "2_WindstormClass" :  case "3_WindstormTest" :  
			WWW = true;
			WWF= false;
			WWL= false;
			FFF= false;
			FFW= false;
			FFL= false;
			LLL= false;
			LLW= false;
			LLF= false;
			WFL= false;
			break;
		case "4_FloatClass": case "5_FloatTest" : 
			WWW = false;
			WWF= true;
			WWL= false;
			FFF= false;
			FFW= false;
			FFL= false;
			LLL= false;
			LLW= false;
			LLF= false;
			WFL= false;
			break;
		case "6_WindrunClass": case "7_WindrunTest" : 
			WWW = false;
			WWF= false;
			WWL= true;
			FFF= false;
			FFW= false;
			FFL= false;
			LLL= false;
			LLW= false;
			LLF= false;
			WFL= false;
			break;
		case "8_MeteoriteClass": case "9_MeteoriteTest" : 
			WWW = false;
			WWF= false;
			WWL= false;
			FFF= true;
			FFW= false;
			FFL= false;
			LLL= false;
			LLW= false;
			LLF= false;
			WFL= false;
			break;
		case "10_FirewallClass": case "11_FirewallTest" : 
			WWW = false;
			WWF= false;
			WWL= false;
			FFF= false;
			FFW= true;
			FFL= false;
			LLL= false;
			LLW= false;
			LLF= false;
			WFL= false;;
			break;
		case "12_FireballClass": case "13_FireballTest" :
			WWW = false;
			WWF= false;
			WWL= false;
			FFF= false;
			FFW= false;
			FFL= true;
			LLL= false;
			LLW= false;
			LLF= false;
			WFL= false;
			break;
		case "14_LightningstormClass": case "15_LightningstormTest" : 
			WWW = false;
			WWF= false;
			WWL= false;
			FFF= false;
			FFW= false;
			FFL= false;
			LLL= true;
			LLW= false;
			LLF= false;
			WFL= false;
			break;
		case "16_BlinkClass"   :case "17_BlinkTest" : 
			WWW = false;
			WWF= false;
			WWL= false;
			FFF= false;
			FFW= false;
			FFL= false;
			LLL= false;
			LLW= true;
			LLF= false;
			WFL= false;
			break;
		case "18_LightninglanceClass" : case "19_LightninglanceTest" : 
			WWW = false;
			WWF= false;
			WWL= false;
			FFF= false;
			FFW= false;
			FFL= false;
			LLL= false;
			LLW= false;
			LLF= true;
			WFL= false;
			break;
		case "20_ShrinkClass" : case "21_ShrinkTest" : 
			WWW = false;
			WWF= false;
			WWL= false;
			FFF= false;
			FFW= false;
			FFL= false;
			LLL= false;
			LLW= false;
			LLF= false;
			WFL= true;
			break;
		default: WWW = true;
			WWF= true;
			WWL= true;
			FFF= true;
			FFW= true;
			FFL= true;
			LLL= true;
			LLW= true;
			LLF= true;
			WFL= true;
			break;
		}

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

		coolDownInvoke = 2.5f;

		GameObject clone1 = (GameObject)Instantiate (Resources.Load("_InvokeCastEffect"), player.transform.position + new Vector3 (0, 0, 0), Quaternion.Euler(70,0,0));
		clone1.transform.SetParent (player.transform);

		int magicTextRandomer = Random.Range (0, 3);
		if ( (orbControl [0] == "W" && orbControl [1] == "W" && orbControl [2] == "W") && WWW) 
		{
			EventManager.MagicCast.Invoke ("WWW");
			if(magicTextRandomer==0)
				spellText.text = "Let the storm rage on!";
			else if(magicTextRandomer==1)
				spellText.text = "Let it go! Let it go!";
			else
				spellText.text = "Windstorm!";
			SpellTextEnable ();
			if (player.transform.localScale.x < 0)
				Instantiate (Resources.Load ("WWW_Windy"), player.transform.position + new Vector3 (-18, 5, 0), Quaternion.Euler (180, 89, 90));
			else if (player.transform.localScale.x > 0) 
			{
				GameObject clone =  (GameObject)Instantiate (Resources.Load ("WWW_Windy"), player.transform.position + new Vector3 (18, 5, 0), Quaternion.Euler (180, 269, 90));
				clone.GetComponent<AreaEffector2D> ().forceAngle = 270;
			}
		}
		else if( ((orbControl [0] == "W" && orbControl [1] == "W" && orbControl [2] == "F")||
			(orbControl [0] == "W" && orbControl [1] == "F" && orbControl [2] == "W") ||
			(orbControl [0] == "F" && orbControl [1] == "W" && orbControl [2] == "W")) && WWF )
		{
			EventManager.MagicCast.Invoke ("WWF");
			if(magicTextRandomer==0)
				spellText.text = "You'll float too!";
			else if(magicTextRandomer==1)
				spellText.text = "I believe i can fly!";
			else
				spellText.text = "Float!";
			SpellTextEnable ();
			if(player.transform.localScale.x < 0)
				Instantiate (Resources.Load("WWF_Float"), player.transform.position + new Vector3 (5.5f, 0, -1), Quaternion.Euler(70,0,0));
			else if(player.transform.localScale.x > 0)
				Instantiate (Resources.Load("WWF_Float"), player.transform.position + new Vector3 (-5.5f, 0, -1), Quaternion.Euler(70,0,0));
		}
		else if( ((orbControl [0] == "W" && orbControl [1] == "W" && orbControl [2] == "L")||
			(orbControl [0] == "W" && orbControl [1] == "L" && orbControl [2] == "W") ||
			(orbControl [0] == "L" && orbControl [1] == "W" && orbControl [2] == "W")) && WWL )
		{
			EventManager.MagicCast.Invoke ("WWL");
			if(magicTextRandomer==0)
				spellText.text = "May the force be with me!";
			else if(magicTextRandomer==1)
				spellText.text = "Run like the wind!";
			else
				spellText.text = "Windrun!";
			if (playerSpeed <= 5) 
			{
				GameObject clone = (GameObject)Instantiate (Resources.Load("WWL_WindRun"), player.transform.position , Quaternion.Euler(180,0,0));
				playerSpeed = 10;
				clone.transform.SetParent (player.transform);
				Invoke ("SpeedDecrease", PlayerPrefs.GetInt("WindrunTime", 8) );
			}
			else
				spellText.text = "Effect remain!";
			SpellTextEnable ();
		}
		else if( (orbControl [0] == "F" && orbControl [1] == "F" && orbControl [2] == "F") && FFF)
		{
			EventManager.MagicCast.Invoke ("FFF");
			if(magicTextRandomer==0)
				spellText.text = "Here, lost stars!";
			else if(magicTextRandomer==1)
				spellText.text = "We'll be counting stars!";
			else
				spellText.text = "Meteorites!";
			SpellTextEnable ();
			float playerScale = player.transform.localScale.x;
			if (playerScale < 0) 
			{
				for (int i = 1; i <= PlayerPrefs.GetInt("Meteorites",7) ; i++) 
				{
					GameObject clone =  (GameObject)Instantiate (Resources.Load("FFF_MeteoriteR"), player.transform.position + new Vector3(Random.Range(-15,-7),15,0), Quaternion.identity);
					clone.GetComponent<Rigidbody2D> ().AddForce (new Vector2(2f,-1) * 500);
					yield return new WaitForSeconds (0.25f);
				}
			} 
			else if (playerScale > 0)
			{
				for (int i = 1; i <= PlayerPrefs.GetInt("Meteorites",7) ; i++) 
				{
					GameObject clone =  (GameObject)Instantiate (Resources.Load("FFF_MeteoriteL"), player.transform.position  + new Vector3(Random.Range(7,15),15,0), Quaternion.identity);
					clone.GetComponent<Rigidbody2D> ().AddForce (new Vector2(-2f,-1) * 500);
					yield return new WaitForSeconds (0.25f);
				}
			}					
		}
		else if( ((orbControl [0] == "F" && orbControl [1] == "F" && orbControl [2] == "W")||
			(orbControl [0] == "F" && orbControl [1] == "W" && orbControl [2] == "F") ||
			(orbControl [0] == "W" && orbControl [1] == "F" && orbControl [2] == "F")) && FFW )
		{
			EventManager.MagicCast.Invoke ("FFW");
			if(magicTextRandomer==0)
				spellText.text = "Now, burn baby burn!";
			else if(magicTextRandomer==1)
				spellText.text = "You are on fire!";
			else
				spellText.text = "Fire Wall!";
			SpellTextEnable ();
			if(player.transform.localScale.x < 0)
				Instantiate (Resources.Load("FFW_FireWall") ,player.transform.position + new Vector3 (6, 2, 0) , Quaternion.Euler(-90,0,0) );
			else if(player.transform.localScale.x > 0)
				Instantiate (Resources.Load("FFW_FireWall") ,player.transform.position + new Vector3 (-6, 2, 0), Quaternion.Euler(-90,0,0) );
		}
		else if( ((orbControl [0] == "F" && orbControl [1] == "F" && orbControl [2] == "L")||
			(orbControl [0] == "F" && orbControl [1] == "L" && orbControl [2] == "F") ||
			(orbControl [0] == "L" && orbControl [1] == "F" && orbControl [2] == "F")) && FFL )
		{
			EventManager.MagicCast.Invoke ("FFL");
			if(magicTextRandomer==0)
				spellText.text = "Like a hot wrecking ball!";
			else if(magicTextRandomer==1)
				spellText.text = "Rolling into the fire!";
			else
				spellText.text = "Fire Ball!";
			SpellTextEnable ();
			if (player.transform.localScale.x < 0) 
			{
				for (int i = 1; i <= PlayerPrefs.GetInt ("Fireball", 1); i++) 
				{
					GameObject clone = (GameObject)Instantiate (Resources.Load("FFL_FireBall") ,player.transform.position + new Vector3 (0, 3, 0) , Quaternion.identity );
					clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (4, 0);
					yield return new WaitForSeconds (1f);
				}
			}
			else if(player.transform.localScale.x > 0)
			{
				for (int i = 1; i <= PlayerPrefs.GetInt ("Fireball", 1); i++) 
				{
					GameObject clone = (GameObject)Instantiate (Resources.Load("FFL_FireBall") ,player.transform.position + new Vector3 (0, 3, 0) , Quaternion.identity );
					clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-4, 0);
					yield return new WaitForSeconds (1f);
				}
			}
		}
		else if( (orbControl [0] == "L" && orbControl [1] == "L" && orbControl [2] == "L") && LLL )
		{
			EventManager.MagicCast.Invoke ("LLL");
			if(magicTextRandomer==0)
				spellText.text = "Thunderous applause!";
			else if(magicTextRandomer==1)
				spellText.text = "Strike down my enemy!";
			else
				spellText.text = "Lightning Storm!";
			SpellTextEnable ();
			for (int i = 1; i <= PlayerPrefs.GetInt("Lightningstorm",24) ; i++) 
			{
				GameObject clone = (GameObject)Instantiate (Resources.Load("ThunderBolt"), player.transform.position +new Vector3(Random.Range(-12,12),7.5f,0) , Quaternion.Euler(0,0,180) );
				Destroy (clone, 1);
				yield return new WaitForSeconds (0.1f);
			}
		}
		else if( ((orbControl [0] == "L" && orbControl [1] == "L" && orbControl [2] == "W")||
			(orbControl [0] == "L" && orbControl [1] == "W" && orbControl [2] == "L") ||
			(orbControl [0] == "W" && orbControl [1] == "L" && orbControl [2] == "L")) && LLW )
		{
			EventManager.MagicCast.Invoke ("LLW");
			if(magicTextRandomer==0)
				spellText.text = "Over here, dude!";
			else if(magicTextRandomer==1)
				spellText.text = "Here, I am!";
			else
				spellText.text = "Blink!";
			SpellTextEnable ();
			Invoke ("BlinkSkill", 0.5f);
		}
		else if( ((orbControl [0] == "L" && orbControl [1] == "L" && orbControl [2] == "F")||
			(orbControl [0] == "L" && orbControl [1] == "F" && orbControl [2] == "L") ||
			(orbControl [0] == "F" && orbControl [1] == "L" && orbControl [2] == "L")) && LLF )
		{
			EventManager.MagicCast.Invoke ("LLF");
			if(magicTextRandomer==0)
				spellText.text = "A gift from Zeus!";
			else if(magicTextRandomer==1)
				spellText.text = "Missing Lightning is here!";
			else
				spellText.text = "Lightning Lance!";
			SpellTextEnable ();
			if (player.transform.localScale.x < 0)
			{
				for (int i = 1; i <= PlayerPrefs.GetInt ("Lightninglance", 1); i++) 
				{
					GameObject clone = (GameObject)Instantiate (Resources.Load ("Bolt"), player.transform.position + new Vector3 (4, 1, 0), Quaternion.identity);
					Destroy (clone,0.5f);
					Instantiate (Resources.Load ("LLF_LightningBolt"), player.transform.position + new Vector3 (8, 1, 0), Quaternion.Euler (180, -90, -90));
					yield return new WaitForSeconds (0.5f);
				}
			} 
			else if (player.transform.localScale.x > 0) 
			{
				for (int i = 1; i <= PlayerPrefs.GetInt ("Lightninglance", 1); i++) 
				{
					GameObject clone = (GameObject)Instantiate (Resources.Load ("Bolt"), player.transform.position + new Vector3 (-4, 1, 0), Quaternion.identity);
					Destroy (clone,0.5f);
					Instantiate (Resources.Load ("LLF_LightningBolt"), player.transform.position + new Vector3 (-8, 1, 0), Quaternion.Euler (0,-90,-90) );
					yield return new WaitForSeconds (0.5f);
				}
			}
		}
		else if( ((orbControl [0] == "W" && orbControl [1] == "F" && orbControl [2] == "L") ||
			(orbControl [0] == "W" && orbControl [1] == "L" && orbControl [2] == "F") ||
			(orbControl [0] == "F" && orbControl [1] == "W" && orbControl [2] == "L") ||
			(orbControl [0] == "F" && orbControl [1] == "L" && orbControl [2] == "W") ||
			(orbControl [0] == "L" && orbControl [1] == "W" && orbControl [2] == "F") ||
			(orbControl [0] == "L" && orbControl [1] == "F" && orbControl [2] == "W")) && WFL )
		{
			EventManager.MagicCast.Invoke ("WFL");
			if (Mathf.Abs(player.transform.localScale.x) >= 2) 
			{
				if(magicTextRandomer==0)
					spellText.text = "In a big big world!";
				else if(magicTextRandomer==1)
					spellText.text = "Words can bring me down!";
				else
					spellText.text = "Small!";
				SpellTextEnable ();
				yield return new WaitForSeconds (0.3f);
				player.transform.localScale = new Vector3 (player.transform.localScale.x/3, player.transform.localScale.y/3, player.transform.localScale.z);
				Instantiate (Resources.Load("WFL_BigSmall"), player.transform.position + new Vector3(0,0.5f,0) , Quaternion.Euler(90,0,0) );
				if (playerSpeed != 10)
					playerSpeed = 4;
			}
			else if(Mathf.Abs(player.transform.localScale.x) <= 2) 
			{
				if(magicTextRandomer==0)
					spellText.text = "I'm a big big girl!";
				else if(magicTextRandomer==1)
					spellText.text = "Words can't bring me down!";
				else
					spellText.text = "Big!";
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

	void OnCooldownReleaseInvoke()
	{
		invokeButton.interactable = true;
		windButton.interactable = true;
		fireButton.interactable = true;
		electricButton.interactable = true;

		invokeClick = false;
	}

	public void OnWindButtonClicked()
	{
		audioSource.PlayOneShot ((AudioClip)Resources.Load ("Sound/_WindOrb"));

		invokeButton.interactable = false;
		windButton.interactable = false;
		fireButton.interactable = false;
		electricButton.interactable = false;
		Invoke ("OnCooldownReleaseWFL", 0f);

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

		Invoke ("OnCooldownReleaseWFL", 0f);

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

		Invoke ("OnCooldownReleaseWFL", 0f);

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
		if (playerSmall)
			playerSpeed = 4;
		else
			playerSpeed = 5;
	}

	void SpellTextEnable()
	{
		spellBox.SetActive (true);
		spellBox.transform.position = new Vector3 (player.transform.position.x + 3, player.transform.position.y + 3, -2);
		spellBoxGuiAnim.Anim_Idle_FadeLoop (0);
		Invoke ("SpellTextDis", 2);
	}
		
	void SpellTextDis()
	{
		spellBox.SetActive (false);
		spellBoxGuiAnim.Anim_Idle_StopFadeLoop ();
	}
		

	public void OnCharmClick(string bookName)
	{
		//book info pause menu
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


	public void OnPlayerDie() //when press restart button from pauseMenu && after died life check
	{
		OnUnPauseClick ();
		playerRigid.velocity = Vector2.zero;
		playerAnim.SetBool ("Dead", true);
		audioSource.PlayOneShot ((AudioClip)Resources.Load ("Sound/_FailSound"));
		lifeNumber--;
		if (lifeNumber <= 0)
			lifeNumber = 0;
		lifeNumberText.text = "" + lifeNumber;
		PlayerPrefs.SetInt ("Life", lifeNumber);

		Invoke ("OnLifeCheck", 1.5f);
	}

	void OnLifeCheck() 
	{
		Time.timeScale = 0;
		failPanel.SetActive (false);
		if (keyNumber < 60)
			lvSkipButton.enabled = false;
		else
			lvSkipButton.enabled = true;
	
		if (keyNumber < 6)
			spendKeyButton.enabled = false;
		else
			spendKeyButton.enabled = true;

		if (lifeNumber <= 0) 
		{
			themeAudioSource.Stop ();
			failPanel.SetActive (true);
			failPanel.transform.localPosition = new Vector3 (0, -23, 0);
		}
		else 
		{
			failPanel.SetActive (false);
			lifeNumberText.text = "" + lifeNumber;
			PlayerPrefs.SetInt ("Life", lifeNumber);
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}
		
	public void OnRelife() 
	{
		lifeNumber = 3;
		lifeNumberText.text = "" + lifeNumber;
		PlayerPrefs.SetInt ("Life", lifeNumber);
		OnLifeCheck ();
	}

	public void OnKeyRelife() 
	{
		if (keyNumber >= 6) {
			keyNumber -= 6;
			keyNumberText.text = "" + keyNumber;
			PlayerPrefs.SetInt ("Key", keyNumber);
			OnRelife ();
		} 
	}

	public void OnSkipStage()					//Skip Stage can't use if at last scene. Make sure this value have last scene confition
	{	
		int levelPassed = PlayerPrefs.GetInt ("LevelPassed");
		if (SceneManager.GetActiveScene ().name != "27_Flappyskull" && keyNumber >= 60 && levelPassed < 27)   //last scene check
		{
			levelPassed++;
			PlayerPrefs.SetInt ("LevelPassed", levelPassed);
			keyNumber -= 60;
			keyNumberText.text = "" + keyNumber;
			PlayerPrefs.SetInt ("Key", keyNumber);
			failPanel.SetActive (false);
			SceneManager.LoadScene ("Map");
		}
	}

	void OnPlayerPass()
	{
		playerRigid.velocity = Vector2.zero;
		themeAudioSource.Stop ();
		audioSource.PlayOneShot ((AudioClip)Resources.Load ("Sound/_WonSound"));
		winVideoButton.SetActive (true);
		clearPanel.SetActive (true);
		clearPanel.transform.localPosition = new Vector3 (0, -25, 0);
		keyNumber += 2;
		keyNumberText.text = "" + keyNumber;
		PlayerPrefs.SetInt ("Key", keyNumber);

		Time.timeScale = 0;
	}

	public void OnRewardWatch()
	{
		keyNumber += 4;
		keyNumberText.text = "" + keyNumber;
		PlayerPrefs.SetInt ("Key", keyNumber);
		wonKeyText.text = "6";
		winVideoButton.SetActive (false);
	}
		
	public void OnPauseClick()
	{
		pausePanel.SetActive (true);
		pausePanel.transform.localPosition = new Vector3 (0, 0, 0);
		pauseMenu.SetActive (true);
		pauseMenu.transform.localPosition = new Vector3 (0, 0, 0);
		Time.timeScale = 0;
	}

	public void OnUnPauseClick()
	{
		pausePanel.SetActive (false);
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void OnChangeMenuClick(string name)
	{
		pausePanel.SetActive(false);
		failPanel.SetActive(false);
		clearPanel.SetActive(false);
		pauseMenu.SetActive(false);
		SceneManager.LoadScene (name);
	}

	public void OnNextButtonClick()
	{
		string currentSceneName = SceneManager.GetActiveScene ().name;
		string nextSceneName;
		switch (currentSceneName) 
		{
		case "1_MagicProof":
			nextSceneName = "2_WindstormClass";
			break;
		case "2_WindstormClass" :  nextSceneName = "3_WindstormTest";
			break;
		case "3_WindstormTest" : nextSceneName = "4_FloatClass";
			break;
		case "4_FloatClass" : nextSceneName = "5_FloatTest";
			break;
		case "5_FloatTest" : nextSceneName = "6_WindrunClass";
			break;
		case "6_WindrunClass" : nextSceneName = "7_WindrunTest";
			break;
		case "7_WindrunTest" : nextSceneName = "8_MeteoriteClass";
			break;
		case "8_MeteoriteClass" : nextSceneName = "9_MeteoriteTest";
			break;
		case "9_MeteoriteTest" : nextSceneName = "10_FirewallClass";
			break;
		case "10_FirewallClass" : nextSceneName = "11_FirewallTest";
			break;
		case "11_FirewallTest" : nextSceneName = "12_FireballClass";
			break;
		case "12_FireballClass" : nextSceneName = "13_FireballTest";
			break;
		case "13_FireballTest" : nextSceneName = "14_LightningstormClass";
			break;
		case "14_LightningstormClass" : nextSceneName = "15_LightningstormTest";
			break;
		case "15_LightningstormTest" : nextSceneName = "16_BlinkClass";
			break;
		case "16_BlinkClass" : nextSceneName = "17_BlinkTest";
			break;
		case "17_BlinkTest" : nextSceneName = "18_LightninglanceClass";
			break;
		case "18_LightninglanceClass" : nextSceneName = "19_LightninglanceTest";
			break;
		case "19_LightninglanceTest" : nextSceneName = "20_ShrinkClass";
			break;
		case "20_ShrinkClass" : nextSceneName = "21_ShrinkTest";
			break;
		case "21_ShrinkTest" : nextSceneName = "22_FinalCharmsTest";
			break;
		case "22_FinalCharmsTest" : nextSceneName = "23_WizardTournamentHasBegun";
			break;
		case "23_WizardTournamentHasBegun" : nextSceneName = "24_CandyCaramel";
			break;
		case "24_CandyCaramel" : nextSceneName = "25_CherryJump";
			break;
		case "25_CherryJump" : nextSceneName = "26_GoblinLovesCandy";
			break;
		case "26_GoblinLovesCandy" : nextSceneName = "27_Flappyskull";
			break;
		case "27_Flappyskull" : nextSceneName = "MainMenu";
			break;
		default: nextSceneName = "Map";
			break;
		}

		pausePanel.SetActive(false);
		failPanel.SetActive(false);
		clearPanel.SetActive(false);
		pauseMenu.SetActive(false);
		SceneManager.LoadScene (nextSceneName);
	}

	public void OnGameExit()
	{
		Application.Quit ();
	}
		
}
