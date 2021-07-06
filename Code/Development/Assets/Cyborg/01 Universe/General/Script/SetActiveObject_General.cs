using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetActiveObject_General : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject parentObjToActivate;
    [SerializeField]
    GameObject parentObjToHide;

    private bool set;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void transferApp()
    {
        parentObjToActivate.SetActive(true);
        parentObjToHide.SetActive(false);
    }
    public void closeApp()
    {
        parentObjToHide.SetActive(false);
    }
    public void openApp()
    {
        parentObjToActivate.SetActive(false);
    }
    public void openObj()
    {
        parentObjToActivate.SetActive(true);

    }
    public void OpenCloseObject()
    {
        set = !set;
        if (set)
        {
            parentObjToActivate.SetActive(true);
        }
        else
        {
            parentObjToHide.SetActive(false);
        }
    }
    public void setActive()
    {
        set = !set;
        if (set)
        {
            parentObjToHide.SetActive(false);
            parentObjToActivate.SetActive(true);
            
            if (parentObjToActivate.transform.tag == "Built-in" || parentObjToActivate.transform.tag == "Web-App")
            {
                EventHandlerScript.activeObj = parentObjToActivate.transform.name;
            }
            
                
        }
        else
        {
            parentObjToActivate.SetActive(false);
            parentObjToHide.SetActive(true);
            EventHandlerScript.activeObj = null;
        }
    }

    public void setActiveTransition()
    {
        parentObjToActivate.SetActive(true);
        parentObjToHide.SetActive(false);
    }

    public void triggerTalkAppControl()
    {
        TriggerTalkAppControls.activateControl(parentObjToActivate);
    }
    public void closeControlTalkApp()
    {
        
        TriggerTalkAppControls.staticAlphaKeyboard.SetActive(true);
        parentObjToActivate.SetActive(false);
        Debug.Log(parentObjToActivate.name);
    }

}
