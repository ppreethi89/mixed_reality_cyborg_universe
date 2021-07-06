using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpriteRendererChange_General : MonoBehaviour
{
    private Sprite spriteStart;
    public Sprite spriteWhileLooking;
    public Sprite spriteSelected;

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
        spriteStart = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void WhileLookingMaterial()
    {
        if (ondwell == false)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteWhileLooking;
        }

    }

    public void SelectedMaterial()
    {
        ondwell = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteSelected;
        changeImage();
    }

    public void OriginalMaterial()
    {
        ondwell = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteStart;
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
