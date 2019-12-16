using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAdsManager : MonoBehaviour
{
	private RewardedAd rewardedAd;

	public void Start()
	{
		MobileAds.Initialize(initStatus => { });

		rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/1712485313");

		AdRequest request = new AdRequest.Builder().Build();
		rewardedAd.LoadAd(request);
	}

	public void UserChoseToWatchAd()
	{
		Debug.Log("Watch Ads");

		if (rewardedAd.IsLoaded()) {
			rewardedAd.Show();
		}
	}
}
