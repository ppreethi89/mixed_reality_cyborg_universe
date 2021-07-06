using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Vuplex.WebView.Demos;
using Vuplex;
using Vuplex.WebView;

public class NewsApi : MonoBehaviour
{
    [SerializeField]
    AdvancedWebViewDemo webVuplex;
    [SerializeField]
    GameObject vuplexBrowser;
    [SerializeField]
    GameObject vuplexPanel;
    static GameObject staticVuplexPanel;

    [SerializeField]
    GameObject vuplexControlExit;
    static GameObject staticVuplexControlExit;

    [SerializeField]
    GameObject specialKeys;
    static GameObject staticspecialKeys;

    static GameObject staticVuplexBrowser;
    static AdvancedWebViewDemo staticWebVuplex;
    // Start is called before the first frame update
    public GameObject prefab;
    public GameObject newObj;

    // Reach out to admin for the credential below
    private readonly string apiURL;

    private void Awake()
    {
        staticVuplexBrowser = vuplexBrowser;

        staticWebVuplex = webVuplex;
        staticVuplexPanel = vuplexPanel;
        staticVuplexControlExit = vuplexControlExit;
        staticspecialKeys = specialKeys;
    }
    void Start()
    {
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
        StartCoroutine(InstantiateNews());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void ShowNewsVuplex(string url)
    {
        //staticVuplexBrowser.SetActive(true);
        staticVuplexControlExit.SetActive(true);
        staticspecialKeys.SetActive(true);
        GameObject vuplex = (GameObject)Instantiate(staticVuplexBrowser, staticVuplexPanel.transform, false);
        vuplex.GetComponent<WebViewPrefab>().InitialUrl = url;
        vuplex.SetActive(true);
        
        //staticWebVuplex._mainWebViewPrefab.WebView.LoadUrl(url);
    }
    public void Exit()
    {
        Destroy(vuplexPanel.transform.GetChild(2).gameObject);
        vuplexControlExit.SetActive(false);
        staticspecialKeys.SetActive(false);
    }
    IEnumerator InstantiateNews()
    {
        UnityWebRequest newsReq = UnityWebRequest.Get(apiURL);
        yield return newsReq.SendWebRequest();

        if (newsReq.isNetworkError || newsReq.isHttpError)
        {
            Debug.LogError(newsReq.error);
            yield break;
        }

        //Script for accessing values from the API

        JSONNode newsInfo = JSON.Parse(newsReq.downloadHandler.text);

        float totalResult = newsInfo["totalResults"];

        JSONNode newsDesc = newsInfo["articles"];
        string[] description = new string[newsDesc.Count];
       // int linkIndex = TMP_TextUtilities.FindIntersectingLink(pTextMeshPro, eventData.position, null);

        for (int i = 0, j = newsDesc.Count - 1; i < newsDesc.Count; i++, j--)
        {
            description[j] = newsDesc[i];
            string author = newsInfo["articles"][i]["author"];
            string title = newsInfo["articles"][i]["title"];
            string desc = newsInfo["articles"][i]["description"];
            
            string url = newsInfo["articles"][i]["url"];
            string urlimage = newsInfo["articles"][i]["urlToImage"];

            


            newObj = (GameObject)Instantiate(prefab, transform);

            GameObject childTitle = newObj.transform.GetChild(0).gameObject;
            GameObject childDesc = newObj.transform.GetChild(1).gameObject;
            GameObject childImage = newObj.transform.GetChild(2).gameObject;
            GameObject childAuthor = newObj.transform.GetChild(3).gameObject;
            newObj.transform.GetChild(2).gameObject.GetComponent<OpenSpecificNews>().newsURL = url;


            childTitle.GetComponent<TextMeshProUGUI>().text = title;
            childDesc.GetComponent<TextMeshProUGUI>().text = desc;
            childAuthor.GetComponent<TextMeshProUGUI>().text = author;
           // childUrl.GetComponent<TextMeshProUGUI>().text= url;



            UnityWebRequest newsIconRequest = UnityWebRequestTexture.GetTexture(urlimage);


            yield return newsIconRequest.SendWebRequest();

/*
            if (newsIconRequest.isNetworkError || newsIconRequest.isHttpError)
            {
                Debug.LogError(newsIconRequest);
                yield break;
            }*/

            
            try
            {
                childImage.GetComponent<RawImage>().texture = DownloadHandlerTexture.GetContent(newsIconRequest);
                childImage.GetComponent<RawImage>().texture.filterMode = FilterMode.Point;
            }
            catch
            {
                Debug.LogError(newsIconRequest);
            }
        }



    }
}
