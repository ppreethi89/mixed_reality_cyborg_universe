using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeViewImages_General : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject AzureGridImages;
    [SerializeField]
    public GameObject AzureSingleImages;
    private GameObject AzureStorage;
    public GameObject collectionOfImages;
    public GameObject singleImage;
    [SerializeField]
    private string container;
    private bool isSingleImageOn;
    [SerializeField]
    private GameObject gallery; 



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ViewImagesInActive() {

        gameObject.SetActive(false);
        gallery.GetComponent<BoxCollider>().enabled = true;

    }

   

    public void DisplaySingleImageCollection() {

        isSingleImageOn = true;
        DisplayCollection();
    }

    public void DisplayGridImageCollection() {

        isSingleImageOn = false;
        DisplayCollection();
    
    }


    public void DisplayCollection()
    {
        if (isSingleImageOn)
        {
            collectionOfImages.SetActive(false);
            AzureStorage = AzureSingleImages;
            changeViewImages();
            StartCoroutine(destroyChildInGallery(singleImage));
            singleImage.SetActive(true);
        }
        else
        {
            singleImage.SetActive(false);
            AzureStorage = AzureGridImages;
            changeViewImages();
            StartCoroutine(destroyChildInGallery(collectionOfImages));
            collectionOfImages.SetActive(true);
        }


    }

    IEnumerator destroyChildInGallery(GameObject imagesContainer)
    {
        foreach (Transform child in imagesContainer.transform)
        {
            Destroy(child.gameObject);
        }
        yield return true;
    }

    public void changeViewImages()
    {

            AzureStorage.GetComponent<AzureImages_General>().azureContainer = container;
            AzureStorage.GetComponent<AzureImages_General>().currentPageNum = 1;
            AzureStorage.GetComponent<AzureImages_General>().count = 0;
            AzureStorage.GetComponent<AzureImages_General>().ListBlobs();

    }
   
        

}
