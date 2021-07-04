using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow : MonoBehaviour
{
    PlayerMovement playerMovement;
    SwipeInput swipeInput;
    public Rigidbody shieldRigidbody;
    public float force;
    public GameObject shieldBody;
    public Transform shieldParent;


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        swipeInput = GetComponent<SwipeInput>();
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Throwable"))
        {
            if (shieldBody)
            {
                return;
            }
            GameObject sBody = collision.collider.gameObject;

            if (!sBody.GetComponent<Shield>().isGrabed)
            {
                shieldBody = sBody;
                shieldBody.GetComponent<Shield>().isGrabed = true;
                shieldBody.transform.parent = transform.GetChild(0);
                shieldBody.transform.position = transform.GetChild(0).position;

            }

        }
    }


    private void Update()
    {
    
        if (Input.GetMouseButtonUp(0))
        {
            playerMovement.speed = 0;
            if (!shieldBody)
            {
                return;
            }
            shieldBody.GetComponent<Shield>().isThrown = true;
            shieldRigidbody = shieldBody.AddComponent<Rigidbody>();
            shieldRigidbody.AddForce(transform.forward*force,ForceMode.Force);
            shieldBody.transform.parent = shieldParent;
            shieldBody = null;
        }
    }
}
