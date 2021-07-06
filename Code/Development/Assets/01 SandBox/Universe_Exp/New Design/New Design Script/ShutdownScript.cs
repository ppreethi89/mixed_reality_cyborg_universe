using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShutdownScript : MonoBehaviour
{
    public GameObject ConfirmationPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Yes()
    {
       
        Application.Quit();
        Debug.Log("Application Quit");
       
        //Application.Quit();
    }

    public void No()
    {
        Destroy(transform.parent.gameObject);
    }

    public void confirm()
    {
        //GameObject prefab = (GameObject)Instantiate(ConfirmationPanel);
        //prefab.GetComponentInChildren<TextMeshPro>().text = "Are you sure you want to exit the application?";
        string message = "Are you sure you want to exit the application?";
        ConfirmationBox_General.showConfirmation(Yes, message);
    }
}
