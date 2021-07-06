using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySelectedImage : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public void borderEnable()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
    public void borderDisable() {

        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public void selectImage()
    {

        string gallerySelected = ArtistInfo.GallerySelected;

        if (gallerySelected == "Style")
        {

            GameObject styleimage = GameObject.Find("StyleFinalImage");
            if (!styleimage.GetComponent<DisplayFinalImages>().checkContainer())
            {
                styleimage.GetComponent<RawImage>().enabled = true;
                styleimage.GetComponent<RawImage>().texture = gameObject.GetComponent<RawImage>().texture;
                styleimage.GetComponent<RawImage>().texture.name = gameObject.name;
                styleimage.GetComponent<DisplayFinalImages>().fillImageContainer();
            }
        }

        else if (gallerySelected == "Color")
        {

            GameObject colorimage = GameObject.Find("ColorFinalImage");
            if (!colorimage.GetComponent<DisplayFinalImages>().checkContainer())
            {
                colorimage.GetComponent<RawImage>().enabled = true;
                colorimage.GetComponent<RawImage>().texture = gameObject.GetComponent<RawImage>().texture;
                colorimage.GetComponent<RawImage>().texture.name = gameObject.name;
                colorimage.GetComponent<DisplayFinalImages>().fillImageContainer();
            }
        }
        else if (gallerySelected == "CropPeople") {

            GameObject cropimage = GameObject.Find("CropFinalImage");
            if (!cropimage.GetComponent<DisplayFinalImages>().checkContainer())
            {
                cropimage.GetComponent<RawImage>().enabled = true;
                cropimage.GetComponent<RawImage>().texture = gameObject.GetComponent<RawImage>().texture;
                cropimage.GetComponent<RawImage>().texture.name = gameObject.name;
                cropimage.GetComponent<DisplayFinalImages>().fillImageContainer();
            }
        }
    }

}
