using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;


public class CyborgSavedSpeech : MonoBehaviour
{
    // Start is called before the first frame update
    public string filepath = "savedSpeeches.json";
    public GameObject prefab;
    //public string speechNode;
    public GameObject content;
    private void Start()
    {
        string streamingAssetFile = CyborgFileRead_General.ReadFileStreamingAsset(filepath);
        CyborgFileRead_General.WriteFileApplicationData(filepath, streamingAssetFile);
    }
    private void OnEnable()
    {
        StartCoroutine(CheckInternet());

        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
            Debug.Log("DESTROY CHILD");
        }
    }
    private void OnDisable()
    {
        foreach (Transform child in content.transform)
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
                StartCoroutine(GetSpeechFile());
            }
            else
            {

                InitializeDynamicConfig.LoadConfigFromAzure(GetSpeech);
                Debug.Log("Has Internet Connection");

            }
        }
    }
    public void GetSpeech()
    {
        StartCoroutine(GetSpeechFile());

    }

    IEnumerator GetSpeechFile()
    {
        Debug.Log("get speech local file");
        //string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        string fileName = CyborgFileRead_General.ReadFileApplicationData(filepath);
        Debug.Log(fileName);

        //string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(fileName);

        JSONNode gameObjectJSON = jsonNode["Speeches"];
        string[] gameObjectString = new string[gameObjectJSON.Count];

        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            string speech = jsonNode["Speeches"][i];
            Debug.Log(speech);

            GameObject textSpeech = (GameObject)Instantiate(prefab);
            textSpeech.transform.SetParent(content.transform, false);
            textSpeech.GetComponentInChildren<TextMeshProUGUI>().text = speech;

        }

        yield return true;

    }
}
