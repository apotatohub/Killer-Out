using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyCountManager : MonoBehaviour
{

    public int enemyCount;
    public Transform enemyList;

    void Start()
    {
        enemyCount = enemyList.childCount;    
    }
    public void SetEnemyCount()
    {
        enemyCount--;
       
        if (enemyCount<=0)
        {
            GameAnalyticsManager_Manual.Instance.OnGameSucceed((SceneManager.GetActiveScene().buildIndex + 1).ToString());
            GameManager.Instance.GameOver();           
        }
        
    }

}
