using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ConfirmationBox_General : MonoBehaviour
{
    public static Action action;
    [SerializeField]
    GameObject prefab;
    public static GameObject confirmationPrefab;
    public static Action action2;
    void Start()
    {
        //this helps convert serializable/non-static variable to a static one.
        confirmationPrefab = prefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This static function is used when a confirmation box (yes,no) is need to pop up.
    //This function needs to have an Action parameter (refers to the function/action need to be taken when YES is dwelled) and a string for message.
    public static void showConfirmation(Action toPassAction, string message, Action additionalAction =null)
    {
        
        confirmationPrefab.transform.GetChild(0).GetComponent<TextMeshPro>().text = message;
        confirmationPrefab.SetActive(true);
        action = toPassAction;
        action2 = additionalAction;


    }
    public void yesConfirm()
    {
        action();
        confirmationPrefab.SetActive(false);
        EventHandlerScript.activeObj = null;
    }
    public void noConfirm()
    {
        action2?.Invoke();

        action2 = null;
        confirmationPrefab.SetActive(false);
        EventHandlerScript.activeObj = null;
    }


}
