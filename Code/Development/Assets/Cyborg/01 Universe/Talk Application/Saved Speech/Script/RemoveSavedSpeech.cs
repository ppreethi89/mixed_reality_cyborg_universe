using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class RemoveSavedSpeech : MonoBehaviour
{
    //public bool removeModeActivated = false;
    //public GameObject removeButton;

    public GameObject parentobj;
    public TMP_InputField input;
    public string filepath = "savedSpeeches.json";
    public bool startnow = false;


    public TMP_InputField textField;
    public TextMeshPro pick;
    public TextMeshPro label;

    static public RemoveSavedSpeech instance;
    public bool speakerMode;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOffRemoveMode()
    {
        /* if (EventHandlerScript.removeModeActive == false)
         {
             EventHandlerScript.removeModeActive = true;
             //instance.label.text = "Remove Mode";  

         }
         else
         {
             EventHandlerScript.removeModeActive = false;
             //instance.label.text = "Play Mode";
         }*/
        EventHandlerScript.removeModeActive = !EventHandlerScript.removeModeActive;
    }

    public void Dwell()
    {
        if (EventHandlerScript.speakerMode)
        {
            CereVoiceAuth.SavedSpeechSpeak(pick.text);
        }
        else if (!EventHandlerScript.speakerMode && EventHandlerScript.removeModeActive ==false)
        {
            addSpeechToInput();
        }
        else if (EventHandlerScript.removeModeActive == true)
        {
            string message = "Are you sure you want to remove saved speech?"; //confirmation box question
            ConfirmationBox_General.showConfirmation(removeSpeech, message);
        }

  
    }
    public void SpeakerDwell()
    {
        EventHandlerScript.speakerMode = !EventHandlerScript.speakerMode;
        /*if (speakerMode)
        {
            CereVoiceAuth.SavedSpeechSpeak(pick.text);
        }*/

    }

    
    public void addSpeechToInput()
    {
        textField.text += pick.text + " ";
        textField.caretPosition = textField.text.Length;
    }


    public void removeSpeech()
    {
        string word = gameObject.GetComponentInChildren<TextMeshPro>().text;
        StartCoroutine(Save(word));
        Destroy(this.gameObject);
        AlertBox_General.ShowAlertBox("Removed Successfully!");
        //Destroy(transform.parent.gameObject);
    }

    public IEnumerator Save(string word)
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);


        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        List<string> myList = new List<string>();
        foreach (JSONNode saved in jsonNode["Speeches"])
        {
            if (saved == word)
            {
                continue;
            }
            else
            {
                myList.Add(saved);
            }
        }


        for (int i = 0; i < myList.Count; i++)
        {
            jsonNode["Speeches"][i] = myList[i]; //Append each word from list
        }

        for (int i = jsonNode["Speeches"].Count - 1; i > myList.Count - 1; i--)
        {
            jsonNode["Speeches"].Remove(i); //delete excess from list
        }
        Debug.Log(jsonNode["Speeches"]);


        File.WriteAllText(fileName, jsonNode.ToString());
        InitializeDynamicConfig.SaveConfigToAzure();
        yield return true;
    }
}
