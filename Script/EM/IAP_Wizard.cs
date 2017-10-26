using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using SgLib.UI;
using EasyMobile;

#if EM_UIAP
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
#endif

public class IAP_Wizard : MonoBehaviour
{
	public Text keyNumberText;
	public GameObject buySuccessPanel;
	public GameObject fireWorkEffect;
	public GameObject buyFailPanel;
	[Space]
	public Button windrunButton;
	public Button meteoritesButton;
	public Button fireballButton;
	public Button lightningstormButton;
	public Button lightninglanceButton;
	public Button unlockStageButton;
	public Button adRemoveButton;

	void OnEnable()
	{            
		IAPManager.PurchaseCompleted += IAPManager_PurchaseCompleted;
		IAPManager.PurchaseFailed += IAPManager_PurchaseFailed;
	}

	void OnDisable()
	{
		IAPManager.PurchaseCompleted -= IAPManager_PurchaseCompleted;
		IAPManager.PurchaseFailed -= IAPManager_PurchaseFailed;   
	}

	void IAPManager_PurchaseCompleted(IAPProduct product)
	{
		//if produc.name  - google first-
		if (product.Name == "Key x 100") 
		{
			int keyNumber = PlayerPrefs.GetInt ("Key", 0);
			keyNumber += 100;
			keyNumberText.text = "" + keyNumber;
			PlayerPrefs.SetInt ("Key", keyNumber);
		}
		else if (product.Name == "Key x 200") 
		{
			int keyNumber = PlayerPrefs.GetInt ("Key", 0);
			keyNumber += 200;
			keyNumberText.text = "" + keyNumber;
			PlayerPrefs.SetInt ("Key", keyNumber);
		}
		else if (product.Name == "Key x 400")
		{
			int keyNumber = PlayerPrefs.GetInt ("Key", 0);
			keyNumber += 400;
			keyNumberText.text = "" + keyNumber;
			PlayerPrefs.SetInt ("Key", keyNumber);
		}
		else if (product.Name == "Remove Ad") 
		{
			AdManager.RemoveAds();
		}
		else if (product.Name == "Unlock all stages") 
		{
			PlayerPrefs.SetInt ("LevelPassed", 26); //pass 26 mean 27 unlock
		}

		buySuccessPanel.SetActive (true);
		fireWorkEffect.SetActive (true);
	}

	void IAPManager_PurchaseFailed(IAPProduct product)
	{
		buyFailPanel.SetActive (true);
	}
		
	void Awake()
	{
		int keyNumber = PlayerPrefs.GetInt ("Key", 0);
		keyNumberText.text = "" + keyNumber;

		buySuccessPanel.SetActive (false);
		fireWorkEffect.SetActive (false);
		buyFailPanel.SetActive (false);

		if (PlayerPrefs.GetString ("WWWBought", "false") == "true" || keyNumber < 100)
			windrunButton.interactable = false;
		if (PlayerPrefs.GetString ("FFFBought", "false") == "true" || keyNumber < 140)
			meteoritesButton.interactable = false;
		if (PlayerPrefs.GetString ("FFLBought", "false") == "true" || keyNumber < 120)
			fireballButton.interactable = false;
		if (PlayerPrefs.GetString ("LLLBought", "false") == "true" || keyNumber < 120)
			lightningstormButton.interactable = false;
		if (PlayerPrefs.GetString ("LLFBought", "false") == "true" || keyNumber < 140)
			lightninglanceButton.interactable = false;
		if (AdManager.IsAdRemoved ())
			adRemoveButton.interactable = false;
		if (PlayerPrefs.GetInt ("LevelPassed", 0) >= 27)
			unlockStageButton.interactable = false;
	}
		
	public void BuySuccessFailOkClick() // seperate method all Item
	{
		buySuccessPanel.SetActive (false);
		fireWorkEffect.SetActive (false);
		buyFailPanel.SetActive (false);
		SceneManager.LoadScene ("Shop");
	}
		
	public void WindrunUpgrade()
	{
		int keyNumber = PlayerPrefs.GetInt ("Key", 0);
		if (keyNumber >= 100) 
		{
			keyNumber -= 100;
			keyNumberText.text = "" + keyNumber;
			PlayerPrefs.SetInt ("Key", keyNumber);
			PlayerPrefs.SetString ("WWWBought", "true");
			PlayerPrefs.SetInt("WindrunTime",18);

			buySuccessPanel.SetActive (true);
			fireWorkEffect.SetActive (true);
		}

	}

	public void MeteoritesUpgrade()
	{
		int keyNumber = PlayerPrefs.GetInt ("Key", 0);
		if (keyNumber >= 140)
		{
			keyNumber -= 140;
			keyNumberText.text = "" + keyNumber;
			PlayerPrefs.SetInt ("Key", keyNumber);
			PlayerPrefs.SetString ("FFFBought", "true");
			PlayerPrefs.SetInt ("Meteorites", 14);

			buySuccessPanel.SetActive (true);
			fireWorkEffect.SetActive (true);
		}
	}

	public void FireballUpgrade()
	{
		int keyNumber = PlayerPrefs.GetInt ("Key", 0);
		if (keyNumber >= 120) 
		{
			keyNumber -= 120;
			keyNumberText.text = "" + keyNumber;
			PlayerPrefs.SetInt ("Key", keyNumber);
			PlayerPrefs.SetString ("FFLBought", "true");
			PlayerPrefs.SetInt ("Fireball", 3);

			buySuccessPanel.SetActive (true);
			fireWorkEffect.SetActive (true);
		}
	}

	public void LightningstormUpgrade()
	{
		int keyNumber = PlayerPrefs.GetInt ("Key", 0);
		if (keyNumber >= 120)
		{
			keyNumber -= 120;
			keyNumberText.text = "" + keyNumber;
			PlayerPrefs.SetInt ("Key", keyNumber);
			PlayerPrefs.SetString ("LLLBought", "true");
			PlayerPrefs.SetInt ("Lightningstorm", 48);

			buySuccessPanel.SetActive (true);
			fireWorkEffect.SetActive (true);
		}
	}

	public void LightninglanceUpgrade()
	{
		int keyNumber = PlayerPrefs.GetInt ("Key", 0);
		if (keyNumber >= 140)
		{
			keyNumber -= 140;
			keyNumberText.text = "" + keyNumber;
			PlayerPrefs.SetInt ("Key", keyNumber);
			PlayerPrefs.SetString ("LLFBought", "true");
			PlayerPrefs.SetInt ("Lightninglance", 3);

			buySuccessPanel.SetActive (true);
			fireWorkEffect.SetActive (true);
		}
	}

	public void Key100()
	{
		IAPManager.Purchase (EM_IAPConstants.Product_Key_x_100);
	}

	public void Key200()
	{
		IAPManager.Purchase (EM_IAPConstants.Product_Key_x_200);
	}

	public void Key400()
	{
		IAPManager.Purchase (EM_IAPConstants.Product_Key_x_400);
	}

	public void RemoveAds()
	{
		//only interstitial
		IAPManager.Purchase (EM_IAPConstants.Product_Remove_Ad);
	}

	public void UnlockAllLevel()
	{
		IAPManager.Purchase (EM_IAPConstants.Product_Unlock_all_stages);
	}
}

