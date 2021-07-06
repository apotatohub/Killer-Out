using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabAndThrow : MonoBehaviour
{
    PlayerMovement playerMovement;
    SwipeInput swipeInput;
    public Rigidbody shieldRigidbody;
    public float force;
    public GameObject shieldBody;
    public Transform shieldParent;
    public Slider shieldHealthBar;

    private void Start()
    {
        shieldHealthBar.transform.parent.gameObject.SetActive(false);
        playerMovement = GetComponent<PlayerMovement>();
        swipeInput = GetComponent<SwipeInput>();
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Shield"))
        {
            if (shieldBody)
            {
                return;
            }
            GameObject sBody = collision.collider.gameObject;

            if (!sBody.GetComponent<Shield>().isGrabed)
            {
                shieldHealthBar.transform.parent.gameObject.SetActive(true);
                shieldBody = sBody;
                shieldBody.GetComponent<ShieldHealth>().OnPickedShield(shieldHealthBar);               
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
            shieldHealthBar.transform.parent.gameObject.SetActive(false);
            shieldBody.GetComponent<Shield>().isThrown = true;
            shieldRigidbody = shieldBody.AddComponent<Rigidbody>();
            shieldRigidbody.AddForce(transform.forward*force,ForceMode.Force);
            shieldBody.transform.parent = shieldParent;
            shieldBody = null;
        }
    }
}
