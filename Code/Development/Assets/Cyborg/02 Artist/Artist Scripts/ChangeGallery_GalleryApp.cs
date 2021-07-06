using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGallery_GalleryApp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject AzureStorageServices;
    [SerializeField]
    private string azureContainer;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeGallerySelected() {

            AzureStorageServices.GetComponent<AzureImages_General>().azureContainer = azureContainer;
            AzureStorageServices.GetComponent<AzureImages_General>().currentPageNum = 1;
            AzureStorageServices.GetComponent<AzureImages_General>().count = 0;
            AzureStorageServices.GetComponent<AzureImages_General>().ListBlobs();
    }

}
