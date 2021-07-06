using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SimpleJSON;
using System.IO;

public class ResizeTalkApp : MonoBehaviour
{
    public GameObject addButton;
    public GameObject miniButton;
    public string filepath = "talkappConfig.json";

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
       ShowStatus.consumeAction(showresize);
    }

    public void showresize()
    {

        if (addButton.activeSelf && miniButton.activeSelf)
        {
            addButton.SetActive(false);
            miniButton.SetActive(false);
            GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = true;
            GameObject.Find("ControlClass").GetComponent<SavePosition>().startSaveScale();
            Debug.Log("I'm here");
        }
        else
        {
            addButton.SetActive(true);
            miniButton.SetActive(true);
            GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = true;
        }

    }

    }

