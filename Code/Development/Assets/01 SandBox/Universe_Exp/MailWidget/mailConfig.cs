using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class mailConfig : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public string filepath = "mail.json";
    
    void Start()
    {
        StartCoroutine(getYourFile());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator getYourFile()
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["value"];
        string[] gameObjectString = new string[gameObjectJSON.Count];


        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            string emailSubject = jsonNode["value"][i]["subject"];
            string emailBodyPreview = jsonNode["value"][i]["bodyPreview"];
            string emailSenderName = jsonNode["value"][i]["from"]["emailAddress"]["name"];

            Debug.Log(emailSubject);
            Debug.Log(emailBodyPreview);
            Debug.Log(emailSenderName);

            GameObject emailData = (GameObject)Instantiate(prefab);
            emailData.transform.SetParent(gameObject.transform, false);
            emailData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = emailSubject;
            emailData.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = emailBodyPreview;
            /*if (status == "Enabled")
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
                string url = jsonNode["GameObject"][i]["url"];

                string viewString = "View" + viewNo;
                Transform view = GameObject.Find(viewString).transform;
                Vector3 position = new Vector3(posX, posY, posZ);
                Quaternion rotation = Quaternion.Euler(rotX, rotY, rotZ);



                //GameObject app = (GameObject)Instantiate(prefab, position, rotation, view);
                GameObject app = (GameObject)Instantiate(prefab);
                app.transform.SetParent(view, true);
                app.tag = tag;
                app.transform.localPosition = position;
                app.transform.localRotation = rotation;
                app.transform.name = name;
                app.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                app.GetComponent<AppDetails>().url = url;
                Transform getChild = app.transform.GetChild(0).GetChild(0);
                SpriteRenderer spIcon = getChild.GetComponent<SpriteRenderer>();
                Transform getChild2 = app.transform.GetChild(0).GetChild(1);
                TextMeshPro title = getChild2.GetComponent<TextMeshPro>();
                title.text = name;
                spIcon.sprite = Resources.Load<Sprite>("image/" + icon);

            }*/

        }



        yield return true;
        
    }
}
