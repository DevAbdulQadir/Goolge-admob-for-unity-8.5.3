using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Analytics;
using Firebase;
using Firebase.Crashlytics;
using GoogleMobileAds.Ump.Api;

public class AdsManager : MonoBehaviour
{
    #region Variables
    public static AdsManager Instance;
    private Action<bool> temp;
    private GoogleAdMobController googleAds;
    #endregion

    #region Common
    void Awake()
    {
        if (!Instance)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
            googleAds = GetComponent<GoogleAdMobController>();
            
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    private void Start()
    {
        
        StartConsent();
        OnFireBase();
    }


    void OnEnable()
    {

        ////Add AdInfo Rewarded Video Events
        //IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        //IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        //IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        //IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        //IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        //IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        //IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;



        ////Add AdInfo Interstitial Events
        //IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
        //IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
        //IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
        //IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
        //IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
        //IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
        //IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;

    }
    void OnDisable()
    {
        //Add AdInfo Interstitial Events
        //IronSourceInterstitialEvents.onAdReadyEvent -= InterstitialOnAdReadyEvent;
        //IronSourceInterstitialEvents.onAdLoadFailedEvent -= InterstitialOnAdLoadFailed;
        //IronSourceInterstitialEvents.onAdOpenedEvent -= InterstitialOnAdOpenedEvent;
        //IronSourceInterstitialEvents.onAdClickedEvent -= InterstitialOnAdClickedEvent;
        //IronSourceInterstitialEvents.onAdShowSucceededEvent -= InterstitialOnAdShowSucceededEvent;
        //IronSourceInterstitialEvents.onAdShowFailedEvent -= InterstitialOnAdShowFailedEvent;
        //IronSourceInterstitialEvents.onAdClosedEvent -= InterstitialOnAdClosedEvent;






        //Add AdInfo Rewarded Video Events
        //IronSourceRewardedVideoEvents.onAdOpenedEvent -= RewardedVideoOnAdOpenedEvent;
        //IronSourceRewardedVideoEvents.onAdClosedEvent -= RewardedVideoOnAdClosedEvent;
        //IronSourceRewardedVideoEvents.onAdAvailableEvent -= RewardedVideoOnAdAvailable;
        //IronSourceRewardedVideoEvents.onAdUnavailableEvent -= RewardedVideoOnAdUnavailable;
        //IronSourceRewardedVideoEvents.onAdShowFailedEvent -= RewardedVideoOnAdShowFailedEvent;
        //IronSourceRewardedVideoEvents.onAdRewardedEvent -= RewardedVideoOnAdRewardedEvent;
        //IronSourceRewardedVideoEvents.onAdClickedEvent -= RewardedVideoOnAdClickedEvent;

    }

    #endregion


    #region CallBacks

    public void InitialAdsNow()
    {
        googleAds.StartInitialization();
        StartCoroutine(RequestAdmobBanner(4));
    }



    public void ActionCallBack(bool value)
    {
        if (temp != null)
        {
            temp.Invoke(value);
        }

        if (value)
        {
            RewardedEvent("Completed");
        }
    }
    public bool HasInterstitial()
    {
        return googleAds.HasInterstitial;
    }
    #endregion
    

    #region AddOn

    //public GameObject _Canvas
    //{
    //    get => CanvasObj;
    //    set => CanvasObj = value;
    //}

    #endregion


    #region IronSourceRewardedCallbacks
    //void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
    //{
    //    Debug.Log("unity-script: I got InterstitialOnAdReadyEvent With AdInfo " + adInfo.ToString());
    //}

    //void InterstitialOnAdLoadFailed(IronSourceError ironSourceError)
    //{
    //    Debug.Log("unity-script: I got InterstitialOnAdLoadFailed With Error " + ironSourceError.ToString());
    //}

    //void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo)
    //{
    //    Debug.Log("unity-script: I got InterstitialOnAdOpenedEvent With AdInfo " + adInfo.ToString());
    //}

    //void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo)
    //{
    //    Debug.Log("unity-script: I got InterstitialOnAdClickedEvent With AdInfo " + adInfo.ToString());
    //}

    //void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo)
    //{
    //    Debug.Log("unity-script: I got InterstitialOnAdShowSucceededEvent With AdInfo " + adInfo.ToString());
    //    if (adInfo != null)
    //    {
    //        ImpressionSuccessEvent(adInfo);
    //    }
    //}

    //void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    //{
    //    Debug.Log("unity-script: I got InterstitialOnAdShowFailedEvent With Error " + ironSourceError.ToString() + " And AdInfo " + adInfo.ToString());
    //}

    //void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
    //{
    //    Debug.Log("unity-script: I got InterstitialOnAdClosedEvent With AdInfo " + adInfo.ToString());
    //}
    ///************* RewardedVideo AdInfo Delegates *************/
    //// Indicates that there’s an available ad.
    //// The adInfo object includes information about the ad that was loaded successfully
    //// This replaces the RewardedVideoAvailabilityChangedEvent(true) event
    //void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    //{
    //}
    //// Indicates that no ads are available to be displayed
    //// This replaces the RewardedVideoAvailabilityChangedEvent(false) event
    //void RewardedVideoOnAdUnavailable()
    //{
    //}
    //// The Rewarded Video ad view has opened. Your activity will loose focus.
    //void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    //{
    //}
    //// The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
    //void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    //{
    //}
    //// The user completed to watch the video, and should be rewarded.
    //// The placement parameter will include the reward data.
    //// When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
    //void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    //{
    //    try
    //    {
    //        ActionCallBack(true);
    //        IronSource.Agent.loadRewardedVideo();
    //    }
    //    catch
    //    {
    //        print("Reward not given");
    //    }

    //    if (adInfo != null)
    //    {
    //        ImpressionSuccessEvent(adInfo);
    //    }
    //}
    //// The rewarded video ad was failed to show.
    //void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
    //{
    //    ActionCallBack(false);
    //}
    //// Invoked when the video ad was clicked.
    //// This callback is not supported by all networks, and we recommend using it only if
    //// it’s supported by all networks you included in your build.
    //void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    //{
    //}



    #endregion


    #region InApp
    /*
    private CrossPlatformValidator validator;
    private string m_LastTransationID;
    private string m_LastReceipt;
    private IStoreController controller; // The Unity Purchasing system.
    private IExtensionProvider extensions; // The store-specific Purchasing subsystems.          //	[SerializeField]
    private string currentPurchaseID = null;
    public static string kProductIDSubscription = "subscription";

    // Apple App Store-specific product identifier for the subscription product.
    [SerializeField]
    private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";
    [SerializeField]
    // Google Play Store-specific product identifier subscription product.
    private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";
   
    public SKU[] Skus;
    void OnIAP()
    {
        var module = StandardPurchasingModule.Instance();
        var builder = ConfigurationBuilder.Instance(module);

        string receipt = builder.Configure<IAppleConfiguration>().appReceipt;
        //		In App Purchases may be restricted in a device’s settings, which can be checked for as follows:
        bool canMakePayments = builder.Configure<IAppleConfiguration>().canMakePayments;
        foreach (SKU pIDC in Skus)
        {
            builder.AddProduct(pIDC.purchaseID, pIDC.purchaseableType);
        }

        builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs {
           { kProductNameGooglePlaySubscription, GooglePlay.Name },
          { kProductNameAppleSubscription, MacAppStore.Name }
        });

        UnityPurchasing.Initialize(this, builder);
    }
         public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
          {
            this.controller = controller;
            this.extensions = extensions;

            extensions.GetExtension<IAppleExtensions>().RestoreTransactions(result =>
            {
                if (result)
                {
                    Debug.Log("Result for restoration started is true");
                }
                else
                {
                    // Restoration failed.
                }
            });

            for (int i = 0; i < Skus.Length; i++)
            {
                Skus[i].localPrice = controller.products.WithID(Skus[i].purchaseID).metadata.localizedPriceString;
            }
        }
        public void OnInitializeFailed(InitializationFailureReason error)
        {

        }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        bool validPurchase = true;
        m_LastTransationID = e.purchasedProduct.transactionID;
        m_LastReceipt = e.purchasedProduct.receipt;


#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
        validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
        try
        {
            var result = validator.Validate(e.purchasedProduct.receipt);

            foreach (IPurchaseReceipt productReceipt in result)
            {
                Debug.Log(productReceipt.productID);
                Debug.Log(productReceipt.purchaseDate);
                Debug.Log(productReceipt.transactionID);
            }
        }

        catch (IAPSecurityException)
        {
            Debug.Log("Invalid receipt, not unlocking content");
            //	validPurchase = false;
        }
#endif



        if (validPurchase)
        {
            Debug.Log("Purchase is valid");
            if (String.Equals(e.purchasedProduct.definition.id, currentPurchaseID, StringComparison.Ordinal))
            {
                Debug.Log("Product Type : " + e.purchasedProduct.definition.type);
                ActionCallBack(true);

            }
            else
            {
                ActionCallBack(false);
            }
        }

        return PurchaseProcessingResult.Complete;
    }
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        ActionCallBack(false);
    }
    void BuyProductID(string productId)
    {
        currentPurchaseID = productId;
        Debug.Log("Purchasing id received is " + productId);
        Debug.Log(string.Format("Purchasing product id is ", productId));
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            Product product = controller.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                controller.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    private bool IsInitialized()
    {
        return controller != null && extensions != null;
    }

    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {

        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = extensions.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                for (int i = 0; i < Skus.Length; i++)
                {
                    if (Skus[i].purchaseableType == ProductType.NonConsumable)
                    {
                        BuyProductID(Skus[i].purchaseID/*, purchaseIDController[i].itemType);
                    }
                }
            });

        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public enum PurchaseType
    {
        RemoveAds,
        UnlockLevels,
        CoinsPack,
        DiamondPack,
        SpecialOffer
    }
    [System.Serializable]
    public class SKU
    {
        public string purchaseID = "";
        public PurchaseType itemType;
        public UnityEngine.Purchasing.ProductType purchaseableType = UnityEngine.Purchasing.ProductType.Consumable;
        internal string localPrice = "$0.99";
    }


    */
    #endregion


    #region PopUps

    //public void ShowRateUsPopUp()
    //{
    //    if (PlayerPrefs.GetString("RateUsClicked").Equals("true"))
    //    {
    //        Debug.Log("User has rated this app, No need to show this pop up any more");
    //        return;
    //    }
    //    DialogBox dB = Instantiate(DialogBox, Vector3.zero, Quaternion.identity);
    //    dB.GetComponent<DialogBox>().SetPopUpDetails("RateUsPopUp", "Will you Rate Us?", "Yes", "No", "You might have liked our game, tap on YES and give us a review.", null, null);
    //    dB.transform.SetParent(CanvasObj.transform, false);
    //}

    //public void ShowRemoveAdsPopUp()
    //{
    //    if (PlayerPrefs.GetString("RemoveAds").Equals("true"))
    //    {
    //        Debug.Log("Remove Ads has been purchased, No need to show this pop up any more");
    //        return;
    //    }
    //    DialogBox dB = Instantiate(DialogBox, Vector3.zero, Quaternion.identity);
    //    dB.GetComponent<DialogBox>().SetPopUpDetails("RemoveAdsPopUp", "Want to remove ads?", "Yes", "No", "Tap on YES and purchase REMOVE_ADS bundle to make your game free from ads.", null, null);
    //    dB.transform.SetParent(CanvasObj.transform, false);
    //}

    /// <summary>
    /// General PopUp, last two parameters are optional
    /// </summary>
    /// <param name="popUpName"></param>
    /// <param name="heading"></param>
    /// <param name="positiveButtonText"></param>
    /// <param name="negativeButtonText"></param>
    /// <param name="instruction"></param>
    public void ShowPopUp(string popUpName, string heading, string positiveButtonText, string negativeButtonText = "", string instruction = "", Sprite image = null)
    {
        //DialogBox dB = Instantiate(DialogBox, Vector3.zero, Quaternion.identity);
        //if (negativeButtonText == "" && instruction != "")
        //    dB.GetComponent<DialogBox>().SetPopUpDetails(popUpName, heading, positiveButtonText, instruction, image);
        //else if (instruction == "")
        //    dB.GetComponent<DialogBox>().SetPopUpDetails(popUpName, heading, positiveButtonText, image);
        //else
        //    dB.GetComponent<DialogBox>().SetPopUpDetails(popUpName, heading, positiveButtonText, negativeButtonText, instruction, image, null);
        //dB.transform.SetParent(CanvasObj.transform, false);
    }



    #endregion


    #region Ads
    public void ReloadAllAds()
    {
        StartCoroutine(RequestAdmobInterstital(0));
        StartCoroutine(RequestAdmobRewarded(0.5f));
       

    }
    public void ReloadAllAdsLate()
    {
        StartCoroutine(RequestAdmobInterstital(1.5f));
        StartCoroutine(RequestAdmobRewarded(2.5f));


    }
    IEnumerator RequestAdmobInterstital(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(googleAds.HasInterstitial==false) googleAds.RequestAndLoadInterstitialAd();
       
        StopCoroutine("RequestAdmobInterstital");
    }
    IEnumerator RequestAdmobRewarded(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (googleAds.HasRewarded == false) googleAds.RequestAndLoadRewardedAd();
        StopCoroutine("RequestAdmobRewarded");
    }
    IEnumerator RequestAdmobBanner(float delay)
    {
        yield return new WaitForSeconds(delay);
        googleAds.RequestBannerAd();
        StopCoroutine("RequestAdmobBanner");
    }
    void InitIronSource()
    {
        //IronSource.Agent.init(ironsourceKey);
        //StartCoroutine(RequestIronSource());
        // StartCoroutine(RequestIronSourceBanner());
    }

    //IEnumerator RequestIronSource()
    //{
    //    yield return new WaitForSeconds(1);
    //    IronSource.Agent.loadInterstitial();
    //    StopCoroutine("RequestIronSource");
    //}
    //IEnumerator RequestIronSourceBanner()
    //{
    //    yield return new WaitForSeconds(1);
    //    IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.TOP);
    //    StopCoroutine("RequestIronSourceBanner");
    //}
    //bool HasRewardedVideo
    //{
    //    get => IronSource.Agent.isRewardedVideoAvailable();
    //}
    //bool HasIntnerstitialIronSource
    //{
    //    get => IronSource.Agent.isInterstitialReady();
    //}
    public void IAP(string sku, Action<bool> result)
    {
        temp = null;
        temp = result;
       // BuyProductID(sku);
    }
    //public void ShowIronSourceinterstiital()
    //{
    //    IronSource.Agent.showInterstitial();
    //}
    //public string LoaclPrice(PurchaseType type)
    //{
    //    string temp = "$ 0.99";
    //    for (int i = 0; i < Skus.Length; i++)
    //    {
    //        if (type == Skus[i].itemType)
    //        {
    //            temp = Skus[i].localPrice;
    //            break;
    //        }
    //    }
    //    return temp;
    //}
    public string LoaclPrice(string mySKu)
    {
        string temp = "$ 0.99";
        //for (int i = 0; i < Skus.Length; i++)
        //{
        //    if (mySKu == Skus[i].purchaseID)
        //    {
        //        temp = Skus[i].localPrice;
        //        break;
        //    }
        //}
        return temp;
    }



    public void Rewarded(Action<bool> result)
    {

        temp = null;
        temp = result;
        if (googleAds.HasRewarded)
        {
            googleAds.ResumedAd = true;
            googleAds.ShowRewardedAd();
        }
        //else if(HasRewardedVideo)
        //{
        //    IronSource.Agent.showRewardedVideo();
        //}
        else
        {
            ActionCallBack(false);
            googleAds.RequestAndLoadRewardedAd();


        }
    }

    public void Intersitial()
    {
        //if (RemoveAds)
        //    return;

        if (googleAds.HasInterstitial)
        {
            googleAds.ResumedAd = true;
            googleAds.ShowInterstitialAd();
        }
        //else if (HasIntnerstitialIronSource)
        //{
        //    IronSource.Agent.showInterstitial();
        //}
        else
        {
            googleAds.RequestAndLoadInterstitialAd();
        }
    }
    public void RewardedIntersitial()
    {
        

        if (googleAds.HasRewardedInterststial())
        {
            googleAds.ShowRewardedInterstitialAd();
        }
      
        else
        {
            MyGameManager.Instance.ShowAdNot();
            googleAds.RequestAndLoadRewardedInterstitialAd();
        }
    }
    public void Banner(bool value)
    {
        //if (RemoveAds)
        //    return;

        if (value)
            googleAds.RequestBannerAd();
        else
            googleAds.DestroyBannerAd();
    }

    public void RectBanner(bool value)
    {
        //if (RemoveAds)
        //    return;


        if (value)
            googleAds.RequestRectBannerAd();
        else
            googleAds.DestroyRectBannerAd();
    }

    public bool RemoveAds
    {
        get => PlayerPrefs.GetInt("RemoveAds", 0) == 1;
        set
        {
            if (value)
                Banner(false);
            PlayerPrefs.SetInt("RemoveAds", value ? 1 : 0);
        }
    }

    public void DummyReward()
    {
        Rewarded(result =>
        {
            if (result)
                print("");
        });
    }

    #endregion


    #region Event
    /// <summary>
    /// progressionID 1 Start
    /// progressionID 2 Complete
    /// progressionID 3 Fail
    /// </summary>
    /// <param name="progressionID"></param>
    /// <param name="getCurrentLevel"></param>

    public void CustomEvent(string status,int level)
    {
        SetAppmetrica("Level_" + level.ToString(), status);
    }
    public void DesignEvent(string message)
    {
        SetAppmetrica("Status", message);
    }
    public void RewardedEvent(string message)
    {
        SetAppmetrica("Video", message);
    }
    void SetAppmetrica(string status, string message)
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>();
        eventParameters.Add(status, message);
    }

