using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;
using EasyMobile;

public class AdsBannerClose1sec : MonoBehaviour 
{
	void Start()
	{
		ShowBannerAd ();
		Invoke ("HideBannerAds", 1);
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
