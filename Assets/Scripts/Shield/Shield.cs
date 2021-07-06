using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool isGrabed;
    public bool isThrown;
    public bool isDamaged;

    float waitTime = 3;
    private void Update()
    {
        if (isThrown && !isDamaged)
        {
            if (waitTime<=0)
            {
                isDamaged = true;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    public void RemoveParent()
    {
        transform.SetParent(null);
    }
}
