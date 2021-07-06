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

public class TalkAppReply : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private static string url = "https://cyborgartist.azurewebsites.net/get_recent_notification_speech";

    private static string replies;
    public List<GameObject> replyPanel;
    public static List<GameObject> staticReplyPanel;
    public TMP_InputField talkAppInputField;
    private void Awake()
    {
        staticReplyPanel = replyPanel;
    }
    void OnEnable()
    {
        InvokeRepeating("GetRecentReplies", 2.0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetRecentReplies()
    {
        try
        {
            StartCoroutine(GetRequest());
        }
        catch
        {
            Debug.Log("Talk App Reply not yet active");
        }
        
    }
    public IEnumerator GetRequest()
    {
        if (CyborgTalkApp.isListen)
        {
            Debug.Log("Get Request");
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                //string[] pages = url.Split('/');
                //int page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    Debug.Log(" Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log(webRequest.downloadHandler.text);
                    replies = webRequest.downloadHandler.text;


                    if (webRequest.downloadHandler.text.Length > 3)
                    {
                        Debug.Log(webRequest.downloadHandler.text);

                        JSONNode notifInfo = JSON.Parse(webRequest.downloadHandler.text);
                        
                        PopulateReplies1(notifInfo);
                    }

                }
            }
        }
        
        
        yield return null;
    }
    public void PopulateReplies1(JSONNode notifInfo)
    {
        string source = notifInfo[0]["source"];
        string subject = notifInfo[0]["subject"];
        JSONNode content = notifInfo[0]["content"];

        //Debug.Log(content["suggested_replies"]);
        JSONNode replies = content["suggested_replies"];

        for (int i = 0, j = replies.Count - 1; i < replies.Count; i++, j--)
        {
            Debug.Log(replies[i]);

            staticReplyPanel[i].SetActive(true);

            staticReplyPanel[i].GetComponentInChildren<TextMeshPro>().text = replies[i];
            //PopulateReplies(replies[i], i);

        }



    }

    static IEnumerator PopulateReplies(string reply, int x)
    {

        staticReplyPanel[x].SetActive(true);
        Debug.Log("yow");
        staticReplyPanel[x].GetComponentInChildren<TextMeshPro>().text = reply;
        yield return new WaitForSeconds(2);

        //yield on a new YieldInstruction that waits for 5 seconds.



    }

    public void InsertReply(GameObject reply)
    {
        talkAppInputField.text += reply.GetComponentInChildren<TextMeshPro>().text;
        for (int i = 0; i < staticReplyPanel.Count(); i++)
        {
            staticReplyPanel[i].SetActive(false);
        }
        //CereVoiceAuth.SavedSpeechSpeak(reply.GetComponentInChildren<TextMeshPro>().text);
    }
}
