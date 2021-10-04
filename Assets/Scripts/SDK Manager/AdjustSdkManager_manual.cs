using com.adjust.sdk;
using UnityEngine;

public class AdjustSdkManager_manual : MonoBehaviour
{
    public static AdjustSdkManager_manual Instance;
    public string appToken = "{Your App Token}";
    public LogEventWithToken[] logEvents;
    void Start()
    {
        
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (IsEditor())
        {
            return;
        }
        AdjustEnvironment environment = AdjustEnvironment.Production;
        AdjustConfig config = new AdjustConfig(appToken, environment, true);
        config.setLogLevel(AdjustLogLevel.Suppress);
        Adjust.start(config);
        DontDestroyOnLoad(this.gameObject);
    }

 

    private static bool IsEditor()
    {
#if UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }

   public void CallEvent(string eventName)
   {
        for (int i = 0; i < logEvents.Length; i++)
        {
            if (logEvents[i].eventName==eventName)
            {
                logEvents[i].logEvent.trackEvent(logEvents[i].eventToken);
            }
        }
       
   }
}
[System.Serializable]
public class LogEvent
{
    public void trackEvent(string eventToken)
    {
        if (IsEditor())
        {
            return;
        }
        AdjustEvent adjustEvent = new AdjustEvent(eventToken);
        if (adjustEvent == null)
        {
            Debug.Log("[Adjust]: Missing event to track.");
            return;
        }
#if UNITY_IOS
            AdjustiOS.TrackEvent(adjustEvent);
#elif UNITY_ANDROID
        AdjustAndroid.TrackEvent(adjustEvent);
#elif (UNITY_WSA || UNITY_WP8)
            AdjustWindows.TrackEvent(adjustEvent);
#else
            Debug.Log(errorMsgPlatform);
#endif
    }

    private static bool IsEditor()
    {
#if UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }

}
[System.Serializable]
public class LogEventWithToken
{
    public string eventName;
    public string eventToken;
    [TextArea(5, 7)]
    public string eventDescription; 
    [HideInInspector]
    public LogEvent logEvent;
}
