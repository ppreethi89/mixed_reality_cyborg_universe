using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;

public class SavedPeople : MonoBehaviour
{
    // Start is called before the first frame update
          
    public GameObject prefab;
    public string filepath = "savedSpeeches.json";
    public bool startnow = false;
    public string content;
    public bool family;
    public bool pa;

    public static SavedPeople instance;
    bool hasInternet;
    private void Awake()
    {
        instance = this;
        instance.content = content;
        instance.pa = pa;
        instance.family = family;
    }   
    void OnEnable()
    {
        StartCoroutine(CheckInternet());
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
            Debug.Log("DESTROY CHILD");
        }
        //StartCoroutine(getYourFile());
        /* if (Application.internetReachability == NetworkReachability.NotReachable)
         {
             Debug.Log("Error. Check internet connection!");
             Start();
         }
         else
         {
             //DownloadTextFile();
             Debug.Log("Has Internet Connection");

             *//*while (!InitializeDynamicConfig.done)
             {
                 Debug.Log("Not yet saved");
             }*//*
             //StartCoroutine(getYourFile());
            *//* if (content == "family")
             {
                 InitializeDynamicConfig.LoadConfigFromAzure(GetFamily);
             }
             if (content == "pa")
             {
                 InitializeDynamicConfig.LoadConfigFromAzure(GetPA);
             }*//*
             switch (content)
             {
                 case "pa":
                     InitializeDynamicConfig.LoadConfigFromAzure(GetPA);
                     break;
                 case "family":
                     InitializeDynamicConfig.LoadConfigFromAzure(GetFamily);
                     break;

             }



         }*/
        if (hasInternet)
        {
            Debug.Log("Has Internet Connection");
            /*switch (instance.content)
            {
                case "pa":
                    InitializeDynamicConfig.LoadConfigFromAzure(GetPA);
                    break;
                case "family":
                    InitializeDynamicConfig.LoadConfigFromAzure(GetFamily);
                    break;

            }*/
            if (instance.pa)
            {
                InitializeDynamicConfig.LoadConfigFromAzure(GetPA);
            }
            if (instance.family)
            {
                InitializeDynamicConfig.LoadConfigFromAzure(GetFamily);
            }
        }
        else
        {
/*            switch (instance.content)
            {
                case "pa":
                    StartCoroutine(getPAFile());
                    break;
                case "family":
                    StartCoroutine(getFamilyFile());
                    break;

            }*/
            if (instance.pa)
            {
                StartCoroutine(getPAFile());
            }
            if(instance.family)
            {
                StartCoroutine(getFamilyFile());
            }
        }
    }
    IEnumerator CheckInternet()
    {
        UnityWebRequest www = new UnityWebRequest("www.google.com");
        yield return www;
        if (www.isNetworkError == false)
        {
            hasInternet = true;
        }
        else
        {
            hasInternet = false;
        }
    }
    public void Start()
    {
        /*foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }*/
        
    }
    void StartAll()
    {

    }
    public void GetFamily()
    {
        StartCoroutine(getFamilyFile());

    }
    public void GetPA()
    {
        StartCoroutine(getPAFile());

    }

    IEnumerator getFamilyFile()
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["People"]["Family"];
        string[] gameObjectString = new string[gameObjectJSON.Count];

        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            string speeches = jsonNode["People"]["Family"][i];
            Debug.Log(speeches);

            GameObject textspeech = (GameObject)Instantiate(prefab);
            textspeech.transform.SetParent(gameObject.transform, false);
            textspeech.GetComponentInChildren<TextMeshPro>().text = speeches;
        }

        yield return true;
        startnow = true;
    }


    IEnumerator getPAFile()
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["People"]["PAs"];
        string[] gameObjectString = new string[gameObjectJSON.Count];

        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            string speeches = jsonNode["People"]["PAs"][i];
            Debug.Log(speeches);

            GameObject textspeech = (GameObject)Instantiate(prefab);
            textspeech.transform.SetParent(gameObject.transform, false);
            textspeech.GetComponentInChildren<TextMeshPro>().text = speeches;
        }

        yield return true;
        startnow = true;
    }
}
