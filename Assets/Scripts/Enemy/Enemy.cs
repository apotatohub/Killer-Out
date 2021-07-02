using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	

	public bool isDead;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Throwable"))
        {
            GetComponent<MeshRenderer>().material.color = Color.gray;
            isDead = true;
        }
    }
}
