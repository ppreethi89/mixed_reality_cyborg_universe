using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBackFunctionality : MonoBehaviour
{
    // Start is called before the first frame update
    private string selectedTheme;
    [SerializeField]
    private GameObject compositionImageContainer;
    [SerializeField]
    private GameObject selectedThemeContainer;
    [SerializeField]
    private GameObject styleGallery;


    void OnEnable()
    {
        selectedTheme = ArtistInfo.ThemeSelected;
        selectedThemeContainer.GetComponent<TMP_Text>().text = selectedTheme;
        compositionImageContainer.GetComponent<RawImage>().texture = ArtistInfo.CompositionImageSelected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void confirm()
    {
        Texture texture = compositionImageContainer.GetComponent<RawImage>().texture;
        ArtistInfo.CompositionImageSelected = texture;
        if (ArtistInfo.GallerySelected == "Style")
        {
            styleGallery.GetComponent<ArtistSpriteRendererChange>().SelectedSprite();
            styleGallery.GetComponent<BoxCollider>().enabled = false;

        }
        gameObject.GetComponent<SetActiveObject_General>().setActiveTransition();

       
    }

}

