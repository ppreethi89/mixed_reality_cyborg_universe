using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class DisabledAppsReader : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public string filepath = "config.json";
    void Start()
    {
        StartCoroutine(getYourFile());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void restartDisableApp()
    {
        StartCoroutine(getYourFile());
    }
    IEnumerator getYourFile()
    {
        //string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        //Debug.Log(fileName);
        string jsonString = CyborgFileRead_General.ReadFileApplicationData(filepath);
        //string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["GameObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];


        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            string status = jsonNode["GameObject"][i]["status"];
            
            if (status == "Disabled")
            {
                string name = jsonNode["GameObject"][i]["name"];
                string icon = jsonNode["GameObject"][i]["icon"];

                GameObject app = (GameObject)Instantiate(prefab, gameObject.transform);
                /*                app.GetComponent<TextMeshProUGUI>().text = name;
                 *                
                */
                app.transform.name = name;
                app.GetComponentInChildren<TextMeshPro>().text = name;
                SpriteRenderer spIcon = app.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
                //Image spIcon = app.GetComponentInChildren<Image>();
                spIcon.sprite = Resources.Load<Sprite>("image/" + icon);
            }

        }



        yield return true;
    }
}
