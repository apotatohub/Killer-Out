using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 direction;
    [SerializeField] float bulletForce=1000;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * bulletForce * 10000 * Time.fixedDeltaTime, ForceMode.Force);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().PlayerDamage(1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            if (other.GetComponent<Shield>().isGrabed)
            {
                other.GetComponent<ShieldHealth>().ShieldDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
