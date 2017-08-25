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
	GameObject buySuccessPanel;
	GameObject fireWorkEffect;

	void OnEnable()
	{            
		IAPManager.PurchaseCompleted += IAPManager_PurchaseCompleted;
		IAPManager.PurchaseFailed += IAPManager_PurchaseFailed;
		IAPManager.RestoreCompleted += IAPManager_RestoreCompleted;
	}

	void OnDisable()
	{
		IAPManager.PurchaseCompleted -= IAPManager_PurchaseCompleted;
		IAPManager.PurchaseFailed -= IAPManager_PurchaseFailed;   
		IAPManager.RestoreCompleted -= IAPManager_RestoreCompleted;
	}

	void IAPManager_PurchaseCompleted(IAPProduct product)
	{
		buySuccessPanel.SetActive (true);
		fireWorkEffect.SetActive (true);
	}

	void IAPManager_PurchaseFailed(IAPProduct product)
	{
	}

	void IAPManager_RestoreCompleted()
	{
	}

	void Awake()
	{
		buySuccessPanel = GameObject.Find ("BuySuccessPanel");
		fireWorkEffect = GameObject.Find ("Firework");
		buySuccessPanel.SetActive (false);
		fireWorkEffect.SetActive (false);

	}
		

	public void DonateSuccessOkClick()
	{
		buySuccessPanel.SetActive (false);
	}
		

	public void Donate_Least()
	{
		IAPManager.Purchase (EM_IAPConstants.Product_Least_Donation);
	}

	public void Donate_Less()
	{
		IAPManager.Purchase (EM_IAPConstants.Product_Less_Donation);
	}

	public void Donate_Little()
	{
		IAPManager.Purchase (EM_IAPConstants.Product_Little_Donation);
	}

	public void Donate_Much()
	{
		IAPManager.Purchase (EM_IAPConstants.Product_Much_Donation);
	}

	public void Donate_More()
	{
		IAPManager.Purchase (EM_IAPConstants.Product_More_Donation);
	}

	public void Donate_Most()
	{
		IAPManager.Purchase (EM_IAPConstants.Product_Most_Donation);
	}

		



		
}

