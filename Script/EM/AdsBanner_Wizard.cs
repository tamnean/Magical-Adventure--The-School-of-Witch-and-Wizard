using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;
using EasyMobile;

public class AdsBanner_Wizard : MonoBehaviour 
{
	void Start()
	{
		ShowBannerAd ();
	}

	void ShowBannerAd()
	{
		AdManager.ShowBannerAd(BannerAdPosition.Bottom);
	}

	public void HideBannerAds()
	{
		AdManager.HideBannerAd();
	}
		
}
