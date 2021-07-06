using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryScroll_GalleryApp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject AzureStorageServices;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextPage()
    {
        int currentPageNum = AzureStorageServices.GetComponent<AzureImages_General>().currentPageNum;
        float maxNumPages = AzureStorageServices.GetComponent<AzureImages_General>().maxNumPages;
        int numImgPage = AzureStorageServices.GetComponent<AzureImages_General>().numImgPage;

        Debug.Log("Max Num Pages " + maxNumPages);

        if (currentPageNum < maxNumPages)
        {
            currentPageNum++;
            Debug.Log("Next Page");
            AzureStorageServices.GetComponent<AzureImages_General>().currentPageNum = currentPageNum;
            AzureStorageServices.GetComponent<AzureImages_General>().count = (currentPageNum - 1) * numImgPage;
            AzureStorageServices.GetComponent<AzureImages_General>().DisplayImages();
        }
    }
    public void previousPage()
    {
        int currentPageNum = AzureStorageServices.GetComponent<AzureImages_General>().currentPageNum;
        int numImgPage = AzureStorageServices.GetComponent<AzureImages_General>().numImgPage;

        if (currentPageNum > 1)
        {
            Debug.Log("Previous Page");
            currentPageNum--;
            AzureStorageServices.GetComponent<AzureImages_General>().currentPageNum = currentPageNum;
            AzureStorageServices.GetComponent<AzureImages_General>().count = (currentPageNum - 1) * numImgPage;
            AzureStorageServices.GetComponent<AzureImages_General>().DisplayImages();

        }
    }
}
