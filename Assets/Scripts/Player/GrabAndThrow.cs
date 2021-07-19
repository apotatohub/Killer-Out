using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabAndThrow : MonoBehaviour
{
    public Rigidbody shieldRigidbody;
    public float force;
    public GameObject shieldBody;
    public Transform shieldParent;
    public Slider shieldHealthBar;
    public Animator anim;

    private void Start()
    {
        shieldHealthBar.transform.parent.gameObject.SetActive(false);
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
                anim.SetLayerWeight(1, 1);
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
            if (!shieldBody)
            {
                return;
            }
            anim.SetLayerWeight(1, 0);
            shieldBody.GetComponent<Shield>().isThrown = true;
            shieldHealthBar.transform.parent.gameObject.SetActive(false);
            shieldRigidbody = shieldBody.AddComponent<Rigidbody>();
            shieldRigidbody.AddForce(transform.forward*force,ForceMode.Force);
            shieldBody.transform.parent = shieldParent;
            shieldBody = null;
        }
    }
}
