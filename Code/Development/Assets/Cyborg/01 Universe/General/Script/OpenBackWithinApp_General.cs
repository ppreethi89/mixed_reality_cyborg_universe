using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OpenBackWithinApp_General : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject parentObjToActivate;
    [SerializeField]
    GameObject parentObjToHide;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openApp()
    {
        parentObjToActivate.SetActive(true);
    }
    public void backApp()
    {
        parentObjToHide.SetActive(false);
    }

}
