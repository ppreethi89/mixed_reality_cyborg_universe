using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class SetActiveInputField_General: MonoBehaviour
{
    
    private TMP_InputField tmpInputfield;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Attached this function to Eyetracking Target Dwell so that whenever input field is gazed, talk app will be called.
    public void callTalkApp()
    {
        tmpInputfield = this.gameObject.GetComponent<TMP_InputField>();
        CyborgTalkApp.callTalkApp(tmpInputfield);
    }
}
