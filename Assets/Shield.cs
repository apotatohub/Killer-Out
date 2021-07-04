using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool isGrabed;
    public bool isThrown;
    public void RemoveParent()
    {
        transform.SetParent(null);
    }
}
