using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewCompositionImage : MonoBehaviour
{
    [SerializeField]
    private GameObject fullBorderIcon;
    // Start is called before the first frame update
    public void fullBorderOn()
    {

        fullBorderIcon.SetActive(true);

    }

    public void fullBorderOff()
    {
        fullBorderIcon.SetActive(false);

    }

    public void displayImage()
    {
        GameObject selectedCompositionImage = GameObject.Find("Composition_Image");
        GameObject nextPage = GameObject.Find("NextPage");

        if (!selectedCompositionImage.GetComponent<DisplayFinalImages>().checkContainer()) {
            Texture imgtex = gameObject.GetComponent<RawImage>().texture;
            selectedCompositionImage.GetComponent<RawImage>().enabled = true;
            selectedCompositionImage.GetComponent<RawImage>().texture = imgtex;
            selectedCompositionImage.GetComponent<RawImage>().texture.name = gameObject.name;
            selectedCompositionImage.GetComponent<DisplayFinalImages>().fillImageContainer();
            nextPage.GetComponent<ArtistSpriteRendererChange>().ActiveSprite();
            nextPage.GetComponent<BoxCollider>().enabled = true;
        }                     
    }
  
}
