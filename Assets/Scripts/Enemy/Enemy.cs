using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

	public bool isDead;
    public EnemyMovement enemyMovement;
    public PlayerDetection playerDetection;
    public GameObject bulletPrefabs;
    public Transform shootingPos;
    public NavMeshAgent navAgent;
    public EnemyCountManager enemyCountManager;
 
    public float BPS = 3;
    float bulletShootTime;
    float minimumVelocity=15;

    private void Start()
    {
        if (!enemyCountManager)
        {
            enemyCountManager = FindObjectOfType<EnemyCountManager>();
        }
        navAgent = GetComponent<NavMeshAgent>();
        enemyMovement = GetComponent<EnemyMovement>();
        playerDetection = GetComponent<PlayerDetection>();
        //BPS = PlayerPrefs.GetFloat("BPS");
        bulletShootTime = 1 / BPS;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Shield"))
        {
            if (!collision.collider.GetComponent<Shield>().isDamaged && !isDead && collision.collider.GetComponent<Shield>().isThrown)
            {
                if (collision.relativeVelocity.magnitude >= minimumVelocity || true)
                {
                    if (!GetComponent<Rigidbody>())
                    {
                        gameObject.AddComponent<Rigidbody>().AddForce(collision.relativeVelocity*10);
                    }

                    isDead = true;
                    collision.collider.GetComponent<Shield>().isDamaged = true;
                    enemyCountManager.SetEnemyCount();
                    navAgent.enabled = false;
                    enemyMovement.enabled = false;
                    playerDetection.viewMeshFilter.gameObject.SetActive(false);
                    playerDetection.enabled = false;                
                    GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.gray;

                }

            }

        }
    }
    private void Update()
    {
        if (!isDead)
        {
            if (playerDetection.isDetected)
            {
                if (bulletShootTime <= 0)
                {
                    Shoot();
                    bulletShootTime = 1;
                }
                else
                {
                    bulletShootTime -= Time.deltaTime;
                }
            }

        }

    }

    private void Shoot()
    {
        Instantiate(bulletPrefabs,shootingPos.position,Quaternion.identity).GetComponent<Bullet>().direction = shootingPos.forward;
    }
}
