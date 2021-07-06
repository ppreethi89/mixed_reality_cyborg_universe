using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class AddSpeeches : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public TMP_InputField input;
    public string filepath = "savedSpeeches.json";
    public bool startnow = false;
    public GameObject parentObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showConfirmation()
    {
        string message = "Are you sure you want to add speech?";
        ConfirmationBox_General.showConfirmation(Add, message);
    }
    public void Add()
    {
        GameObject speech = (GameObject)Instantiate(prefab);
        speech.transform.SetParent(parentObj.transform, false);
        speech.GetComponentInChildren<TextMeshPro>().text = input.text;

        StartCoroutine(Save());
        AlertBox_General.ShowAlertBox("Speech Saved!");
    }
    public IEnumerator Save()
    {
        //string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        //Debug.Log(fileName);

        //string jsonString = File.ReadAllText(fileName);
        string jsonString = CyborgFileRead_General.ReadFileApplicationData(filepath);
        JSONNode jsonNode = JSON.Parse(jsonString);
        
        JSONNode gameObjectJSON = jsonNode["Speeches"];
        jsonNode["Speeches"][gameObjectJSON.Count] = input.text;
        Debug.Log(jsonNode["Speeches"][gameObjectJSON.Count-1]);



        /*string[] gameObjectString = new string[gameObjectJSON.Count];

        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            string status = jsonNode["GameObject"][i + 1];
            GameObject obj;
            if (status == "Enabled")
            {
                obj = GameObject.Find(jsonNode["GameObject"][i]["name"]);

                jsonNode["GameObject"][i]["position"]["posX"] = obj.GetComponent<Transform>().localPosition.x;
                jsonNode["GameObject"][i]["position"]["posY"] = obj.GetComponent<Transform>().localPosition.y;
                jsonNode["GameObject"][i]["position"]["posZ"] = obj.GetComponent<Transform>().localPosition.z;

                jsonNode["GameObject"][i]["rotation"]["rotX"] = obj.GetComponent<Transform>().rotation.x;
                jsonNode["GameObject"][i]["rotation"]["rotY"] = obj.GetComponent<Transform>().rotation.y;
                jsonNode["GameObject"][i]["rotation"]["rotZ"] = obj.GetComponent<Transform>().rotation.z;

                jsonNode["GameObject"][i]["scale"]["scaleX"] = obj.GetComponent<Transform>().localScale.x;
                jsonNode["GameObject"][i]["scale"]["scaleY"] = obj.GetComponent<Transform>().localScale.y;
                jsonNode["GameObject"][i]["scale"]["scaleZ"] = obj.GetComponent<Transform>().localScale.z;
                Debug.Log("Save Success");
            }
        }*/
        //File.WriteAllText(fileName, jsonNode.ToString());
        CyborgFileRead_General.WriteFileApplicationData(filepath, jsonNode.ToString());
        InitializeDynamicConfig.SaveConfigToAzure();
        yield return true;
    }
}
