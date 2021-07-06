using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ChangeMaterialOnActive_Design : MonoBehaviour
{
    [SerializeField]
    Material materialActiveMode;
    [SerializeField]
    Material materialOriginalMode;
    static public ChangeMaterialOnActive_Design instance;
    [SerializeField]
    string activeLabel;
    [SerializeField]
    string originalLabel;
    [SerializeField]
    TextMeshPro txt;

    bool mode;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        instance.activeLabel = this.activeLabel;
        instance.originalLabel = this.originalLabel;
        instance.mode = this.mode;
        instance.materialOriginalMode = this.materialOriginalMode;
        instance.materialActiveMode = this.materialActiveMode;
        instance.txt = this.txt;

    }
    void Start()
    {

    }
    

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMaterial()
    {
        if (EventHandlerScript.removeModeActive == false)
        {
            Debug.Log(gameObject.transform.name);
            gameObject.GetComponent<Renderer>().material= materialOriginalMode;
            txt.text = originalLabel;


        }
        else
        {
            gameObject.GetComponent<Renderer>().material = materialActiveMode;
            //instance.gameObject.transform.GetComponentInChildren<TextMeshPro>().text;
            txt.text = activeLabel;

        }
        
    }
    public void ChangeMatOnOff()
    {
        instance.mode = !instance.mode;
        if (!instance.mode)
        {
            instance.gameObject.GetComponent<Renderer>().material = instance.materialOriginalMode;
            instance.txt.text = instance.originalLabel;
        }
        else
        {
            instance.gameObject.GetComponent<Renderer>().material = instance.materialActiveMode;
            //instance.gameObject.transform.GetComponentInChildren<TextMeshPro>().text;
            instance.txt.text = instance.activeLabel;
        }
    }
}
