using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ShowStatus : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject SerializeStatusPrefab;
    public static Action controlAction;
    public static GameObject statusPrefab;

    void Start()
    {
        //help in converting non-static field to static field - to consume by showStatus()
        statusPrefab = SerializeStatusPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void showStatus(string control)
    {
        statusPrefab.SetActive(true);
        statusPrefab.GetComponentInChildren<TextMeshPro>().text = control;
    }

    //consume passed Action from App
    public static void consumeAction(Action action)
    {
        controlAction = action;
    }

    //run passed action from App, as well as deactivating status bar
    public void removeStatus()
    {
        controlAction();
        statusPrefab.SetActive(false);
        EventHandlerScript.activeObj = null;
    }

}
