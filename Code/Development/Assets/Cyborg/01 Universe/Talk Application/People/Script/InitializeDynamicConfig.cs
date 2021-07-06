using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using UnityEngine.Networking;
using RESTClient;
using Azure.StorageServices;


public class InitializeDynamicConfig : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Azure Storage Service")]

    // Reach out to admin for the 3 credentials below
    [SerializeField]
    private string storageAccount;
    [SerializeField]
    private string accessKey;
    [SerializeField]
    public string container;


    public string filepath = "savedSpeeches.json";


    private StorageServiceClient client;
    private BlobService blobService;
    private Text label;
    private List<Blob> items;
    public static InitializeDynamicConfig instance;

    public static bool done;
    public Action action;
    public Action action2;
    private void Awake()
    {
        
        instance = this;
        instance.action = action;
        instance.action2 = action2;
    }
    void Start()
    {
        if (string.IsNullOrEmpty(storageAccount) || string.IsNullOrEmpty(accessKey))
        {
            Log.Text(label, "Storage account and access key are required", "Enter storage account and access key in Unity Editor", Log.Level.Error);
        }

        client = StorageServiceClient.Create(storageAccount, accessKey);
        blobService = client.GetBlobService();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void SaveConfigToAzure()
    {
        instance.TappedSaveText();
        //instance.action = passedAction;

    }

    public static void LoadConfigFromAzure(Action passedAction)
    {
        instance.DownloadTextFile();
        instance.action = passedAction;
        

    }
    public static void LoadConfigFromAzure2(Action passedAction)
    {
        instance.DownloadTextFile();
        instance.action2 = passedAction;
    }
    static void LoadPassedAction()
    {
        instance.action();
        //instance.action2();
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(5f);
        done = false;
    }


    //Saving from local to cloud
    public void TappedSaveText()
    {
        //ChangeLabelText("Saving...");
        //string text = !string.IsNullOrEmpty(inputField.text) ? inputField.text : "hello";
        //string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        //string jsonString = File.ReadAllText(fileName);
        string jsonString = CyborgFileRead_General.ReadFileApplicationData(filepath);
        StartCoroutine(blobService.PutTextBlob(PutTextBlobComplete, jsonString, container, filepath));
    }

    private void PutTextBlobComplete(RestResponse response)
    {
        if (response.IsError)
        {
            //Log.Text(label, response.ErrorMessage + " Error putting blob:" + response.Content);
            Debug.Log(response.ErrorMessage + "and" + response.Content);
            return;
        }
        //Log.Text(label, "Put blob status:" + response.StatusCode);
        Debug.Log(response.StatusCode);

        InitializeDynamicConfig.done = true;
        StartCoroutine(Reset());
    }


    //Fetching from cloud to Local
    public void DownloadTextFile()
    {
        client = StorageServiceClient.Create(storageAccount, accessKey);
        blobService = client.GetBlobService();



        items = new List<Blob>();

        string resourcePath = container + "/savedSpeeches.json";
        StartCoroutine(blobService.GetTextBlob(GetTextBlobComplete, resourcePath));
    }
    private void GetTextBlobComplete(RestResponse response)
    {
        if (response.IsError)
        {
            Debug.Log("Error: " + response.IsError);



            return;
        }
        Debug.Log(response.Content);


        CyborgFileRead_General.WriteFileApplicationData(filepath, response.Content);
        //string fileName = Path.Combine(Application.streamingAssetsPath, "savedSpeeches.json");

        //File.WriteAllText(fileName, response.Content);
        //SavedSpeeches.StartReadingFile();
        LoadPassedAction();
        InitializeDynamicConfig.done = true;
        StartCoroutine(Reset());
    }
}
