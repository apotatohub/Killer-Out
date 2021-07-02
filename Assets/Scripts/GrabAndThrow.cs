using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow : MonoBehaviour
{
    public SwipeInput swipeInput;
    public Rigidbody shieldRigidbody;
    public float force;
    GameObject shieldBody;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Throwable"))
        {
            shieldBody = collision.collider.gameObject; 
            shieldBody.transform.parent = transform.GetChild(0);
            shieldBody.transform.position = transform.GetChild(0).position;
          
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (shieldBody)
            {
                shieldBody.transform.parent = null;
                shieldRigidbody = shieldBody.AddComponent<Rigidbody>();
                shieldRigidbody.isKinematic = false;
                Vector3 _forceDir = new Vector3(swipeInput.Direction.x, 0, swipeInput.Direction.y);
                shieldRigidbody.AddForce(_forceDir*force);
            }

        }
    }
}
