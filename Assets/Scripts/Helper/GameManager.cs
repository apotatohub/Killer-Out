using com.adjust.sdk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameoverPanel,enemyClearEffect;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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
