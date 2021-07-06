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

public class VoiceAlert : MonoBehaviour
{
    // Start is called before the first frame update
    // public TextMeshProUGUI text;
    private string reqUrl = "https://api.cerevoice.com/v2/auth";
    private string user = "cyborg.psm@outlook.com";
    private string password = "aaaaaaaaaa";
    private string postTokenUrl = "https://api.cerevoice.com/v2/speak?voice=Adam&streaming=false";
    public AudioSource TTSspeaker;
    private string token;
    public TextMesh bpm;
    public TextMesh oxygen;
    static public VoiceAlert     instance;

    [Serializable]
    public class jsonClass
    {
        public string data;
    }
    void Awake()
    {
        instance = this;
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
       GameObject BPMValue=GameObject.Find("BPMValue");
       String bpm_threshold_alert=threshold_calculation("bpm",Int32.Parse(bpm.text));
       
       String oxygen_threshold_alert=threshold_calculation("oxygen",Int32.Parse(oxygen.text.Replace("%","")));
      

        StartCoroutine(PostSpeech(" Need immediate Attention.   The BPM value is "+ bpm_threshold_alert+ " at " +bpm.text+". And the oxygen value is "+ oxygen_threshold_alert + " at "+ oxygen.text));
    }

    public string threshold_calculation(String type,int value)
    {
        String threshold="";
        if(type=="bpm"){
         if(value<60)
       {
        threshold="low";
       }    
       else if(value >100)
       {
           threshold="high";
       }
       else
       threshold="normal";
        }

        else if(type=="oxygen"){

       if(value<93)
       {
        threshold="low";
       }    
       else
       threshold="normal";

        }
        return threshold;

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
        
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {

            AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
            TTSspeaker.clip = myClip;
            TTSspeaker.Play();
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
