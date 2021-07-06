using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ImageMatChange : MonoBehaviour
{
    private Material materialStart;
    public Material materialWhileLooking;
    public Material materialSelected;

    private bool ondwell;


    private bool onSelect;
    public Image imageToChange;
    public Sprite notActiveImage;
    public Sprite activeImage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Awake()
    {
        materialStart = gameObject.GetComponent<Image>().material;
    }

    public void WhileLookingMaterial()
    {
        if (ondwell == false)
        {
            gameObject.GetComponent<Image>().material = materialWhileLooking;
        }

    }

    public void SelectedMaterial()
    {
        ondwell = true;
        gameObject.GetComponent<Image>().material = materialSelected;
        changeImage();
    }

    public void OriginalMaterial()
    {
        ondwell = false;
        gameObject.GetComponent<Image>().material = materialStart;
    }
    void changeImage()
    {
        if (imageToChange != null)
        {
            onSelect = !onSelect;
            if (onSelect)
            {
                imageToChange.sprite = activeImage;
            }
            else
            {
                imageToChange.sprite = notActiveImage;
            }
        }
    }
}
