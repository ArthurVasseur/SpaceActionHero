using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Ads
{
    [RequireComponent(typeof(Button))]
    public class AutomaticAd : MonoBehaviour, IUnityAdsListener
    {

#if UNITY_IOS
    private readonly string gameId = "1486551";
#elif UNITY_ANDROID
        private readonly string gameId = AdsController.GameIdAndroid;
#elif UNITY_WEBGL
    private readonly string gameId = "";
#endif

        public static AutomaticAd instance;
        private const string MyPlacementId = "video";
        public bool AdsReady = true;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
#if !UNITY_WEBGL
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, AdsController.TestMode);
#endif
        }

        // Implement a function for showing a rewarded video ad:
        public void ShowRewardedVideo()
        {
#if !UNITY_WEBGL
            GameManager.SetGamePause(true);
            Advertisement.Show(MyPlacementId);
#endif
        }

        // Implement IUnityAdsListener interface methods:
        public void OnUnityAdsReady(string placementId)
        {
            // If the ready Placement is rewarded, activate the button: 
            AdsReady = placementId == MyPlacementId;
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                // Reward the user for watching the ad to completion.
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
            }
            GameManager.SetGamePause(false);
        }

        public void OnUnityAdsDidError(string message)
        {
            GameManager.SetGamePause(false);
        }
        public void OnUnityAdsDidStart(string placementId)
        {
            GameManager.SetGamePause(true);

        }
    }
}