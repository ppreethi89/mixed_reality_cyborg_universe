using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherOptionsSelection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject finalCompositonImageConatiner;
    void OnEnable()
    {
        finalCompositonImageConatiner.GetComponent<RawImage>().texture = ArtistInfo.CompositionImageSelected;
        if (ArtistInfo.PhotomosaicSelected)
        {
            gameObject.GetComponent<ArtistSpriteRendererChange>().SelectedSprite();
        }
        else {

            gameObject.GetComponent<ArtistSpriteRendererChange>().OriginalSprite();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectPhotomosaic() {

        

        if (ArtistInfo.PhotomosaicSelected)
        {

            gameObject.GetComponent<ArtistSpriteRendererChange>().OriginalSprite();
            ArtistInfo.PhotomosaicSelected = false;

        }
        else {
            gameObject.GetComponent<ArtistSpriteRendererChange>().SelectedSprite();
            ArtistInfo.PhotomosaicSelected = true;
        }
    }
}
