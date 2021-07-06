using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;

public class CyborgPeopleData : MonoBehaviour
{
    // Start is called before the first frame update
    public string filepath = "savedSpeeches.json";
    public TextMeshProUGUI people;
    void Start()
    {
        //people = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InputPeople()
    {
        CyborgTalkApp.staticTalkAppInputField.text += people.text + " ";
        CyborgTalkApp.staticTalkAppInputField.caretPosition = CyborgTalkApp.staticTalkAppInputField.text.Length;
    }
    public void DeletePeopleData()
    {
        string message = "Do you want to delete " + people.text;
        ConfirmationBox_General.showConfirmation(UpdatePeopleJson, message);
    }
    public void UpdatePeopleJson()
    {
        StartCoroutine(GetPeopleFile());
        
    }
    IEnumerator GetPeopleFile()
    {
        string fileName = CyborgFileRead_General.ReadFileApplicationData(filepath);

        JSONNode jsonNode = JSON.Parse(fileName);

        JSONNode gameObjectJSON = jsonNode["People"];

        
        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            JSONNode item = jsonNode["People"][i];

            for (int x = 0, y = item.Count - 1; x < item.Count; x++, y--)
            {

                if (item[x] == people.text)
                {
                    JSONNode deletethis = jsonNode["People"][i][x];

                    jsonNode["People"][i].Remove(item[x]);

                    CyborgFileRead_General.WriteFileApplicationData(filepath, jsonNode.ToString());
                    InitializeDynamicConfig.SaveConfigToAzure();

                    AlertBox_General.ShowAlertBox("Updated People List!");
                    Destroy(gameObject.transform.gameObject);

                }
            }

        }

        yield return null;
    }
}
