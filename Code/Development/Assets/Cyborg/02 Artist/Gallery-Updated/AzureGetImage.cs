using Azure.StorageServices;
using Microsoft.MixedReality.Toolkit.Utilities;
using RESTClient;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AzureGetImage : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Azure Storage Service")]
    public string azureContainer;

    private StorageServiceClient client;
    private BlobService blobService;

    [Header("Display Images")]
    //[SerializeField]
    //private GridObjectCollection galleryImagesContainer;
    [SerializeField]
    private GameObject imgContainer;
    private List<Blob> galleryImagesName;
    [SerializeField]
    private GameObject imgContent;

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
            DisplayImages(21);

        }
    }

    public void DisplayImages(int totalImg)
    {

        StartCoroutine(destroyChildInGallery());

        for (var i = 0; i < totalImg; i++)
        {
            GameObject singleImageContainer = (GameObject)Instantiate(imgContainer);
            singleImageContainer.transform.SetParent(imgContent.transform, false);

            try
            {
                string imagePath = azureContainer + "/" + galleryImagesName[i].Name;
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
                singleImageContainer.GetComponent<RawImage>().texture = texture;
            }
        }
    }

    IEnumerator destroyChildInGallery()
    {
        foreach (Transform child in imgContent.transform)
        {
            Destroy(child.gameObject);
        }
        yield return true;
    }

}
