using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAdsManager : MonoBehaviour
{
	private RewardedAd rewardedAd;
	private BannerView bannerView;
	private InterstitialAd interstitial;

	public void Start()
	{
		MobileAds.Initialize(initStatus => { });

		RequestBanner();

		RequestRewardAd();
	}

	public void UserChoseToWatchAd()
	{
		if (rewardedAd.IsLoaded()) {
			Debug.Log("Watch Ads");
			rewardedAd.Show();
		}
	}

	public void RequestBanner()
	{
		bannerView = new BannerView("ca-app-pub-3940256099942544/2934735716", AdSize.Banner, AdPosition.Bottom);

		AdRequest request = new AdRequest.Builder().Build();
		bannerView.LoadAd(request);
	}

	public void RequestRewardAd()
	{
		string adUnitId;
		#if UNITY_ANDROID
		            adUnitId = "ca-app-pub-3940256099942544/5224354917";
		#elif UNITY_IPHONE
				adUnitId = "ca-app-pub-3940256099942544/1712485313";
		#else
		            adUnitId = "unexpected_platform";
		#endif

		this.rewardedAd = new RewardedAd(adUnitId);
		AdRequest request = new AdRequest.Builder().Build();
		rewardedAd.LoadAd(request);

		// Called when an ad request has successfully loaded.
		//this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		// Called when an ad is shown.
		//this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		//this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
	}


	public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToLoad event received with message: "
							 + args.Message);
	}

	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToShow event received with message: "
							 + args.Message);
	}

	public void HandleUserEarnedReward(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		Debug.Log(
			"HandleRewardedAdRewarded event received for "
						+ amount.ToString() + " " + type);

		UserManager.Instance.IncreaseCoin(LevelManager.Instance.currentWeapon.rewardCoins);
	}
}
