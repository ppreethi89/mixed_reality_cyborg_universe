using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine.UI;
using TMPro;

public class TriggerAvatar : MonoBehaviour
{
    //Note that the ip address of PSM's personal computer which the avatar will run should be placed here
    string portURL = "http://127.0.0.1:8004";
    public GameObject psmInputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerWebClient()
    {
        string talkAppText = psmInputField.GetComponent<TMP_InputField>().text;
        string psmJSONData = "{\"source\":\"acat\", \"requestID\":\"someidgoeshere\", \"requestType\":\"avatar\", \"params\": { \"name\":\"ttsText\", \"valueRef\":\"inline\", \"valueFormat\":\"text\", \"value\":\"<speak><p><s>" + talkAppText + "</s></p></speak>\" } }";
        triggerWebClientAsync(psmJSONData);
    }

    public async Task triggerWebClientAsync(string psmJSONData)
    {
        Debug.Log(psmJSONData);
        using (var client = new HttpClient())
        {
            portURL = EventHandlerScript.avatarIPAddress;
            Debug.Log(portURL);
            try
            {
                var response = await client.PostAsync(portURL, new StringContent(psmJSONData, Encoding.UTF8, "application/json"));
            }
            catch
            {
                Debug.Log("Failed to connect");
            }
        }
    }
}
