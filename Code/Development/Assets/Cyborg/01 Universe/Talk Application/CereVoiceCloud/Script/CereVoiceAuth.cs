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
using System.Text.RegularExpressions;

public class CereVoiceAuth : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI text;
    private string reqUrl = "https://api.cerevoice.com/v2/auth";

    // Please reach out to admin for the 3 credentials below
    private string user;
    private string password;
    private string postTokenUrl;


    public AudioSource TTSspeaker;
    private string token;
    public List<String> listStrLineElements;
    // Start is called before the first frame update
    /*[SerializeField]
    ProgressController progressController;*/
    [SerializeField]
    GameObject progressBarPrefab;
    
    //static ProgressController staticProgressController;
    static GameObject staticProgressBarPrefab;
    static public CereVoiceAuth instance;

    [Serializable]
    public class jsonClass
    {
        public string data;
    }
    void Awake()
    {
        instance = this;
        //helper to convert non static to static var
        //staticProgressController = progressController;
        staticProgressBarPrefab = progressBarPrefab;
    }
    void Start()
    {
     
        StartCoroutine(GetRequestToken());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GetRequestToken()
    {
        UnityWebRequest req = createRequest(reqUrl, user, password);

        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.LogError(req.error);
        }
        else
        {
            Debug.Log("Req Successful");
        }
        JSONNode reqInfo = JSON.Parse(req.downloadHandler.text);
        Debug.Log(reqInfo);


        string reqToken = reqInfo["access_token"];
        token = reqToken;
        Debug.Log(reqToken);

       

       
    }
    public void onDwellSpeak()
    {
        // Search for a pattern that is not found in the input string.
        //string pattern = @"\<(.*?)\>";
        //string pattern = @"(?<=\<).+?(?=\>)";
        string input = text.text;
        listStrLineElements = input.Split(' ').ToList();
        Debug.Log(listStrLineElements);
        string sentences = "";
        foreach (string word in listStrLineElements)
        {
            if (word.StartsWith("<"))
            {
                sentences += "<voice emotion='" + word.Trim(new Char[] { '<', '>'}) + "'> ";
            }
            else if(word.StartsWith("["))
            {
                sentences += word.Trim(new Char[] { '[', ']' }) + " ";
            }
            else if (word.EndsWith("]"))
            {
                sentences += word.Trim(new Char[] { '[', ']' }) + " </voice> ";
            }
            else
            {
                sentences += word + " ";
            }
            Debug.Log(sentences);
        }
        Debug.Log(sentences);
        StartCoroutine(PostSpeech(sentences));
    }
    public static void SavedSpeechSpeak(string savedSpeech)
    {
        instance.StartCoroutine(instance.PostSpeech(savedSpeech));
    }
    IEnumerator PostSpeech(string textSpeech)
    {
           
        //string data = "Good afternoon everyone its nice to see you";

        string encoding = "UTF-8";
        /*string base65 = System.Convert.ToBase64String(
            System.Text.Encoding.GetEncoding(encoding)
                .GetBytes(data)
        );*/
        byte[] bytes;
        bytes = System.Text.Encoding.ASCII.GetBytes(textSpeech);



       /* WWWForm form = new WWWForm();
        form.AddField("data", data);*/

        UnityWebRequest post = UnityWebRequest.Post(postTokenUrl, textSpeech);
        post.uploadHandler = new UploadHandlerRaw(bytes);
        post.uploadHandler.contentType = "text/plain";
        post.SetRequestHeader("accept", "audio/wav");
        post.SetRequestHeader("charset", "utf-8");
        post.SetRequestHeader("Authorization", "Bearer " + token);
        post.SetRequestHeader("Content-Type", "text/plain");
        

        yield return post.SendWebRequest();
        if (post.isNetworkError || post.isHttpError)
        {
            onDwellSpeak();
            Debug.LogError(post.error);
            Debug.LogError("The token may be already expired.");
            StartCoroutine(GetRequestToken());
        }
        else
        {
            Debug.Log("Post Successful");
            JSONNode postInfo = JSON.Parse(post.downloadHandler.text);
            string audioUrl = postInfo["url"];
            Debug.Log(audioUrl);
            StartCoroutine(GetAudioClip(audioUrl));

        }
    }


    IEnumerator GetAudioClip(string url)
    {
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV);
        CyborgProgressBar.ShowLoadingBar();
        //StartCoroutine(LoadDownloadProgress.LoadProgress(www));
        //staticProgressBarPrefab.SetActive(true);
        www.SendWebRequest();
        while (!www.isDone)
        {
            CyborgProgressBar.value = www.downloadProgress;
            yield return new WaitForFixedUpdate();

        }
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
            CyborgProgressBar.HideLoadingBar();
        }
        else
        {

            AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
            TTSspeaker.clip = myClip;
            TTSspeaker.Play();
            //staticProgressBarPrefab.SetActive(false);
            CyborgProgressBar.HideLoadingBar();
            Debug.Log("Playing");

        }
        
            
        
    }
    UnityWebRequest createRequest(string url, string username, string password)
    {
        UnityWebRequest req = UnityWebRequest.Get(url);
        Debug.Log(req);
        // could also use "US-ASCII" or "ISO-8859-1" encoding
        string encoding = "UTF-8";
        string base64 = System.Convert.ToBase64String(
            System.Text.Encoding.GetEncoding(encoding)
                .GetBytes(username + ":" + password)
        );

        req.SetRequestHeader("Authorization", "Basic " + base64);
        
        Debug.Log(req);
        return req;
    }
    

    
}
