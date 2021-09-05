﻿using UnityEngine;
using Facebook.Unity;


public class FacebookSDKManager_Manual : MonoBehaviour
{
   
// Awake function from Unity's MonoBehavior
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback);
        }
        else
        {
        // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

 
        private void InitCallback()
        {
            if (FB.IsInitialized)
            {
                // Signal an app activation App Event
                FB.ActivateApp();
                // Continue with Facebook SDK
                // ...
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }
}
