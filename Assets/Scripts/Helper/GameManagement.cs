using com.adjust.sdk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance;
    public GameObject gameoverPanel,enemyClearEffect;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        gameoverPanel.SetActive(false);
    }
    public void GameOver()
    {
        gameoverPanel.SetActive(true);
        enemyClearEffect.GetComponent<Animator>().Play("Hit");
    }

}
