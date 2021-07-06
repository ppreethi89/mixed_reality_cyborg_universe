using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SavePosition : MonoBehaviour
{
    // Start is called before the first frame update
    public string filepath;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startSavePos()
    {
        StartCoroutine(SavePos());
    }

    public void startSaveScale()
    {
        StartCoroutine(SaveScale());
        Debug.Log("Saving");
    }


    IEnumerator SavePos()
    {
        string name1 = GameObject.Find("ControlClass").GetComponent<animationControl>().movingGameObject;
        if (name1 == "TalkAppUpdated(Clone)")
        {
            filepath = "talkappConfig.json";
        }
        else
        {
            filepath = "config.json";
        }
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["GameObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];

        
        
        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {

            if (name1 == jsonNode["GameObject"][i]["name"])
            {
                GameObject obj = GameObject.Find(jsonNode["GameObject"][i]["name"]);
                jsonNode["GameObject"][i]["position"]["posX"] = obj.GetComponent<Transform>().localPosition.x;
                jsonNode["GameObject"][i]["position"]["posY"] = obj.GetComponent<Transform>().localPosition.y;
                jsonNode["GameObject"][i]["position"]["posZ"] = obj.GetComponent<Transform>().localPosition.z;
                Debug.Log("Save Position");

            }

        }
        File.WriteAllText(fileName, jsonNode.ToString());
        GameObject.Find("ControlClass").GetComponent<animationControl>().movingGameObject = "";

        yield return true;
    }


    IEnumerator SaveScale()
    {
        string name1 = GameObject.Find("ControlClass").GetComponent<animationControl>().movingGameObject;
        /*
                if (name1 == "TalkAppUpdated(Clone)")
                {
                    filepath = "talkappConfig.json";
                }
                else
                {
                    filepath = "config.json";
                }*/
        filepath = "talkappConfig.json";
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);
        //Debug.Log(name1);
        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["GameObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];

        Debug.Log("Sved?");

        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {

            if ("TalkAppUpdated(Clone)" == jsonNode["GameObject"][i]["name"])
            {
                GameObject obj = GameObject.Find(jsonNode["GameObject"][i]["name"]);
                jsonNode["GameObject"][i]["scale"]["scaleX"] = obj.GetComponent<Transform>().localScale.x;
                jsonNode["GameObject"][i]["scale"]["scaleY"] = obj.GetComponent<Transform>().localScale.y;
                jsonNode["GameObject"][i]["scale"]["scaleZ"] = obj.GetComponent<Transform>().localScale.z;
                Debug.Log("Save Scale");

            }

        }
        File.WriteAllText(fileName, jsonNode.ToString());

        yield return true;
    }
}
