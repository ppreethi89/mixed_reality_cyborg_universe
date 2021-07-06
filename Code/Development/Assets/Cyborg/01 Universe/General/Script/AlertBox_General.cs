using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class AlertBox_General : MonoBehaviour
{
    [SerializeField]
    GameObject nonStaticPrefab;
    public static GameObject alertPrefab;


    private void Awake()
    {
        //this helps convert serializable/non-static variable to a static one.
        alertPrefab = nonStaticPrefab;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This static function is used when an information/alert box is need to pop up.
    //In contrast with the ConfirmationBox, this function only requires a string parameter for message, no action parameter is needed.
    public static void ShowAlertBox(string message)
    {
        alertPrefab.transform.GetChild(0).GetComponent<TextMeshPro>().text = message;
        alertPrefab.SetActive(true);
       
        

    }
    public void OkConfirm()
    {
        alertPrefab.SetActive(false);
        //EventHandlerScript.activeObj = null;
    }
}
