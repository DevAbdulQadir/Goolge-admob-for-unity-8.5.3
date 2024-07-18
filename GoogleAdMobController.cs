using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;

public class GoogleAdMobController : MonoBehaviour
{
    [SerializeField] string AppID, BannerID, RectBannerID, InterstitialID, RewardedID, RewardedInterstitialID, AppOpenID;
    [Space(10),Header("For Testing")]
    [SerializeField] bool TestAds;
    private AppOpenAd appOpenAd;
    private BannerView bannerView, rectBannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private RewardedInterstitialAd rewardedInterstitialAd;
    private float deltaTime;
    private bool isShowingAppOpenAd;
    [SerializeField] GameObject LoadingCanvasObj;
    #region UNITY MONOBEHAVIOR METHODS

    public void StartInitialization()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);

        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

        // Add some test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
        deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
#elif UNITY_ANDROID
        deviceIds.Add("75EF8D155528C04DACBBA6F36F433035");
#endif

        // Configure TagForChildDirectedTreatment and test device IDs.
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
            .SetTestDeviceIds(deviceIds).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            //statusText.text = "Initialization complete";
            
            RequestAndLoadInterstitialAd();
           // RequestAndLoadRewardedInterstitialAd();
            RequestAndLoadRewardedAd();
            RequestAndLoadAppOpenAd();
        });
    }

    #endregion

    #region HELPER METHODS

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
    }

    //public void OnApplicationPause(bool paused)
    //{
    //    // Display the app open ad when the app is foregrounded.
    //    if (!paused)
    //    {
    //        ShowAppOpenAd();
    //    }
    //}

    #endregion




    #region BANNER ADS

    public void RequestBannerAd()
    {


        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
        if (TestAds) BannerID = "ca-app-pub-3940256099942544/6300978111";
        // Create a 320x50 banner at top of the screen
        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        bannerView = new BannerView(BannerID.Trim(), adaptiveSize, AdPosition.Bottom);

        // Add Event Handlers

       
        // Load a banner ad
        bannerView.LoadAd(CreateAdRequest());

        ListenToAdEvents();
        
    }

    
    

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    private void ListenToAdEvents()
    {
        
        // Raised when an ad fails to load into the banner view.
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            RequestBannerAd();
        };
        
    }
    #endregion

    #region RECT BANNER ADS

    public void RequestRectBannerAd()
    {


        // Clean up banner before reusing
        if (rectBannerView != null)
        {
            rectBannerView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        rectBannerView = new BannerView(RectBannerID.Trim(), AdSize.MediumRectangle, AdPosition.BottomLeft);

        // Add Event Handlers
       

        // Load a banner ad
        rectBannerView.LoadAd(CreateAdRequest());
    }

    public void DestroyRectBannerAd()
    {
        if (rectBannerView != null)
        {
            rectBannerView.Destroy();
        }
    }


    #endregion

    #region INTERSTITIAL ADS

    public void RequestAndLoadInterstitialAd()
    {


        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        //Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        //var adRequest = new AdRequest();

        // send the request to load the ad.
        string TempId;
        if (TestAds)
            TempId = "ca-app-pub-3940256099942544/1033173712";
        else 
            TempId = InterstitialID;

        InterstitialAd.Load(TempId, CreateAdRequest(),
            (InterstitialAd ad, LoadAdError error) =>
            {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
                RegisterEventHandlersInterstitial(interstitialAd);
            });


        
    }

    private void RegisterEventHandlersInterstitial(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
              RequestAndLoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            // RequestAndLoadInterstitialAd();

        };
    }

    public bool HasInterstitial
    {
        get
        {
            if (interstitialAd != null && interstitialAd.CanShowAd())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void ShowInterstitialAd()
    {
        interstitialAd.Show();
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }

    #endregion

    #region REWARDED ADS

    public void RequestAndLoadRewardedAd()
    {
        // create new rewarded ad instance
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        string TempId;
        if (TestAds)
            TempId = "ca-app-pub-3940256099942544/5224354917";
        else
            TempId = RewardedID;

        // send the request to load the ad.
        RewardedAd.Load(RewardedID, CreateAdRequest(),
            (RewardedAd ad, LoadAdError error) =>
            {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                RegisterEventHandlersVideoAd(rewardedAd);
            });
        
    }

    private void RegisterEventHandlersVideoAd(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
          //  AdsManager.Instance.ActionCallBack(true);
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            AdsManager.Instance.ActionCallBack(false);
            RequestAndLoadRewardedAd();

        };
    }
    public bool HasRewarded
    {
        get
        {
            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                return true;
            }
            else
            {
                
                return false;
            }
        }
    }
    public void ShowRewardedAd()
    {
        const string rewardMsg =
         "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                AdsManager.Instance.ActionCallBack(true);
                RequestAndLoadRewardedAd();
            });
        }
    }

    public void RequestAndLoadRewardedInterstitialAd()
    {

        // Create an interstitial.
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Destroy();
            rewardedInterstitialAd = null;
        }
        string TempId;
        if (TestAds)
            TempId = "ca-app-pub-3940256099942544/5354046379";
        else
            TempId = RewardedInterstitialID;
        Debug.Log("Loading the rewarded interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedInterstitialAd.Load(TempId, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    Debug.LogError("rewarded interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedInterstitialAd = ad;
            });
        RegisterEventHandlers(rewardedInterstitialAd);
    }

    public void ShowRewardedInterstitialAd()
    {
        if (rewardedInterstitialAd != null )
        {
            rewardedInterstitialAd.Show((Reward reward) =>
            {
                MyGameManager.Instance.SelectCrossbow();
                RequestAndLoadRewardedInterstitialAd();
            });
        }

    }

    private void RegisterEventHandlers(RewardedInterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded interstitial ad paid {0} {1}.",
            adValue.Value,
            adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded interstitial ad failed to open " +
                           "full screen content with error : " + error);
        };
    }
        public bool HasRewardedInterststial()
    {
        return rewardedInterstitialAd.CanShowAd();
    }
    #endregion

    #region APPOPEN ADS

    DateTime _expireTime;

    public void RequestAndLoadAppOpenAd()
    {
        string TempId;
        if (TestAds)
            TempId = "ca-app-pub-3940256099942544/9257395921";
        else
            TempId = AppOpenID;
        Debug.Log("Loading the app open ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        AppOpenAd.Load(TempId, adRequest,
            (AppOpenAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("app open ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("App open ad loaded with response : "
                          + ad.GetResponseInfo());
                _expireTime = DateTime.Now + TimeSpan.FromHours(4);
                appOpenAd = ad;

            });
    }
    private void RegisterEventHandlersAppOpen(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("App open ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("App open ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("App open ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            isShowingAppOpenAd = true;
            Debug.Log("App open ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            isShowingAppOpenAd = false;
            LoadingCanvasObj.SetActive(false);
            RequestAndLoadAppOpenAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            isShowingAppOpenAd = false;
            LoadingCanvasObj.SetActive(false);
            RequestAndLoadAppOpenAd();
        };
    }
    public void ShowAppOpenAd()
    {
        if (IsAppOpenAdAvailable)
        {
            if (isShowingAppOpenAd)
            {
                return;
            }
            StartCoroutine(AppOpenShowLate());
        }

    }
    IEnumerator AppOpenShowLate()
    {
        LoadingCanvasObj.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        RegisterEventHandlersAppOpen(appOpenAd);
        appOpenAd.Show();


    }
    public bool IsAppOpenAdAvailable
    {
        get
        {
            return appOpenAd != null
                   && appOpenAd.CanShowAd()
                   && DateTime.Now < _expireTime;
        }
    }
    [HideInInspector] public bool ResumedAd;
    private void OnApplicationFocus(bool focus)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!focus) { if (!ResumedAd) ShowAppOpenAd(); ResumedAd = false; }
        }


    }
    #endregion


    #region AD INSPECTOR

    public void OpenAdInspector()
    {
        //statusText.text = "Open Ad Inspector.";

        MobileAds.OpenAdInspector((error) =>
        {
            if (error != null)
            {
                string errorMessage = error.GetMessage();
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                   // statusText.text = "Ad Inspector failed to open, error: " + errorMessage;
                });
            }
            else
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                   // statusText.text = "Ad Inspector closed.";
                });
            }
        });
    }

    #endregion
}
