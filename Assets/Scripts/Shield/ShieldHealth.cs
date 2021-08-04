using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Slider healthBar;
    Transform shieldParent;
    private void Start()
    {
        shieldParent = FindObjectOfType<GrabAndThrow>().shieldParent;
    }
    public void OnPickedShield(Slider _healthBar)
    {
        healthBar = _healthBar;
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void ShieldDamage(float damageAmmount)
    {
        if (!GetComponent<Shield>().isGrabed)
        {
            return;
        }
        if (currentHealth>0)
        {
            currentHealth -= damageAmmount;
            healthBar.value = currentHealth;
            if (currentHealth<maxHealth/2)
            {
                //Warning!!!!
                Debug.Log("Shield in running out!!!");
            }
        }
        if(currentHealth<=0)
        {
            Debug.Log("Shield destroyed, find new one!!");
            transform.parent = shieldParent;
            GetComponent<Shield>().Respawn();
        }


    }
}
