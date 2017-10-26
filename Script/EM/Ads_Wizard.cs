using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;
using EasyMobile;
	
public class Ads_Wizard : MonoBehaviour
{
	string result;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	void OnEnable()
	{
		AdManager.RewardedAdCompleted += AdManager_RewardedAdCompleted;
	}

	void OnDisable()
	{
		AdManager.RewardedAdCompleted -= AdManager_RewardedAdCompleted;
	}

	void OnLevelWasLoaded ()
	{
		AdSettings.DefaultAdNetworks defaultNetworks = new AdSettings.DefaultAdNetworks (BannerAdNetwork.None, InterstitialAdNetwork.None, RewardedAdNetwork.None);

		#if UNITY_ANDROID
		defaultNetworks = EM_Settings.Advertising.AndroidDefaultAdNetworks;
		#elif UNITY_IOS
			defaultNetworks = EM_Settings.Advertising.IosDefaultAdNetworks;
		#endif
	}
		
	public void ShowInterstitialAd ()
	{
		if (AdManager.IsInterstitialAdReady ()) 
		{
			AdManager.ShowInterstitialAd ();
		}
	}

	public void ShowRewardedAd(string _result)
	{
		if (AdManager.IsRewardedAdReady())
		{
			AdManager.ShowRewardedAd();
			result = _result;
		}
	}

	void AdManager_RewardedAdCompleted(RewardedAdNetwork arg1, AdLocation arg2)
	{
		if (result == "reward") 
		{
			EventManager.AfterRewardAd.Invoke ();
		} 
		else if (result == "relife") 
		{
			EventManager.AfterRelifeAd.Invoke ();
		}
	}
}

