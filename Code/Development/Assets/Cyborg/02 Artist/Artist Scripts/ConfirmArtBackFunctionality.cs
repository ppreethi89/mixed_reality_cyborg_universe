using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConfirmArtBackFunctionality : MonoBehaviour
{
    // Start is called before the first frame update
    private string createArtBaseUrl = "https://cyborgartist.azurewebsites.net/create_final_art?";
    [SerializeField]
    private GameObject styleFinalImageContainer;
    [SerializeField]
    private GameObject colorFinalImageContainer;
    [SerializeField]
    private GameObject cropPeopleFinalImageContainer;

    void OnEnable()
    {
                   
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back() {

        styleFinalImageContainer.GetComponent<DisplayFinalImages>().emptyImageConatiner();
        colorFinalImageContainer.GetComponent<DisplayFinalImages>().emptyImageConatiner();
        cropPeopleFinalImageContainer.GetComponent<DisplayFinalImages>().emptyImageConatiner();
        ArtistInfo.PhotomosaicSelected = false;

    }
    public void createArt() {

        //Style image must be selected to create and art

        //Style image must be selected to create and art
        if (styleFinalImageContainer.GetComponent<RawImage>().texture == null & !ArtistInfo.PhotomosaicSelected)
        {
            AlertBox_General.ShowAlertBox("A style image is required to create an art");
        }
        else {

            //Saving style and color images 
            ArtistInfo.ColorImageSelected = colorFinalImageContainer.GetComponent<RawImage>().texture;
            ArtistInfo.StyleImageSelected = styleFinalImageContainer.GetComponent<RawImage>().texture;
            ArtistInfo.CropImageSelected = cropPeopleFinalImageContainer.GetComponent<RawImage>().texture;

            //Building QueryParameters for each selection
            string compositionImageQueryParams = UnityWebRequest.EscapeURL(ArtistInfo.CompositionImageSelected.name);
            string styleImageQueryParams = UnityWebRequest.EscapeURL(noImageSelectedCheck(styleFinalImageContainer));
            string colorImageQueryParams = UnityWebRequest.EscapeURL(noImageSelectedCheck(colorFinalImageContainer));
            string cropImageQueryParams = UnityWebRequest.EscapeURL(noImageSelectedCheck(cropPeopleFinalImageContainer));
            //string potentialCompositionImagesQueryParams = UnityWebRequest.EscapeURL(imagesNameToOneString());

            //Building creat Art url endpoint
            string urlCreateArt = createArtBaseUrl + "composition_image=" + compositionImageQueryParams + "&style_image=" + styleImageQueryParams +
            "&color_image=" + colorImageQueryParams + "&crop_people=" + cropImageQueryParams + "&photomosaic=" + ArtistInfo.PhotomosaicSelected;
            //+"&potentialComposition_imagesNames=" + potentialCompositionImagesQueryParams;
            Debug.Log(urlCreateArt);

            //Trigger CreateArt endpoint
            StartCoroutine(OnResponse(urlCreateArt));
            //Testing when trigger is not enabled. 
            //AlertBox_General.ShowAlertBox("The Art Creation is in progress");
            //Activate finalArt Page prefab after the art creation was submitted.
            //gameObject.GetComponent<SetActiveObject_General>().setActiveTransition();
            //Back();
        }
    }

    private string noImageSelectedCheck(GameObject imageContainer) {

        if (imageContainer.GetComponent<RawImage>().texture == null)
        {
            return "noImageSelected";
        }

        //Color image was selected for art creation
        else
        {

            return imageContainer.GetComponent<RawImage>().texture.name;
        }


    }

    
    private IEnumerator OnResponse(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log("Error: " + webRequest.error);
                AlertBox_General.ShowAlertBox("Error: The AIArtAssistant is currently unavailable");
            }
            else
            {
                if (webRequest.isDone)
                {
                    Debug.Log("WebRequest Done");
                    Debug.Log(webRequest.downloadHandler.text);
                    AlertBox_General.ShowAlertBox("The Art Creation is in progress");
                    gameObject.GetComponent<SetActiveObject_General>().setActiveTransition();
                    Back();
                }
            }
        }
    }
}
