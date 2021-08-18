using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            GameManager.Instance.GameOver();           
        }
        
    }

}
