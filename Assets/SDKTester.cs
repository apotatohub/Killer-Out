using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDKTester : MonoBehaviour
{
    // Start is called before the first frame update
    public string eventName;
    void Start()
    {

        StartCoroutine(CallEvent());
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator CallEvent()
    {
        yield return new WaitForSeconds(0.5f);
        AdjustSdkManager_manual.Instance.CallEvent(eventName);
    }
}
