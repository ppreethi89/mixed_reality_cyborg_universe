using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GalleryScroll_General : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject AzureGridImages;
    [SerializeField]
    private GameObject AzureSingleImages;
    [SerializeField]
    private GameObject collectionImages;
    private GameObject AzureStorage;
    [SerializeField]
    private TextMeshPro paginationLabel;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextPage()
    {

        if (collectionImages.activeSelf)
        {

            AzureStorage = AzureGridImages;

        }
        else {

            AzureStorage = AzureSingleImages; 
        }

        int currentPageNum = AzureStorage.GetComponent<AzureImages_General>().currentPageNum;
        float maxNumPages = AzureStorage.GetComponent<AzureImages_General>().maxNumPages;
        int numImgPage = AzureStorage.GetComponent<AzureImages_General>().numImgPage;

        Debug.Log("Max Num Pages " + maxNumPages);

        if (currentPageNum < maxNumPages)
        {
            currentPageNum++;
            Debug.Log("Next Page");
            AzureStorage.GetComponent<AzureImages_General>().currentPageNum = currentPageNum;
            AzureStorage.GetComponent<AzureImages_General>().count = (currentPageNum - 1) * numImgPage;
            AzureStorage.GetComponent<AzureImages_General>().DisplayImages();
            paginationLabel.SetText("{0} of {1}", currentPageNum, maxNumPages);

        }
    }
    public void previousPage()
    {
        if (collectionImages.activeSelf)
        {

            AzureStorage = AzureGridImages;

        }
        else
        {

            AzureStorage = AzureSingleImages;
        }

        int currentPageNum = AzureStorage.GetComponent<AzureImages_General>().currentPageNum;
        int numImgPage = AzureStorage.GetComponent<AzureImages_General>().numImgPage;
        float maxNumPages = AzureStorage.GetComponent<AzureImages_General>().maxNumPages;

        Debug.Log("Max Num Pages " + maxNumPages);

        if (currentPageNum > 1)
        {
            Debug.Log("Previous Page");
            currentPageNum--;
            AzureStorage.GetComponent<AzureImages_General>().currentPageNum = currentPageNum;
            AzureStorage.GetComponent<AzureImages_General>().count = (currentPageNum - 1) * numImgPage;
            AzureStorage.GetComponent<AzureImages_General>().DisplayImages();
            paginationLabel.SetText("{0} of {1}", currentPageNum, maxNumPages);

        }
    }
}
