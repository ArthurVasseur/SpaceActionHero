using System.Collections;
using System.Collections.Generic;
using Ads;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAds : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    private string gameId = "null";
#elif UNITY_ANDROID
    private readonly string gameId = AdsController.GameIdAndroid;
#else
    private readonly string gameId = "null";
#endif

    Button myButton;
    public string myPlacementId = "video";

    private void Start()
    {
        return;
#if !UNITY_WEBGL
        myButton = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady(myPlacementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener(this.ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, AdsController.TestMode);
#endif
    }

    // Implement a function for showing a rewarded video ad:
    private void ShowRewardedVideo()
    {
        return;
#if !UNITY_WEBGL
        Advertisement.Show(myPlacementId);
#endif
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        return;
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        return;
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
            Debug.LogWarning("The ad did not finish due to an error");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}