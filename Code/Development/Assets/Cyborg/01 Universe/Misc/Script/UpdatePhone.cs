using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UpdatePhone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TMP_InputField input;
    [SerializeField]
    TextMeshProUGUI activeContact;
    private void OnEnable()
    {
        activeContact.text = "Active: " + EventHandlerScript.staticContactNo;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveContact()
    {
        EventHandlerScript.staticContactNo = input.text;
        activeContact.text = "Active: " + EventHandlerScript.staticContactNo;
        AlertBox_General.ShowAlertBox("Contact Number Updated");
    }
}
