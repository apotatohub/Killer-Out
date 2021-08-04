using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] Vector3 initialPosition;
    [SerializeField] Quaternion initialRotation;
    public bool isGrabed;
    public bool isThrown;
    public bool isDamaged;

    float waitTimeToDamage = 1.2f;
    float waitTimeToRespawn = 5;
    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    private void Update()
    {
        if (isThrown)
        {
            //Respawn
            if (waitTimeToRespawn<=0)
            {
                Respawn();
            }
            else
            {
                waitTimeToRespawn -= Time.deltaTime;
            }

            if (!isDamaged)
            {
                //damage
                if (waitTimeToDamage <= 0)
                {
                    isDamaged = true;
                }
                else
                {
                    waitTimeToDamage -= Time.deltaTime;
                }
            }
         
        }

    }
    public void RemoveParent()
    {
        transform.SetParent(null);
    }

    public void Respawn()
    {
       
        Destroy(gameObject.GetComponent<Rigidbody>());
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        isGrabed = isThrown = isDamaged = false;
        waitTimeToRespawn = 5;
        waitTimeToDamage = 1.2f;
    }
}
