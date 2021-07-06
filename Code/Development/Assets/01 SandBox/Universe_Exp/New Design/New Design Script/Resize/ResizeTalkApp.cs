using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SimpleJSON;
using System.IO;

public class showpanel : MonoBehaviour
{
    public GameObject addButton;
    public GameObject miniButton;
    //public string filepath = "talkappConfig.json";
    public GameObject talkApp;

    // Start is called before the first frame update
    void Start()
    {
        addButton.SetActive(false);
        miniButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resize()
    {
       addButton.SetActive(true);
       miniButton.SetActive(true);
       
       ShowStatus.showStatus("Resize");
       ShowStatus.consumeAction(SaveResizeTalkApp);
    }

    public void SaveResizeTalkApp()
    {

        if (addButton.activeSelf && miniButton.activeSelf)
        {
            addButton.SetActive(false);
            miniButton.SetActive(false);
            GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = true;
            
            
            StartCoroutine(InitializeSave.SaveTransform(talkApp));
            Debug.Log(talkApp);
        }
        else
        {
            addButton.SetActive(true);
            miniButton.SetActive(true);
            GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = true;
        }

    }

    }

