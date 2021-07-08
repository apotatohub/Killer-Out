using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCountManager : MonoBehaviour
{

    public int enemyCount;
    public Transform enemyList;
    public GameObject gameoverPanel;

    void Start()
    {
        gameoverPanel.SetActive(false);
        enemyCount = enemyList.childCount;
    }
    public void SetEnemyCount()
    {
        enemyCount--;
        if (enemyCount<=0)
        {
            gameoverPanel.SetActive(true);
        }
    }

}
