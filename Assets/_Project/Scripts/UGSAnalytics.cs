using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;
using UnityEngine.UnityConsent;


public class UGS_Analytics : MonoBehaviour
{
    async void Start()
    {
            await UnityServices.InitializeAsync();
            GiveConsent(); //Get user consent according to various legislations
            LevelCompletedCustomEvent();
    }


    private void LevelCompletedCustomEvent()
    {
        int currentLevel = Random.Range(1, 4); //Gets a random number from 1-3


        //Define Custom Parameters
        CustomEvent myEvent = new CustomEvent("levelCompleted")
        	{
            	{ "levelName", "level" + currentLevel.ToString()}
        	};


        //The ‘levelCompleted’ event will get cached locally
        //and sent during the next scheduled upload, within 1 minute
        AnalyticsService.Instance.RecordEvent(myEvent);


        // You can call Events.Flush() to send the event immediately
        AnalyticsService.Instance.Flush();
    }


    public void GiveConsent()
    {
        // Call if consent has been given by the user
        EndUserConsent.SetConsentState(new ConsentState
        {
             AnalyticsIntent = ConsentStatus.Granted,
        });
        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }


}