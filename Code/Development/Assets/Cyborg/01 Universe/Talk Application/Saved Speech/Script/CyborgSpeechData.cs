using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class CyborgSpeechData : MonoBehaviour
{
    // Start is called before the first frame update
    public string filepath = "savedSpeeches.json";
    public TextMeshProUGUI speech;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Speak()
    {
        CereVoiceAuth.SavedSpeechSpeak(speech.text);
    }
    public void DeleteSpeech()
    {
        string message = "Do you want to delete this speech?";
        ConfirmationBox_General.showConfirmation(UpdateSpeechJson, message);
    }
    public void UpdateSpeechJson()
    {
        StartCoroutine(GetSpeechFile());

    }
    IEnumerator GetSpeechFile()
    {
        string fileName = CyborgFileRead_General.ReadFileApplicationData(filepath);

        JSONNode jsonNode = JSON.Parse(fileName);

        JSONNode gameObjectJSON = jsonNode["Speeches"];

        
        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            JSONNode item = jsonNode["Speeches"][i];
            if (item == speech.text)
            {
                JSONNode deletethis = jsonNode["Speeches"][i];

                jsonNode["Speeches"][i].Remove(item);
                InitializeDynamicConfig.SaveConfigToAzure();

                AlertBox_General.ShowAlertBox("Updated Speech List!");
                Destroy(gameObject.transform.gameObject);

            }
           

        }

        yield return null;
    }
}
