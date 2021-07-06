using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using TMPro;



public class PhonecallNotification : MonoBehaviour
{
    public TextMesh bpm;
    public TextMesh oxygen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateCall()
    {
        StartCoroutine(GetRequest());
    }


    IEnumerator GetRequest()
    {
        //string PhNo = "+12675308382";
        string PhNo = EventHandlerScript.staticContactNo;
        string uri = "https://medicphonecall20210124081859.azurewebsites.net/api/Phonecall_Functionality?bpm=" + bpm.text + "&oxygen=" + oxygen.text + "&destinationPhoneNumber=" + PhNo;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
    
}