    #endregion

    #region Internet-Connection
    public void InternetConnection(System.Action<bool> action)
    {
        StopCoroutine("CheckInternet");
        StartCoroutine(CheckInternet(action));
    }
    IEnumerator CheckInternet(System.Action<bool> action)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }

        else if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            action(true);
        }
        else
        {
            action(false);
        }

        StopCoroutine("CheckInternet");
    }

    #endregion Internet-Connection

    #region Firebase


    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    protected bool firebaseInitialized = false;


    void OnFireBase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                    "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }
    void InitializeFirebase()
    {
        //  Debug.Log("Enabling data collection.");
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

        // Debug.Log("Set user properties.");
        // Set the user's sign up method.
        FirebaseAnalytics.SetUserProperty(
            FirebaseAnalytics.UserPropertySignUpMethod,
            "Google");
        // Set the user ID.
        FirebaseAnalytics.SetUserId(SystemInfo.deviceUniqueIdentifier);
        // Set default session duration values.
        FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
        Crashlytics.ReportUncaughtExceptionsAsFatal = true;
        firebaseInitialized = true;
    }
    


    #endregion


    #region GDPR

    void StartConsent()
    {

        //var debugSettings = new ConsentDebugSettings
        //{
        //    // Geography appears as in EEA for debug devices.
        //    DebugGeography = DebugGeography.EEA,
        //    TestDeviceHashedIds = new List<string>
        //    {
        //         "B832B9A50083BF1D2289CD83B42E99CD"
        //    }
        //};


        // Create a ConsentRequestParameters object.
        ConsentRequestParameters request = new ConsentRequestParameters();

        // Check the current consent information status.
        ConsentInformation.Update(request, OnConsentInfoUpdated);
    }

    void OnConsentInfoUpdated(FormError consentError)
    {
        if (consentError != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(consentError);
            return;
        }

        // If the error is null, the consent information state was updated.
        // You are now ready to check if a form is available.
        ConsentForm.LoadAndShowConsentFormIfRequired((FormError formError) =>
        {
            if (formError != null)
            {
                // Consent gathering failed.
                UnityEngine.Debug.LogError(consentError);
                return;
            }

            // Consent has been gathered.
            if (ConsentInformation.CanRequestAds())
            {
                InitialAdsNow();

            }
        });

    }
    public void ResetConsent()
    {
        ConsentInformation.Reset();
    }

   

    #endregion
}