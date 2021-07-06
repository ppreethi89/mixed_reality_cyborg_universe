using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeKeyboard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Graphic graphic;
    private Color color;
    private Color32 startingColor;
    public Material materialSelected;
    public Material materialWhileLooking;

    private void Start()
    {
        if (graphic == null)
        {
            graphic = GetComponent<Graphic>();
            //color = graphic.color;
        }
        //graphic.color = new Color32(132,171,188,255);



    }
    public void Awake()
    {
        startingColor = gameObject.GetComponent<Image>().color;
    }

    /// <summary>
    /// Sets a random color on the renderer's material.
    /// </summary>
    public void ChangeColor()
    {

        graphic.color = Color.blue;
    }

    public void OriginalColor()
    {

        //graphic.color = Color.white;
        graphic.color = startingColor;
    }

    public void ChangeMaterial()
    {
        gameObject.GetComponent<Renderer>().material = materialSelected;
        gameObject.GetComponent<Renderer>().material = materialWhileLooking;
    }
}
