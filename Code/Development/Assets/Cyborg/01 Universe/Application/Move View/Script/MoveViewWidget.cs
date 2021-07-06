using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MoveViewWidget : MonoBehaviour
{
        // Start is called before the first frame update
    public int viewNumber;
    public string filepath = "config.json";
    public GameObject appTile;
    private float view;
    void Start()
    {
        view = appTile.GetComponent<AppDetails>().view; //check for widget details
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showMoveViewAppPrefab()
    {

        ShowMoveViewAppPrefab.showMoveViewPrefab(appTile.name, moveViewApp);
        EventHandlerScript.activeObj = null;
        appTile.GetComponent<ForwardTile>().thisAppState = AppState.InActive;
    }

    public void moveViewApp()
    {
        Transform parent = GameObject.Find("View" + ShowMoveViewAppPrefab.view).transform;
        appTile.transform.SetParent(parent, false);
        StartCoroutine(SaveView());
        ShowMoveViewAppPrefab.staticMoveViewPrefab.SetActive(false); //include this in the widegt components ps. found in the apptile

    }

    IEnumerator SaveView()
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["WidgetObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];


        string name1 = appTile.transform.name;
        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {

            if (name1 == jsonNode["WidgetObject"][i]["name"])
            {

                GameObject obj = GameObject.Find(jsonNode["WidgetObject"][i]["name"]);
                jsonNode["WidgetObject"][i]["view"] = ShowMoveViewAppPrefab.view;

                Debug.Log("ave View");

            }

        }
        File.WriteAllText(fileName, jsonNode.ToString());


        yield return true;
    }

}
