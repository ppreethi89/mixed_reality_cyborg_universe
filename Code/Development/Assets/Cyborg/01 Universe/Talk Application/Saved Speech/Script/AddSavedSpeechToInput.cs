using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using SimpleJSON;
using System.IO;


public class AddSavedSpeechToInput : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField input;
    public TextMeshPro pick;
    public GameObject personNamePrefab;
    public string filepath = "savedSpeeches.json";
    void Start()
    {
        input = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void inputspeech()
    {
        input.text += pick.text + " ";
        input.caretPosition = input.text.Length;
    }
    public void pickPeople()
    {
        if (EventHandlerScript.removeModeActive == false)
        {
            input.text += pick.text + " ";
            input.caretPosition = input.text.Length;
        }
        else
        {
           string message = "Do you want to delete " + pick.text;
            ConfirmationBox_General.showConfirmation(updatePeopleJson, message);
        }
    }
    public void updatePeopleJson()
    {
        StartCoroutine(getYourFile());
        AlertBox_General.ShowAlertBox("Updated People List!");
    }

    IEnumerator getYourFile()
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["People"];
        string[] gameObjectString = new string[gameObjectJSON.Count];

        /*foreach (JSONNode item in gameObjectJSON)
        {*/
        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            JSONNode item = jsonNode["People"][i];
            
            for (int x = 0, y = item.Count - 1; x < item.Count; x++, y--)
            {
                
                if (item[x] == pick.text)
                {
                    JSONNode deletethis = jsonNode["People"][i][x];
                    
                    jsonNode["People"][i].Remove(item[x]);


                }
            }
            
        }




        /*}*/



        File.WriteAllText(fileName, jsonNode.ToString());
        Destroy(personNamePrefab);
        yield return true;
       
    }
}
