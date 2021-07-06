using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using SimpleJSON;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;
using TMPro;


public class CyborgNotification : MonoBehaviour
{
    // Start is called before the first frame update
    private static string reqUrl = "https://cyborgartist.azurewebsites.net/get_recent_notification";
    private static string postUrl = "https://cyborgartist.azurewebsites.net/push_new_notifications";
    [SerializeField]
    TalkAppReply talkAppReply;
    public GameObject loadingContainer;
    public GameObject finalArtContainer;
    [SerializeField]
    GameObject notifPrefab;
    [SerializeField]
    GameObject contentObj;
    [SerializeField]
    TextMeshPro notifTextCount;
    [SerializeField]
    GameObject notifButton;
    [SerializeField]
    GameObject notifPopUpContainer;
    [SerializeField]
    GameObject notifPopUpBox;
    //static public CyborgNotification instance;
    public class NotifLoad
    {
        public string source;
        public string subject;
        public string content;
    }
    void Start()
    {
        /*NotifLoad notif = new NotifLoad();
        notif.content = "Test content";
        notif.source = "Test source";
        notif.subject = "Test subject";
        string json = JsonUtility.ToJson(notif);

        StartCoroutine(PushNotification(json));*/
        InvokeRepeating("StartGetNotif", 5.0f, 5f);
        InvokeRepeating("CheckNotifNumber", 1.0f, 1.0f);
    }
    void StartGetNotif()
    {
        StartCoroutine(GetRequest());
    }

    // Update is called once per frame
    void Awake()
    {
        //instance = this;
    }
    void CheckNotifNumber()
    {
        int notifNumber = contentObj.transform.childCount;
        notifTextCount.text = notifNumber.ToString();
        if (notifNumber >= 1)
        {
            notifButton.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            notifButton.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    IEnumerator GetRequest()
    {
        //Debug.Log("Get Request");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(reqUrl))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = reqUrl.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                
                if (webRequest.downloadHandler.text.Length > 3)
                {
                    Debug.Log(webRequest.downloadHandler.text);

                    JSONNode notifInfo = JSON.Parse(webRequest.downloadHandler.text);
                    CreateNotificationInterface(notifInfo);
                }
                
            }
        }
    }

    //Use this static IEnumerator to pass a NotifLoad Object.
    //Convert the NotifLoad Object first before passing it to the function.
    //You can find NotifLoad structure on lines 34-39
    public static IEnumerator PushNotification(string notifContent)
    {
        
       
        byte[] bytes;
        bytes = System.Text.Encoding.ASCII.GetBytes(notifContent);

        UnityWebRequest post = UnityWebRequest.Post(postUrl, notifContent);
        post.uploadHandler = new UploadHandlerRaw(bytes);
        post.uploadHandler.contentType = "application/json";
        post.SetRequestHeader("Content-Type", "application/json");
        post.SetRequestHeader("Accept", "application/json");


        yield return post.SendWebRequest();
        if (post.isNetworkError || post.isHttpError)
        {
            Debug.LogError(post.error);
           
        }
        else
        {
            Debug.Log("Post Successful");
           
        }
    }
    void CreateNotificationInterface(JSONNode notifInfo)
    {
        //string notifDesc = notifInfo[0];
        //Debug.Log(JsonUtility.ToJson(notifDesc));

        //Debug.Log(notifDesc.GetType());
        //Debug.Log(notifInfo[0]["source"].GetType());
        Debug.Log("yow");
        for (int i = 0, j = notifInfo.Count - 1; i < notifInfo.Count; i++, j--)
        {

            string source = notifInfo[i]["source"];
            string subject = notifInfo[i]["subject"];
            string content = notifInfo[i]["content"];
            Debug.Log(source);
            Debug.Log(subject);
            Debug.Log(content);

            GameObject newObj = (GameObject)Instantiate(notifPrefab, contentObj.transform);

            newObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = source;
            newObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = subject;
            newObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = content;

            newObj.GetComponent<RedirectApp>().identifier = source;

            GameObject newPopUp = (GameObject)Instantiate(notifPopUpBox, notifPopUpContainer.transform);
            newPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().text = subject;
            newPopUp.GetComponent<RedirectApp>().identifier = source;
            try
            {
                if (source == "Cyborg Artist")
                {
                    try
                    {
                        string toenailUrl = "https://cyborguniverse.blob.core.windows.net/temporary-art/art_temp.jpg";
                        string thumbnailUrl = "https://cyborguniverse.blob.core.windows.net/temporary-art/art_temp_thumbnail.jpg";
                        StartCoroutine(DownloadImage(thumbnailUrl,true));
                        StartCoroutine(DownloadImage(toenailUrl, false));
                        Debug.Log("Downloading Content");
                    }
                    catch
                    {
                        Debug.Log("No imageURL content");
                    }
                }
             
            }


            catch
            {
                Debug.Log("No image URL to be downloaded.");
            }
            /*if (source == "PSM Webapp")
            {
                try
                {
                    
                    JSONNode notif = JSON.Parse(content);
                    Debug.Log("PSMWebApp Content");
                    Debug.Log(content);
                    Debug.Log(notifInfo["content"]["suggest_agree"][1]);
                    this.talkAppReply.PopulateReplies1(content);




                }
                catch
                {
                    Debug.Log("No Webapp content");
                }
            }*/
            StartCoroutine(DestroyAfterSomeTime(newPopUp));
        }

    }
    
    IEnumerator DestroyAfterSomeTime(GameObject notifPopUp)
    {
        yield return new WaitForSeconds(10f);
        Destroy(notifPopUp);
    }
    IEnumerator DownloadImage(string MediaUrl, bool IsThumbnailImage)

    {

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {

            Debug.Log(request.error);
        }
           
        else {

            if (IsThumbnailImage)
            {

                loadingContainer.SetActive(false);
                ArtistInfo.ThumbnailArtImage = ((DownloadHandlerTexture)request.downloadHandler).texture;
                finalArtContainer.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                finalArtContainer.SetActive(true);
                Debug.Log("Download Complete");
            }
            else {

                ArtistInfo.ToenailArtImage = ((DownloadHandlerTexture)request.downloadHandler).texture;
            }
        }     
    }
}
