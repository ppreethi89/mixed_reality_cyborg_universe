using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class removeAppTile : MonoBehaviour
{
    // Start is called before the first frame update
    public string filepath = "config.json";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void removeApplication()
    {
        
        StartCoroutine(DisableApplication());
        
    }


    IEnumerator DisableApplication()
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["GameObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];


        string name1 = gameObject.GetComponent<Transform>().name;
        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {

            if (name1 == jsonNode["GameObject"][i]["name"])
            {

                jsonNode["GameObject"][i]["status"] = "Disabled";
                Debug.Log("DisabledApp");

            }

        }
        File.WriteAllText(fileName, jsonNode.ToString());
        Destroy(gameObject);
        yield return true;
    }
}
