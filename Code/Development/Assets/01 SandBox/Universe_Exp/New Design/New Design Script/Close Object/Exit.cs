using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject controlPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void exitApp()
    {
        if(EventHandlerScript.webViewPrefab != null)
        {
            EventHandlerScript.webViewPrefab = null;
        }
        Destroy(controlPanel.transform.parent.gameObject);
    }
    
}
