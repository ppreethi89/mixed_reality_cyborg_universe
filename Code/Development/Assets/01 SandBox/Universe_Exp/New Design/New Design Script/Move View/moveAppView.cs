using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class moveAppView : MonoBehaviour
{
    // Start is called before the first frame update
    //public static int panelnum;
    public int viewNumber;
    public string filepath = "config.json";
    //public GameObject ConfirmationPanel;
    int view;
    //public GameObject ConfirmBox;
    public bool confirmed = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void moveViewApp(int panelnum)
    {
        string appname = GameObject.Find("ControlClass").GetComponent<animationControl>().movingViewGameObject;
        GameObject tobeMoveGameObject = GameObject.Find(appname);
        Transform parent = GameObject.Find("View" + panelnum).transform;
        tobeMoveGameObject.transform.SetParent(parent, false);
        StartCoroutine(SaveView());
        Destroy(transform.parent.gameObject);
    }
    IEnumerator SaveView()
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["GameObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];


        string name1 = GameObject.Find("ControlClass").GetComponent<animationControl>().movingViewGameObject;
        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {

            if (name1 == jsonNode["GameObject"][i]["name"])
            {

                GameObject obj = GameObject.Find(jsonNode["GameObject"][i]["name"]);
                jsonNode["GameObject"][i]["view"] = viewNumber;

                Debug.Log("Save View");

            }

        }
        File.WriteAllText(fileName, jsonNode.ToString());
        GameObject.Find("ControlClass").GetComponent<animationControl>().movingViewGameObject = "";

        yield return true;
    }

    /*public void confirmation()
    {
        panelnum = viewNumber;
        view = viewNumber + 1;
        GameObject prefab = (GameObject)Instantiate(ConfirmationPanel);
        prefab.GetComponentInChildren<TextMeshPro>().text = "Are you sure you want to move this app to view " + view + " ?";
        Destroy(transform.parent.gameObject);
    }*/

    public void NoMove()
    {
        Destroy(transform.parent.gameObject);
    }

}