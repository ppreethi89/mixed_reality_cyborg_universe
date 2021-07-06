using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class ConfirmationScript : MonoBehaviour
{
    public GameObject asset;
    public string method;
    public string action;
    public GameObject ConfirmationPanel;

    public static string assetname;
    public static string confirmationMethod;
    public static int panelview;
    public static TMP_InputField input;

    public GameObject parentRemoveControl;
    public GameObject controlclass;
    public string filepath = "config.json";

    // Start is called before the first frame update
    void Start()
    {
        controlclass = GameObject.Find("ControlClass");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Yes()
    {
        GameObject confirmed = GameObject.Find(assetname);
        Debug.Log(confirmationMethod);
        if (confirmationMethod == "Shutdown")
        {
            confirmed.GetComponent<ShutdownScript>().Yes();
        }
        else if (confirmationMethod == "MoveView")
        {
            confirmed.GetComponent<moveAppView>().moveViewApp(panelview);
            Destroy(transform.parent.gameObject);
        }
        else if (confirmationMethod == "AddSpeech")
        {
            //confirmed.GetComponent<AddSpeeches>().Add(input);
            Debug.Log(input);
            Destroy(transform.parent.gameObject);
        }
        else if (confirmationMethod == "Remove")
        {
            Debug.Log("pumasok");
            //confirmed.GetComponent<RemovingApp>().yesRemove();
            string tobeRemoveGameobject = controlclass.GetComponent<animationControl>().removeGameObject;
            StartCoroutine(DisableApplication(tobeRemoveGameobject));
            Destroy(transform.parent.gameObject);
        }
    }

    public void No()
    {
        GameObject confirmed = GameObject.Find(assetname);
        if (confirmationMethod == "MoveView")
        {
            confirmed.GetComponent<moveAppView>().NoMove();
        }
        Destroy(transform.parent.gameObject);
    }


    public void confirm()
    {
        GameObject prefab = (GameObject)Instantiate(ConfirmationPanel);
        prefab.GetComponentInChildren<TextMeshPro>().text = action;
        confirmationMethod = method;
        assetname = asset.name;
        Debug.Log(confirmationMethod);
       
        
        if (method == "MoveView")
        {
            panelview = asset.GetComponent<moveAppView>().viewNumber;
        }
        else if (method == "AddSpeech")
        {
            input = asset.GetComponent<AddSpeeches>().input;
        }
        else if (method == "Remove")
        {
            gameObject.GetComponentInParent<TileToward>().isDwell = false;
            controlclass.GetComponent<animationControl>().isChosen = false;

            string parentobj = gameObject.transform.parent.parent.name;
            controlclass.GetComponent<animationControl>().removeGameObject = gameObject.transform.parent.parent.name;
            string removingGameobject = controlclass.GetComponent<animationControl>().removeGameObject;
            prefab.GetComponentInChildren<TextMeshPro>().text = action + parentobj + "?";
        }
    }

    IEnumerator DisableApplication(string removeGameobject)
    {

        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["GameObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];


        string name1 = removeGameobject;
        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {

            if (name1 == jsonNode["GameObject"][i]["name"])
            {

                jsonNode["GameObject"][i]["status"] = "Disabled";
                Debug.Log("DisabledApp");

            }

        }
        File.WriteAllText(fileName, jsonNode.ToString());
        controlclass.GetComponent<animationControl>().removeGameObject = "";    
        GameObject toRemove = GameObject.Find(removeGameobject);
        Destroy(toRemove);
        Destroy(parentRemoveControl);
        Debug.Log("Dapat Gumana");
        yield return true;
    }
}
