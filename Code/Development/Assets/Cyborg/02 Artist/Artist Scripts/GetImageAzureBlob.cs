using RESTClient;
using Azure.StorageServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using System;

public class GetImageAzureBlob : MonoBehaviour
{
    // Start is called before the first frame update
    private string azureContainer = "thumbnails";
    private StorageServiceClient client;
    private BlobService blobService;
    static public GetImageAzureBlob instance;
  
    //test variables
    public FetchImageFromStatic imageFromStatic;

    [SerializeField]
    private GameObject LoadingIcon;

    public GameObject ImagesContainer;
    public GameObject img;

    //[SerializeField]
    //public List<string> imagesList;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        client = StorageServiceClient.Create(ArtistInfo.AzureStorageAccount, ArtistInfo.AzureAccessKey);
        blobService = client.GetBlobService();        

    }
    public static void ThemeSelected()
    {
        instance.StartCoroutine(instance.OnResponse(ArtistInfo.URL));
        instance.LoadingIcon.SetActive(true);
    }
    public IEnumerator OnResponse(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log("Error: " + webRequest.error);
                AlertBox_General.ShowAlertBox("Error: The AIArtAssistant is currently unavailable");
                instance.LoadingIcon.SetActive(false);
            }
            else
            {
                if (webRequest.isDone)
                {                    
                    JSONNode jsonData = JSON.Parse(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
                    Debug.Log(jsonData["potential_images"].Count);
                    if (jsonData["potential_images"].Count != 0)
                    {
                        foreach (JSONNode image in jsonData["potential_images"])
                        {
                            string resourcePath = azureContainer + "/" + image;
                            ArtistInfo.CompositionImagesNames.Add(image);
                            //imagesList.Add(image);
                        }
                        ArtistInfo.maxPage = MaxPage();
                        imageFromStatic.DisplayPerPage();
                    }
                    else
                    {
                        AlertBox_General.ShowAlertBox("No images matched that theme. Please try a new one");
                        instance.LoadingIcon.SetActive(false);
                    }
                }
                
            }
        }
    }
    public void DestroyImages()
    {
        StartCoroutine(destroyChildInImagesContainer());
        StopAllCoroutines();
    }
    IEnumerator destroyChildInImagesContainer()
    {
        foreach (Transform child in ImagesContainer.transform)
        {
            Destroy(child.gameObject);
        }
        yield return true;
    }

    public static void DownloadImage(int pageNo)
    {
        instance.LoadingIcon.SetActive(true);
        instance.StartCoroutine(instance.destroyChildInImagesContainer());
        
        for (int x = 0; x < 6; x++)
        {

            GameObject singleImageContainer = Instantiate(instance.img);
            int currentImageNo = (pageNo * 6) + x;
            try
            {
                string resourcePath = instance.azureContainer + "/" + ArtistInfo.CompositionImagesNames[currentImageNo];
                
                
                singleImageContainer.transform.SetParent(instance.ImagesContainer.transform, false);
                singleImageContainer.name = ArtistInfo.CompositionImagesNames[currentImageNo];
                instance.StartCoroutine(instance.blobService.GetImageBlob(GetImageBlobComplete, resourcePath));
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
                    
                    Debug.Log("Failed to load image: " + response.StatusCode + response.ErrorMessage);
                }
                else
                {
                    Debug.Log("status code is" + response.StatusCode);
                   
                    Texture texture = response.Data;
                    ChangeImage(texture as Texture2D);
                }
            }


            void ChangeImage(Texture2D texture)
            {
                
                singleImageContainer.GetComponent<RawImage>().texture = texture;
              
               
            }
        }

        //Testing purpose for Notification Redirect Function
        /*CyborgNotification.NotifLoad notifLoad = new CyborgNotification.NotifLoad();
        notifLoad.source = "Cyborg Artist";
        notifLoad.subject = "Composition Image";
        notifLoad.content = "Azure Composition Images are downloaded";
        string notifJson = JsonUtility.ToJson(notifLoad);
        instance.StartCoroutine(CyborgNotification.PushNotification(notifJson));*/
        

        instance.LoadingIcon.SetActive(false);

    }
    // Update is called once per frame

    public int MaxPage()
    {
        int maxPage = ArtistInfo.CompositionImagesNames.Count / 6;
        if(ArtistInfo.CompositionImagesNames.Count%6 > 0)
        {
            maxPage++;
        }
        Debug.Log("Max page is " + maxPage);
        return maxPage;
    }
    void Update()
    {
        
    }
}
