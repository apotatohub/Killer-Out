using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	public bool isDead;
    public EnemyMovement enemyMovement;
    public PlayerDetection playerDetection;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        playerDetection = GetComponent<PlayerDetection>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Throwable"))
        {
            if (collision.collider.GetComponent<Shield>().isThrown)
            {
                enemyMovement.enabled = false;
                playerDetection.enabled = false;
                gameObject.AddComponent<Rigidbody>();
                GetComponent<MeshRenderer>().material.color = Color.gray;
                isDead = true;
            }

        }
    }
}
