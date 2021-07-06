using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class InitializeSave : MonoBehaviour
{
    // Start is called before the first frame update
    static public InitializeSave instance;
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    float delay;

    [SerializeField]
    private static string filepath = "config.json";
    [SerializeField]
    bool intro;
    Transform objDetails;

    bool startLoad = false;


    /*    private void Awake()
        {
            instance = this;
            instance.objDetails = this.objDetails;
        }*/
    private void Awake()
    {
        string streamingAssetFile = CyborgFileRead_General.ReadFileStreamingAsset(filepath);
        string file = Path.Combine(Application.persistentDataPath, filepath);
        if (startLoad == false)
        {
            /*if (!File.Exists(file))
            {
                CyborgFileRead_General.WriteFileApplicationData(filepath, streamingAssetFile);
                startLoad = true;
            }*/
            CyborgFileRead_General.WriteFileApplicationData(filepath, streamingAssetFile);
            startLoad = true;
        }
        
       
    }
    void Start()
    {
        
        StartCoroutine(initialize());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator initialize()
    {
        /* string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
         Debug.Log(fileName);

         string jsonString = File.ReadAllText(fileName);*/
        if (intro)
        {
            yield return new WaitForSeconds(delay);
        }
        
        Debug.Log("Start Initializing");
        string fileName = CyborgFileRead_General.ReadFileApplicationData(filepath);
        JSONNode jsonNode = JSON.Parse(fileName);

        JSONNode gameObjectJSON = jsonNode["GameObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];


        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            string status = jsonNode["GameObject"][i]["status"];

            if (status == "Enabled")
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
                app.GetComponent<AppDetails>().view = viewNo;
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

            }

        }



        yield return null;
    }
   
    public static IEnumerator SaveTransform(GameObject app)
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
       
        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["GameObject"];

        //string[] gameObjectString = new string[gameObjectJSON.Count];

        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            string status = jsonNode["GameObject"][i]["status"];
            //GameObject obj = GameObject.Find(jsonNode["GameObject"][i]["name"]);

            if (app.transform.name == jsonNode["GameObject"][i]["name"])
            {
                jsonNode["GameObject"][i]["position"]["posX"] = app.GetComponent<Transform>().localPosition.x;
                jsonNode["GameObject"][i]["position"]["posY"] = app.GetComponent<Transform>().localPosition.y;
                jsonNode["GameObject"][i]["position"]["posZ"] = app.GetComponent<Transform>().localPosition.z;

                jsonNode["GameObject"][i]["rotation"]["rotX"] = app.GetComponent<Transform>().rotation.x;
                jsonNode["GameObject"][i]["rotation"]["rotY"] = app.GetComponent<Transform>().rotation.y;
                jsonNode["GameObject"][i]["rotation"]["rotZ"] = app.GetComponent<Transform>().rotation.z;

                jsonNode["GameObject"][i]["scale"]["scaleX"] = app.GetComponent<Transform>().localScale.x;
                jsonNode["GameObject"][i]["scale"]["scaleY"] = app.GetComponent<Transform>().localScale.y;
                jsonNode["GameObject"][i]["scale"]["scaleZ"] = app.GetComponent<Transform>().localScale.z;
                Debug.Log("Save Success");
            }
        }
        File.WriteAllText(fileName, jsonNode.ToString());
        yield return true;
    }


    public static IEnumerator SaveRemoveApp(GameObject app)
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);

        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["GameObject"];

        //string[] gameObjectString = new string[gameObjectJSON.Count];

        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            //string status = jsonNode["GameObject"][i]["status"];
            //GameObject obj = GameObject.Find(jsonNode["GameObject"][i]["name"]);

            if (app.transform.name == jsonNode["GameObject"][i]["name"])
            {
                jsonNode["GameObject"][i]["status"] = "Disabled";
                Debug.Log("Save Success");
            }
        }
        //File.WriteAllText(fileName, jsonNode.ToString());
        CyborgFileRead_General.WriteFileApplicationData(filepath, jsonNode.ToString());
        yield return true;
    }
}
