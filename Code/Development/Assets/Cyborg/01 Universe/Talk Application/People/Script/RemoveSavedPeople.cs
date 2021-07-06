using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class RemoveSavedPeople : MonoBehaviour
{
    [SerializeField]
    Sprite delete;
    [SerializeField]
    Sprite plus;
    [SerializeField]
    GameObject familyPrefab;
    [SerializeField]
    GameObject paPrefab;
    [SerializeField]
    GameObject personNamePrefab;
    // Start is called before the first frame update
    bool val;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOffRemoveMode()
    {
        val = !val;
        //EventHandlerScript.removeModeActive = !EventHandlerScript.removeModeActive;
        EventHandlerScript.removeModeActive = val;
        Debug.Log(EventHandlerScript.removeModeActive + "Yow");
        /*if (EventHandlerScript.removeModeActive == false)
        {
            
            gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Remove Mode";
        }
        else
        {
                    
            gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Play Mode";
        }*/
    }

}
