using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;
using EasyMobile;
	
public class Ads_Wizard : MonoBehaviour
{

		void OnEnable()
		{
			AdManager.InterstitialAdCompleted += AdManager_InterstitialAdCompleted;    
		}

		void OnDisable()
		{
			AdManager.InterstitialAdCompleted -= AdManager_InterstitialAdCompleted;  
		}

		void AdManager_InterstitialAdCompleted(InterstitialAdNetwork arg1, AdLocation arg2)
		{
		 	
		}

		void Start()
		{
			AdSettings.DefaultAdNetworks defaultNetworks = new AdSettings.DefaultAdNetworks(BannerAdNetwork.None, InterstitialAdNetwork.None, RewardedAdNetwork.None);

			#if UNITY_ANDROID
			defaultNetworks = EM_Settings.Advertising.AndroidDefaultAdNetworks;
			#elif UNITY_IOS
			defaultNetworks = EM_Settings.Advertising.IosDefaultAdNetworks;
			#endif
		}
		

		public void ShowInterstitialAd()
		{
			if (AdManager.IsInterstitialAdReady())
			{
				AdManager.ShowInterstitialAd();
			}
		}


}

