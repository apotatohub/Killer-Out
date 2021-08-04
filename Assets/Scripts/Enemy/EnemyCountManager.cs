using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCountManager : MonoBehaviour
{

    public int enemyCount;
    public Transform enemyList;
    public Text enemyCountText; 

    void Start()
    {
        enemyCountText = GameObject.FindGameObjectWithTag("Enemy Count Text").GetComponent<Text>();
        enemyCount = enemyList.childCount;
        enemyCountText.text = enemyCount.ToString();
    }
    public void SetEnemyCount()
    {
        enemyCount--;
        enemyCountText.text = enemyCount.ToString();
        if (enemyCount<=0)
        {
            GameManager.Instance.GameOver();
           
        }
        
    }

}
