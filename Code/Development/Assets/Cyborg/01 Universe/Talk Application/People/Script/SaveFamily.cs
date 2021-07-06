using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;
public class SaveFamily : MonoBehaviour
{
    // Start is called before the first frame update

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
    void OnDisable()
    {
        foreach (Transform child in contentGameObject.transform)
        {
            Destroy(child.gameObject);
            Debug.Log("DESTROY CHILD");
        }
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


            InitializeDynamicConfig.LoadConfigFromAzure(GetFamily);

        }
        else
        {
            StartCoroutine(getFamilyFile());

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
    public void GetFamily()
    {
        StartCoroutine(getFamilyFile());

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
            textspeech.transform.SetParent(contentGameObject.transform, false);
            textspeech.GetComponentInChildren<TextMeshPro>().text = speeches;
        }

        yield return true;

    }
}
