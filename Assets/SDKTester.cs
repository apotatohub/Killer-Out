using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDKTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CallEvent());
        
    }

    IEnumerator CallEvent()
    {
        yield return new WaitForSeconds(0.5f);
        AdjustSdkManager_manual.Instance.CallEvent("test_event_delete");
    }
}
