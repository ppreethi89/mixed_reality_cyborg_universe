using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RendererMatChange : MonoBehaviour
{
    private Material materialStart;
    public Material materialWhileLooking;
    public Material materialSelected;

    private bool ondwell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        materialStart = gameObject.GetComponent<Renderer>().material;
    }

    public void WhileLookingMaterial()
    {
        if (ondwell == false)
        {
            gameObject.GetComponent<Renderer>().material = materialWhileLooking;
        }

    }

    public void SelectedMaterial()
    {
        ondwell = true;
        gameObject.GetComponent<Renderer>().material = materialSelected;
    }

    public void OriginalMaterial()
    {
        ondwell = false;
        gameObject.GetComponent<Renderer>().material = materialStart;
    }
    public void ToggleMat()
    {
        ondwell = !ondwell;
        if (ondwell)
        {
            gameObject.GetComponent<Renderer>().material = materialSelected;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = materialStart;
        }
    }
}
