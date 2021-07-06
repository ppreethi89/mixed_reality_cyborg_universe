using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.IO;
using System;

public class OnOffRemoveMode : MonoBehaviour
{
    public bool removeModeActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateRemoveMode()
    {
        if (removeModeActivated == false)
        {
            removeModeActivated = true;
            Debug.Log("Remove Mode Activated");
        }
        else
        {
            removeModeActivated = false;
            Debug.Log("Add Mode Activated");
        }
    }
}
