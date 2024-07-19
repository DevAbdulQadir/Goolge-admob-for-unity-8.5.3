# Goolge admob for unity 8.5.3
 Ads Package
Attach AdsManager.cs and GoogleAdMobController.cs to a object and place that object in first scene of game.

Copy and paste these lines to call ads.
AdsManager.Instance.Banner(true);
AdsManager.Instance.Intersitial();
AdsManager.Instance.Rewarded(result =>
        {
            if (result)
                Reward();
        });