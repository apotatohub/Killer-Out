using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EZCameraShake;
public class Enemy : MonoBehaviour
{
    public bool isSmartToMove, isSmartToDetect, isSmartToShoot;
    public bool isDead;
    public EnemyMovement enemyMovement;
    public PlayerDetection playerDetection;
    public GameObject bulletPrefabs;
    public Transform shootingPos;
    public NavMeshAgent navAgent;
    public EnemyCountManager enemyCountManager;
    public Animator anim;
    public GameObject bloodParticles;
    public float BPS = 3;
    float bulletShootTime;
    float minimumVelocity = 0;
    Transform player;

    RagdollActivate ragdoll;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ragdoll = GetComponentInChildren<RagdollActivate>();
        if (!enemyCountManager)
        {
            enemyCountManager = FindObjectOfType<EnemyCountManager>();
        }
        navAgent = GetComponent<NavMeshAgent>();
        enemyMovement = GetComponent<EnemyMovement>();
        //playerDetection = GetComponent<PlayerDetection>();
        //BPS = PlayerPrefs.GetFloat("BPS");
        bulletShootTime = 1 / BPS;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Shield"))
        {
            if (!collision.collider.GetComponent<Shield>().isDamaged && !isDead && collision.collider.GetComponent<Shield>().isThrown)
            {
                if (collision.relativeVelocity.magnitude >= minimumVelocity)
                {
                    
                    bloodParticles.SetActive(true);
                    CameraShaker.Instance.ShakeOnce(4, 2, 0.1f, 1);
                    isDead = true;
                    collision.collider.GetComponent<Shield>().isDamaged = true;
                    enemyCountManager.SetEnemyCount();
                    navAgent.enabled = false;
                    enemyMovement.enabled = false;
                    playerDetection.viewMeshFilter.gameObject.SetActive(false);
                    playerDetection.enabled = false;
                    anim.enabled = false;
                    //gameObject.AddComponent<Rigidbody>().AddForce(collision.relativeVelocity * 300,ForceMode.Force);
                    //Ragdol Enabling Here
                    ragdoll.SetState(true);
                    //GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.gray;
                   
                    
                }

            }

        }
    }
    private void Update()
    {
        if (isDead)
        {
            return;
        }
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

    private void Shoot()
    {
        if (isSmartToShoot)
        {
             Instantiate(bulletPrefabs, shootingPos.position, Quaternion.identity).GetComponent<Bullet>().direction = shootingPos.forward;         
        }
           
    }
}
