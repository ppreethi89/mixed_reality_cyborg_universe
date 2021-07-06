using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ShowMoveViewAppPrefab : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject SerializeMoveViewPrefab;
    public static Action controlAction;
    public static GameObject staticMoveViewPrefab;
    public static float view;
    static string app;

    
    void Start()
    {
        staticMoveViewPrefab = SerializeMoveViewPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void showMoveViewPrefab(string appName, Action moveAction)
    {
        app = appName;
        controlAction = moveAction;
        string message = "Where do you want to move the ";
        staticMoveViewPrefab.SetActive(true);
        staticMoveViewPrefab.GetComponentInChildren<TextMeshPro>().text = message + appName + " ?";
    }
    public void moveView(float viewnumber)
    {
        float panelview = viewnumber + 1;
        string message = "Are you sure you want to move " + app + " to view " + panelview + " ?";
        view = viewnumber;
        ConfirmationBox_General.showConfirmation(controlAction, message);
        
    }
}
