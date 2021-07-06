using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class AddPeople : MonoBehaviour
{
    public static string category;
    public TMP_InputField input;
    public string filepath = "savedSpeeches.json";
    public GameObject confirmationContainer;

    [SerializeField]
    GameObject familyPrefab;
    [SerializeField]
    GameObject paPrefab;
    [SerializeField]
    GameObject personNamePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void Family()
    {
        category = "Family";
        StartCoroutine(Save(input));
        GameObject family = (GameObject)Instantiate(personNamePrefab);
        family.transform.SetParent(familyPrefab.transform, false);
        family.GetComponentInChildren<TextMeshPro>().text = input.text;
        Debug.Log("Speech Saved");
        gameObject.SetActive(false);

    }
    public void PAs()
    {
        category = "PAs";
        StartCoroutine(Save(input));
        GameObject pa = (GameObject)Instantiate(personNamePrefab);
        pa.transform.SetParent(paPrefab.transform, false);
        pa.GetComponentInChildren<TextMeshPro>().text = input.text;
        Debug.Log("Speech Saved");
        gameObject.SetActive(false);
    }

    public IEnumerator Save(TMP_InputField input)
    {
        Debug.Log(input.text);
        //string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        //Debug.Log(fileName);

        //string jsonString = File.ReadAllText(fileName);
        string jsonString = CyborgFileRead_General.ReadFileApplicationData(filepath);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["People"][category];
        jsonNode["People"][category][gameObjectJSON.Count] = input.text;

        //File.WriteAllText(fileName, jsonNode.ToString());
        CyborgFileRead_General.WriteFileApplicationData(filepath, jsonNode.ToString());
        InitializeDynamicConfig.SaveConfigToAzure();
        yield return true;
    }
}
