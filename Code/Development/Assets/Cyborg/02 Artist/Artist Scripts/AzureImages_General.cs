using Azure.StorageServices;
using Microsoft.MixedReality.Toolkit.Utilities;
using RESTClient;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AzureImages_General : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Azure Storage Service")]
    public string azureContainer;

    private StorageServiceClient client;
    private BlobService blobService;

    [Header("Page Control")]
    public int count = 0;
    public int currentPageNum = 1;
    public float maxNumPages;
    [SerializeField]
    private TextMeshPro paginationLabel;


    [Header("Display Images")]
    [SerializeField]
    private GridObjectCollection galleryImagesContainer;
    [SerializeField]
    private GameObject imgContainer;
    public int numImgPage; 
    private List<Blob> galleryImagesName;



    void Start()
    {

            client = StorageServiceClient.Create(ArtistInfo.AzureStorageAccount, ArtistInfo.AzureAccessKey);
            blobService = client.GetBlobService();
            galleryImagesName = new List<Blob>();
            ListBlobs();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ListBlobs()
    {

        if (!string.IsNullOrEmpty(azureContainer))
        {

            StartCoroutine(blobService.ListBlobs(ListBlobsCompleted, azureContainer));

        }


    }

    private void ListBlobsCompleted(IRestResponse<BlobResults> response)
    {
        if (response.IsError)
        {
            Debug.Log("Failed to get list of blobs List blob error: " + response.ErrorMessage);
            return;
        }
        else
        {

            galleryImagesName.Clear();
            galleryImagesName.AddRange(response.Data.Blobs);
            maxNumPages = Mathf.Ceil(response.Data.Blobs.Length / (float)numImgPage);
            if (maxNumPages == 0)
            {
                AlertBox_General.ShowAlertBox("There are currently no images in this Gallery");
            }
            else{

                if(galleryImagesContainer.gameObject.activeSelf){
                    paginationLabel.SetText("{0} of {1}", currentPageNum, maxNumPages);
                }
                DisplayImages();
            }
        }
    }

    public void DisplayImages()
    {
        StartCoroutine(destroyChildInGallery());

        for (var i = count; i < currentPageNum * numImgPage; i++)
        {
            GameObject singleImageContainer = (GameObject)Instantiate(imgContainer);
            try
            {
                string imagePath = azureContainer + "/" + galleryImagesName[i].Name;
                singleImageContainer.transform.SetParent(galleryImagesContainer.transform, false);
                singleImageContainer.name = galleryImagesName[i].Name;
                StartCoroutine(blobService.GetImageBlob(GetImageBlobComplete, imagePath));

            }
            catch
            {
                Destroy(singleImageContainer);
                break;
            }


            void GetImageBlobComplete(IRestResponse<Texture> response)
            {
                if (response.IsError)
                {
                    Debug.Log(response.ErrorMessage);
                    Debug.Log(response.StatusCode);
                }
                else
                {
                    Texture texture = response.Data;
                    LoadImage(texture as Texture2D);
                }
            }



            void LoadImage(Texture2D texture)
            {
                //GameObject singleImageContainer = (GameObject)Instantiate(imgContainer);
                //singleImageContainer.transform.SetParent(galleryImagesContainer.transform, false);
                singleImageContainer.GetComponent<RawImage>().texture = texture;
                galleryImagesContainer.UpdateCollection();
            }


        }


    }

    IEnumerator destroyChildInGallery()
    {
        foreach (Transform child in galleryImagesContainer.transform)
        {
            Destroy(child.gameObject);
        }
        yield return true;
    }
}
