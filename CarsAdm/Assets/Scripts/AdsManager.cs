using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour {
    
    private string adUnitId = "ca-app-pub-3666412446678119/4565983625";
    private InterstitialAd interstitial;
    private int nowLoses;

    private void Start() {
        DontDestroyOnLoad(gameObject);
        DestroyAndStartNew(true);
    }

    private void Update() {
        if (interstitial.IsLoaded() && GameController.countLoses % 3 == 0 && GameController.countLoses != 0 && GameController.countLoses != nowLoses) {
            nowLoses = GameController.countLoses;
            interstitial.Show();
        }
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        DestroyAndStartNew();
    }

    public void HandleOnAdClosed(object sender, EventArgs args) {
        DestroyAndStartNew();
    }

    public void HandleOnAdDidRecordImpression(object sender, EventArgs args) {
        Debug.Log("Ad impression recorded");
    }

    void DestroyAndStartNew(bool isFirst = false) {
        if(!isFirst)
            interstitial.Destroy();
        
        interstitial = new InterstitialAd(adUnitId);
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdDidRecordImpression += HandleOnAdDidRecordImpression;
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
}
