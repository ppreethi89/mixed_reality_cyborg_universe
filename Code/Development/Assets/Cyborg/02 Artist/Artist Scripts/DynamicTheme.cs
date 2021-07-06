using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using SimpleJSON;

public class DynamicTheme : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<TextMeshPro> themePrefab;

    [SerializeField]
    private static string filepath = "artistThemes.json";
    [SerializeField]
    private List<string> themeList;

    private int pageNo = 0;
    private int maxPage;

    // Start is called before the first frame update
    void Start()
    {
        string streamingAssetFile = CyborgFileRead_General.ReadFileStreamingAsset(filepath);
        CyborgFileRead_General.WriteFileApplicationData(filepath, streamingAssetFile);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        pageNo = 0;
        themeList.Clear();
        StartCoroutine(initialize());
    }

    IEnumerator initialize()
    {
        /*string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);*/

        string fileName = CyborgFileRead_General.ReadFileApplicationData(filepath);
        JSONNode jsonNode = JSON.Parse(fileName);

        JSONNode gameObjectJSON = jsonNode["Theme"];
        string[] gameObjectString = new string[gameObjectJSON.Count];
        Debug.Log(gameObjectJSON.Count);
        Debug.Log(gameObjectJSON.Count + " this is the count");

        maxPage = MaxPage(gameObjectJSON.Count);
        for (int i = gameObjectJSON.Count-1;  i > -1; i--)
        {
            string themeName = jsonNode["Theme"][i];
            Debug.Log(themeName);
            this.themeList.Add(themeName);
        }
        DisplayPerPage();

        yield return null;
    }

    public void DisplayPerPage()
    {
        for (int x = 0; x < 4; x++)
        {
            try
            {
                int currentImageNo = pageNo * 4 + x;
                themePrefab[x].text = themeList[currentImageNo];
            }
            catch {

                themePrefab[x].text = null;
            }         
        }
    }

    public int MaxPage(int count)
    {
        int maxPageLocal = count / 4;
        Debug.Log("% " + count % 4);
        if (count % 4 > 0)
        {
            maxPageLocal++;
        }
        Debug.Log("Max page is " + maxPageLocal);
        return maxPageLocal;
    }

    public void NextPage()
    {
        if (pageNo + 1 < maxPage)
        {
            Debug.Log("Switch page");
            pageNo++;
            DisplayPerPage();
        }

    }

    public void PreviousPage()
    {
        if (pageNo > 0)
        {
            pageNo--;
            DisplayPerPage();
        }
    }
}
