using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class EnableApp : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject prefab;
    public GameObject prefabAddMessage;
    public string filepath = "config.json";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Enable()
    {
        StartCoroutine(EnableApplication());
    }
    IEnumerator EnableApplication()
    {
        //string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        //Debug.Log(fileName);

        //string jsonString = File.ReadAllText(fileName);
        string jsonString = CyborgFileRead_General.ReadFileApplicationData(filepath);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["GameObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];
        //Transform view = GameObject.Find("View0").transform;

        string name1 = gameObject.GetComponentInChildren<TextMeshPro>().text;
        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            
            if (name1 == jsonNode["GameObject"][i]["name"])
            {

                float posX = jsonNode["GameObject"][i]["position"]["posX"];
                float posY = jsonNode["GameObject"][i]["position"]["posY"];
                float posZ = jsonNode["GameObject"][i]["position"]["posZ"];
                string name = jsonNode["GameObject"][i]["name"];

                float rotX = jsonNode["GameObject"][i]["rotation"]["rotX"];
                float rotY = jsonNode["GameObject"][i]["rotation"]["rotY"];
                float rotZ = jsonNode["GameObject"][i]["rotation"]["rotZ"];

                float scaleX = jsonNode["GameObject"][i]["scale"]["scaleX"];
                float scaleY = jsonNode["GameObject"][i]["scale"]["scaleY"];
                float scaleZ = jsonNode["GameObject"][i]["scale"]["scaleZ"];

                string icon = jsonNode["GameObject"][i]["icon"];
                float viewNo = jsonNode["GameObject"][i]["view"];
                string tag = jsonNode["GameObject"][i]["type"];
                string viewString = "View" + viewNo;
                string url = jsonNode["GameObject"][i]["url"];
                //Transform view = GameObject.Find(viewString).transform;
                Transform view = ViewGameObjects.FindView(viewString);
                Vector3 position = new Vector3(posX, posY, posZ);
                Quaternion rotation = Quaternion.Euler(rotX, rotY, rotZ);

                GameObject app = (GameObject)Instantiate(prefab);
                app.GetComponent<AppDetails>().url = url;
                app.transform.SetParent(view, true);
                app.transform.localPosition = position;
                app.transform.localRotation = rotation;
                app.transform.name = name;
                app.tag = tag;
                app.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                Transform getChild = app.transform.GetChild(0).GetChild(0);
                SpriteRenderer spIcon = getChild.GetComponent<SpriteRenderer>();
                Transform getChild2 = app.transform.GetChild(0).GetChild(1);
                TextMeshPro title = getChild2.GetComponent<TextMeshPro>();
                title.text = name;
                spIcon.sprite = Resources.Load<Sprite>("image/" + icon);

                string status = jsonNode["GameObject"][i]["status"];
                jsonNode["GameObject"][i]["status"] = "Enabled";

            }

        }
        //GameObject addapp = (GameObject)Instantiate(prefabAddMessage);
        //addapp.transform.position = new Vector3(0, 0, 2);

        //File.WriteAllText(fileName, jsonNode.ToString());
        CyborgFileRead_General.WriteFileApplicationData(filepath, jsonNode.ToString());
        Destroy(gameObject);
        yield return true;
    }
}
