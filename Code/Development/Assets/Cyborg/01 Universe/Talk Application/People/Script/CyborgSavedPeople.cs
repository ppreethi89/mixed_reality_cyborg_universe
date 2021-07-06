using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;


public class CyborgSavedPeople : MonoBehaviour
{
    public string filepath = "savedSpeeches.json";
    public GameObject prefab;
    public string peopleNode;
    public GameObject content;
    // Start is called before the first frame update
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
                StartCoroutine(GetPeopleFile());
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
        StartCoroutine(GetPeopleFile());

    }

    IEnumerator GetPeopleFile()
    {
        Debug.Log("get people local file");
        //string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        string fileName = CyborgFileRead_General.ReadFileApplicationData(filepath);
        Debug.Log(fileName);

        //string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(fileName);

        JSONNode gameObjectJSON = jsonNode["People"][peopleNode];
        string[] gameObjectString = new string[gameObjectJSON.Count];

        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {
            string people = jsonNode["People"][peopleNode][i];
            Debug.Log(people);

            GameObject textPeople = (GameObject)Instantiate(prefab);
            textPeople.transform.SetParent(content.transform, false);
            textPeople.GetComponentInChildren<TextMeshProUGUI>().text = people;
            
        }        

       yield return true;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
