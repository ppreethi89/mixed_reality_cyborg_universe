using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialChangeKeyboard : MonoBehaviour
{
    private Material materialStart;
    public Material materialOriginal;
    public Material materialWhileLooking;
    public Material materialSelected;

    public bool ondwell;

    // Start is called before the first frame update
    void Start()
    {
        //materialStart
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        //materialStart = gameObject.GetComponent<Image>().material;
        materialStart = gameObject.GetComponent<Renderer>().material;
    }

    public void WhileLookingMaterial()
    {
        if (ondwell == false)
        {
            //gameObject.GetComponent<Image>().material = materialWhileLooking;
            gameObject.GetComponent<Renderer>().material = materialWhileLooking;
        }
       
    }

    public void SelectedMaterial()
    {
        ondwell = true;
        //gameObject.GetComponent<Image>().material = materialSelected;
        gameObject.GetComponent<Renderer>().material = materialWhileLooking;
    }

    public void OriginalMaterial()
    {
        ondwell = false;
        //gameObject.GetComponent<Image>().material = materialOriginal;
        gameObject.GetComponent<Renderer>().material = materialWhileLooking;
    }

}
