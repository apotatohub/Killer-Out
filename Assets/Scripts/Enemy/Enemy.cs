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
            if (collision.collider.GetComponent<Shield>().isThrown && !collision.collider.GetComponent<Shield>().isDamaged && !isDead)
            {
                isDead = true;
                enemyCountManager.SetEnemyCount();
                navAgent.enabled=false;
                enemyMovement.enabled = false;
                playerDetection.enabled = false;
                if (!GetComponent<Rigidbody>())
                {
                    gameObject.AddComponent<Rigidbody>();
                }
                GetComponent<MeshRenderer>().material.color = Color.gray;
              
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
