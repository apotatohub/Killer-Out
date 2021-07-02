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
        if (collision.collider.CompareTag("Throwable") && !shieldBody)
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
                Vector3 _forceDir = new Vector3(swipeInput.Direction.normalized.x, 0, swipeInput.Direction.normalized.y);
                shieldRigidbody = shieldBody.AddComponent<Rigidbody>();
                shieldRigidbody.AddForce(_forceDir*force);
                shieldBody.transform.parent = null;
            }

        }
    }
}
