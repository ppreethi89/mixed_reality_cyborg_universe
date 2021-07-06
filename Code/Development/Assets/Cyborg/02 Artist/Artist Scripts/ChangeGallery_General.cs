using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGallery_General : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject viewImagesButton;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeGallerySelected() {

     viewImagesButton.SetActive(true);
     gameObject.GetComponent<ArtistSpriteRendererChange>().SelectedSprite();
     gameObject.GetComponent<BoxCollider>().enabled = false;
     viewImagesButton.GetComponent<ChangeViewImages_General>().DisplayCollection();

    }

    public void nameOfGallerySelected() {

        ArtistInfo.GallerySelected = gameObject.name;
    }

}
