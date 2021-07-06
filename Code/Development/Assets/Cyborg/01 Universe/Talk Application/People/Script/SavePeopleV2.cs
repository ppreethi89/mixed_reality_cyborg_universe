using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;

public class SavePeopleV2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public string filepath = "savedSpeeches.json";
    public string content;
    public bool family;
    public bool pa;
    public GameObject contentGameObjectPA;
    public GameObject contentGameObjectFamily;

    bool hasInternet;
    public static SavedPeople instance;

    private void Start()
    {
        string streamingAssetFile = CyborgFileRead_General.ReadFileStreamingAsset(filepath);
        CyborgFileRead_General.WriteFileApplicationData(filepath,streamingAssetFile);
    }
    void OnDisable()
    {
        foreach (Transform child in contentGameObjectPA.transform)
        {
            Destroy(child.gameObject);
            Debug.Log("DESTROY CHILD");
        }
        foreach (Transform child in contentGameObjectFamily.transform)
        {
            Destroy(child.gameObject);
            Debug.Log("DESTROY CHILD");
        }
    }
    void OnEnable()
    {
        StartCoroutine(CheckInternet());
        foreach (Transform child in contentGameObjectPA.transform)
        {
            Destroy(child.gameObject);
            Debug.Log("DESTROY CHILD");
        }
        foreach (Transform child in contentGameObjectFamily.transform)
        {
            Destroy(child.gameObject);
            Debug.Log("DESTROY CHILD");
        }
    }
    IEnumerator CheckInternet()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("www.google.com"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("No Internet Connection");
                StartCoroutine(getPeopleFile());
            }
            else
            {

                InitializeDynamicConfig.LoadConfigFromAzure(GetPeople);
                Debug.Log("Has Internet Connection");

            }
        }
    }
    public void GetPeople()
    {
        StartCoroutine(getPeopleFile());

    }
    IEnumerator getPeopleFile()
    {
        Debug.Log("get people local file");
        //string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        string fileName = CyborgFileRead_General.ReadFileApplicationData(filepath);
        Debug.Log(fileName);

        //string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(fileName);

        JSONNode gameObjectJSON = jsonNode["People"]["Family"];
        string[] gameObjectString = new string[gameObjectJSON.Count];

        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            string speeches = jsonNode["People"]["Family"][i];
            Debug.Log(speeches);

            GameObject textspeech = (GameObject)Instantiate(prefab);
            textspeech.transform.SetParent(contentGameObjectFamily.transform, false);
            textspeech.GetComponentInChildren<TextMeshPro>().text = speeches;
        }

        JSONNode gameObjectJSON2 = jsonNode["People"]["PAs"];
        string[] gameObjectString2 = new string[gameObjectJSON2.Count];
        for (int i = 0, j = gameObjectJSON2.Count - 1; i < gameObjectJSON2.Count; i++, j--)
        {
            string speeches = jsonNode["People"]["PAs"][i];
            Debug.Log(speeches);

            GameObject textspeech = (GameObject)Instantiate(prefab);
            textspeech.transform.SetParent(contentGameObjectPA.transform, false);
            textspeech.GetComponentInChildren<TextMeshPro>().text = speeches;
        }

        yield return true;

    }
}
