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

public class TriggerPSMAvatar : MonoBehaviour
{
    // Start is called before the first frame update
    String port_url = "http://localhost:8004";
    String psm_xml_path = "C:\\Users\\Zoriel\\Desktop\\psm\\test\\TTS.xml";
    //String port_url = "http://192.168.3.190:8004";
    String psm_json_string = "{\"source\":\"acat\", \"requestID\":\"someidgoeshere\", \"requestType\":\"avatar\", \"params\": { \"name\":\"ttsText\", \"valueRef\":\"file\", \"valueFormat\":\"xml\", \"value\":\"C:/Users/Zoriel/Desktop/psm/test/TTS.xml\" } }";

    public GameObject psm_inputfield;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void trigger_webclient()
    {
        Debug.Log(psm_inputfield.GetComponent<TMP_InputField>().text);
        rewrite_xml(psm_inputfield.GetComponent<TMP_InputField>().text);
        trigger_webclientAsync();
        Debug.Log("request sent!");
    }

    public async Task trigger_webclientAsync()
    {
        Debug.Log(psm_json_string);
        port_url = EventHandlerScript.avatarIPAddress;
        Debug.Log(port_url);
        using (var client = new HttpClient())
        {
            var response = await client.PostAsync(port_url, new StringContent(psm_json_string, Encoding.UTF8, "application/json"));
        }
    }

    public void rewrite_xml(String psm_text)
    {
        XmlTextWriter xmlw = new XmlTextWriter(@psm_xml_path, System.Text.Encoding.UTF8);
        xmlw.WriteStartDocument();
        xmlw.WriteStartElement("speak");
        xmlw.WriteStartElement("p");
        xmlw.WriteStartElement("s");
        xmlw.WriteString(psm_text);
        xmlw.WriteEndElement();
        xmlw.WriteEndElement();
        xmlw.WriteEndDocument();
        xmlw.Close();
        Debug.Log("xml saved!");
    }
}
