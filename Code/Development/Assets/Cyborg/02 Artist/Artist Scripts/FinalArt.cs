using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RESTClient;
using Azure.StorageServices;
using UnityEngine.UI;
using System;

public class FinalArt : MonoBehaviour
{
    private string azureContainer = "temporary-art";
    private string thumbnailArt = "art_temp_thumbnail.jpg";
    private string toenailArt = "art_temp.jpg";
    private StorageServiceClient client;
    private BlobService blobService;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject finalArtContainer;
    [SerializeField]
    private GameObject compositionImageContainer;
    [SerializeField]
    private GameObject styleImageContainer;
    [SerializeField]
    private GameObject colorImageContainer;
    [SerializeField]
    private GameObject cropImageContainer;
    [SerializeField]
    private GameObject loadingContainer;
    void Awake()
    {
        client = StorageServiceClient.Create(ArtistInfo.AzureStorageAccount, ArtistInfo.AzureAccessKey);
        blobService = client.GetBlobService();
    }

    // Update is called once per frame
    void OnEnable()
    {
        compositionImageContainer.GetComponent<RawImage>().texture = ArtistInfo.CompositionImageSelected;
        //styleImageContainer.GetComponent<RawImage>().texture = ArtistInfo.StyleImageSelected;
        //colorImageContainer.GetComponent<RawImage>().texture = ArtistInfo.ColorImageSelected;
        //cropImageContainer.GetComponent<RawImage>().texture = ArtistInfo.CropImageSelected;
        imageSelected(ArtistInfo.StyleImageSelected, styleImageContainer);
        imageSelected(ArtistInfo.ColorImageSelected, colorImageContainer);
        imageSelected(ArtistInfo.CropImageSelected, cropImageContainer);

    }

    private void imageSelected(Texture imageSelected, GameObject container) {

        if (imageSelected != null) {

            container.GetComponent<RawImage>().enabled = true;
            container.GetComponent<RawImage>().texture = imageSelected;

        }
         
    }

    public void DiscardImage()
    {
        ConfirmationBox_General.showConfirmation(delete, "The art created will be lost if you don't save it. Do you wish to continue?");
    }

    public void SaveImage() {

        ConfirmationBox_General.showConfirmation(save, "Are you sure you want to save the art created?");    
    }

    private void save() {

        string filename = string.Format("{0}.jpg", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
        byte[] imageBytesThumbnail = ArtistInfo.ThumbnailArtImage.EncodeToJPG();
        byte[] imageBytesToenail = ArtistInfo.ToenailArtImage.EncodeToJPG();
        StartCoroutine(blobService.PutImageBlob(PutImageCompleted, imageBytesThumbnail, "art-thumbnail", filename, "image/jpg"));
        StartCoroutine(blobService.PutImageBlob(PutImageCompleted, imageBytesToenail, "art-toenail", filename, "image/jpg"));
        AlertBox_General.ShowAlertBox("The art created was successfully saved as: " + filename);
        DeleteBlobs();
        gameObject.GetComponent<BackToUniverse>().initializeBackUniverse();
        gameObject.GetComponent<SetActiveObject_General>().setActiveTransition();
        resetPage();
    }

    private void delete()
    {

        AlertBox_General.ShowAlertBox("The art created was deleted");
        DeleteBlobs();
        gameObject.GetComponent<BackToUniverse>().initializeBackUniverse();
        gameObject.GetComponent<SetActiveObject_General>().setActiveTransition();
        resetPage();

    }

    //Reset the Page after saving or discarding the art and going back to Universe. 
    private void resetPage() {

        ArtistInfo.CompositionImageSelected = null;
        ArtistInfo.StyleImageSelected = null;
        ArtistInfo.ColorImageSelected = null;
        ArtistInfo.CropImageSelected = null;
        ArtistInfo.ThumbnailArtImage = null;
        ArtistInfo.ToenailArtImage = null; 
        finalArtContainer.GetComponent<RawImage>().texture = null;
        styleImageContainer.GetComponent<RawImage>().enabled = false;
        colorImageContainer.GetComponent<RawImage>().enabled = false;
        cropImageContainer.GetComponent<RawImage>().enabled = false;

        finalArtContainer.SetActive(false);
        loadingContainer.SetActive(true);
    }

    private void PutImageCompleted(RestResponse response)
    {
        if (response.IsError)
        {
            Debug.Log(response.ErrorMessage + "Error putting blob image:" + response.Content);
            return;
        }
        Debug.Log(response.Url + "Put image blob:" + response.Content);
    }

    private void DeleteBlobs() {
        StartCoroutine(blobService.DeleteBlob(DeleteBlobCompleted, azureContainer, thumbnailArt));
        StartCoroutine(blobService.DeleteBlob(DeleteBlobCompleted, azureContainer, toenailArt));
    }

    private void DeleteBlobCompleted(RestResponse response)
    {
        if (response.IsError)
        {
            Debug.Log("Couldn't delete blob" + response.StatusCode + " Couldn't delete blob: " + response.ErrorMessage);
            return;
        }
        Debug.Log("Deleted blob" + " Deleted blob " + response.StatusCode);
    }
}
