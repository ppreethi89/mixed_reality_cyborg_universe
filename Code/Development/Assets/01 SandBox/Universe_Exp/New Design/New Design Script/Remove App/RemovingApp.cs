using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class RemovingApp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parentRemoveControl;
    public GameObject controlclass;
    public string filepath = "config.json";
    void Start()
    {
        controlclass = GameObject.Find("ControlClass");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void yesRemove()
    {
        string tobeRemoveGameobject = controlclass.GetComponent<animationControl>().removeGameObject;
        StartCoroutine(DisableApplication(tobeRemoveGameobject));
    }
    public void noRemove()
    {
        controlclass.GetComponent<animationControl>().removeGameObject = "";
        Destroy(parentRemoveControl);
        
    }

    IEnumerator DisableApplication(string removeGameobject)
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["GameObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];


        string name1 = removeGameobject;
        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {

            if (name1 == jsonNode["GameObject"][i]["name"])
            {

                jsonNode["GameObject"][i]["status"] = "Disabled";
                Debug.Log("DisabledApp");

            }

        }
        File.WriteAllText(fileName, jsonNode.ToString());
        controlclass.GetComponent<animationControl>().removeGameObject = "";
        GameObject toRemove = GameObject.Find(removeGameobject);
        Destroy(toRemove);
        Destroy(parentRemoveControl);

        yield return true;
    }
}
