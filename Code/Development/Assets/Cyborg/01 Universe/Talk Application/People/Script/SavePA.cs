using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;

public class SavePA : MonoBehaviour
{
    public GameObject prefab;
    public string filepath = "savedSpeeches.json";
    public string content;
    public bool family;
    public bool pa;
    public GameObject contentGameObject;

    public static SavedPeople instance;
    bool hasInternet;

    private void Awake()
    {


    }
    private void Start()
    {
        
    }
    void OnEnable()
    {
        StartCoroutine(CheckInternet());
        foreach (Transform child in contentGameObject.transform)
        {
            Destroy(child.gameObject);
            Debug.Log("DESTROY CHILD");
        }
        if (hasInternet)
        {
            Debug.Log("Has Internet Connection");


            InitializeDynamicConfig.LoadConfigFromAzure2(GetPA);
            
        }
        else
        {
            StartCoroutine(getPAFile());

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

    public void GetPA()
    {
        StartCoroutine(getPAFile());

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
            textspeech.transform.SetParent(contentGameObject.transform, false);
            textspeech.GetComponentInChildren<TextMeshPro>().text = speeches;
        }

        yield return true;
    }
}
