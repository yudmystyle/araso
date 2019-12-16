using System;
using UnityEngine;
using System.Collections;
#if USE_ADMOB
using GoogleMobileAds.Api;
#endif

/*
 * Download the package here - https://github.com/googleads/googleads-mobile-unity/releases
 * Get the Ad Units from here - https://www.google.com/admob/
*/

[Serializable]
public enum TestMethod
{
    EnableTestMode,
    DisableTestMode
}


public class AdmobManager : MonoBehaviour
{
#if USE_ADMOB

    //NOTE: DO NOT CHANGE THE GOOGLE TEST IDS. DON'T EVEN THINK ABOUT.
    internal string GoogleSampleTestID = "ca-app-pub-3940256099942544/1033173712";
    internal string GoogleSampleAppId = "ca-app-pub-3940256099942544~3347511713";

    internal bool NPA;

    internal TestMethod TestMode = TestMethod.DisableTestMode;

    private InterstitialAd interstitial;

    private string LastID;

    internal static AdmobManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    //	#if UNITY_ANDROID
    //	public static string GetAdMobID() {
    //		UnityEngine.AndroidJavaClass up = new UnityEngine.AndroidJavaClass("com.unity3d.player.UnityPlayer");
    //		UnityEngine.AndroidJavaObject currentActivity = up.GetStatic<UnityEngine.AndroidJavaObject>("currentActivity");
    //		UnityEngine.AndroidJavaObject contentResolver = currentActivity.Call<UnityEngine.AndroidJavaObject>("getContentResolver");
    //		UnityEngine.AndroidJavaObject secure = new UnityEngine.AndroidJavaObject("android.provider.Settings$Secure");
    //		string deviceID = secure.CallStatic<string>("getString", contentResolver, "android_id");
    //		return Md5Sum(deviceID).ToUpper();
    //	}
    //	#endif
    //
    //	#if UNITY_IPHONE
    //	public static string GetAdMobID() {
    //	return Md5Sum(UnityEngine.iPhone.advertisingIdentifier);
    //	}
    //	#endif
    //	public static string Md5Sum(string strToEncrypt) {
    //		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
    //		byte[] bytes = ue.GetBytes(strToEncrypt);
    //
    //		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
    //		byte[] hashBytes = md5.ComputeHash(bytes);
    //
    //		string hashString = ""; 
    //		for (int i = 0; i < hashBytes.Length; i++) {
    //			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
    //		}
    //
    //		return hashString.PadLeft(32, '0');
    //	}

    internal void RequestInterstitial(string InterstitialAdUnitId)
    {
        LastID = InterstitialAdUnitId;

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(InterstitialAdUnitId);

        // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when the user returned from the app after an ad click.
        interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request;

        // Create an ad request.
        if (NPA)
            request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddExtra("npa", "1").Build();
        else
            request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).Build();


        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    internal void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
            RequestInterstitial(LastID);
    }

    internal void CheckStatus()
    {
        if (interstitial.IsLoaded())
            return;
        else
            RequestInterstitial(LastID);
    }

    void DestroyInterstitial()
    {
        // Destroy the listeners
        interstitial.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        interstitial.OnAdClosed -= HandleOnAdClosed;
        interstitial.OnAdLeavingApplication -= HandleOnAdLeavingApplication;

        if (interstitial != null)
            interstitial.Destroy();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
#if DEBUG
        print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
#endif
        if (interstitial != null)
            DestroyInterstitial();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
#if DEBUG
        print("HandleInterstitialClosed event received");
#endif
        if (interstitial != null)
            DestroyInterstitial();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
#if DEBUG
        print("HandleInterstitialLeftApplication event received");
#endif
        if (interstitial != null)
            DestroyInterstitial();
    }
#endif
}