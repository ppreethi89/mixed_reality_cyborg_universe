using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class WidgetConfig : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> widgetPrefabs;
    public string filepath = "config.json";
    public bool startnow = false;
    [SerializeField]
    float delay;
    [SerializeField]
    bool intro;
    void Start()
    {
        StartCoroutine(getYourFile());
    }

    IEnumerator getYourFile()
    {
        if (intro)
        {
            yield return new WaitForSeconds(delay);
        }
       
        //string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        //Debug.Log(fileName);

        //string jsonString = File.ReadAllText(fileName);
        string jsonString = CyborgFileRead_General.ReadFileApplicationData(filepath);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["WidgetObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];


        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            string status = jsonNode["WidgetObject"][i]["status"];

            if (status == "Enabled")
            {
                float posX = jsonNode["WidgetObject"][i]["position"]["posX"];
                float posY = jsonNode["WidgetObject"][i]["position"]["posY"];
                float posZ = jsonNode["WidgetObject"][i]["position"]["posZ"];
                string name = jsonNode["WidgetObject"][i]["name"];

                float rotX = jsonNode["WidgetObject"][i]["rotation"]["rotX"];
                float rotY = jsonNode["WidgetObject"][i]["rotation"]["rotY"];
                float rotZ = jsonNode["WidgetObject"][i]["rotation"]["rotZ"];

                float scaleX = jsonNode["WidgetObject"][i]["scale"]["scaleX"];
                float scaleY = jsonNode["WidgetObject"][i]["scale"]["scaleY"];
                float scaleZ = jsonNode["WidgetObject"][i]["scale"]["scaleZ"];


                float viewNo = jsonNode["WidgetObject"][i]["view"];
                string tag = jsonNode["WidgetObject"][i]["type"];


                string viewString = "View" + viewNo;
                Transform view = GameObject.Find(viewString).transform;
                Vector3 position = new Vector3(posX, posY, posZ);
                Quaternion rotation = Quaternion.Euler(rotX, rotY, rotZ);


                foreach (GameObject prefab in widgetPrefabs)
                {
                    Debug.Log("Prfab is " + prefab.name);
                    if (prefab.name == jsonNode["WidgetObject"][i]["prefab"])
                    {
                        Debug.Log(jsonNode["WidgetObject"][i]["prefab"]);
                        Debug.Log(prefab.name);
                        GameObject app = (GameObject)Instantiate(prefab);
                        app.SetActive(true);
                        app.transform.SetParent(view, false);
                        app.tag = tag;
                        app.transform.localPosition = position;
                        app.transform.localRotation = rotation;
                        app.transform.name = name;
                        app.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                    }
                    
                }

            }

        }



        yield return true;
        startnow = true;
    }

}
