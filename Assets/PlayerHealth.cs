﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;
    public Slider healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void PlayerDamage(float damageAmmount)
    {
        if (currentHealth>0)
        {
            currentHealth -= damageAmmount;
            healthBar.value = currentHealth;
        }
        if(currentHealth<=0)
        {
            //Play Death Animation
            Debug.Log("Player is done playing!!!");
        }
    }
}
