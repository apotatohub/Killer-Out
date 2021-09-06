using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAnalitycsManager_Manual : MonoBehaviour, IGameAnalyticsATTListener
{
    public static GameAnalitycsManager_Manual Instance;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GameAnalytics.RequestTrackingAuthorization(this);
        }
        else
        {
            GameAnalytics.Initialize();                                                                              
        }
    }

    public void GameAnalyticsATTListenerNotDetermined()
    {
        GameAnalytics.Initialize();
    }
    public void GameAnalyticsATTListenerRestricted()
    {
        GameAnalytics.Initialize();
    }
    public void GameAnalyticsATTListenerDenied()
    {
        GameAnalytics.Initialize();
    }
    public void GameAnalyticsATTListenerAuthorized()
    {
        GameAnalytics.Initialize();
    }

    public void OnGameStart(string levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level_Num", levelNumber);
    }
    public void OnGameSucceed(string levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level_Num", levelNumber);
    }
    public void OnGameFailed(string levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level_Num", levelNumber);
    }
    public void OnGameEnd(string levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "Level_Num", levelNumber);
    }
}
