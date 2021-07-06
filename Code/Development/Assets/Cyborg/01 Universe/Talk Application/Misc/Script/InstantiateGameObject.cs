using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using Microsoft.MixedReality.Toolkit.Input;
using SimpleJSON;
using TMPro;


public class InstantiateGameObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public Boolean IsTriggered = false;
    public string filepath = "talkappConfig.json";



    public GameObject talkicon;
    public GameObject keyboardv2; //inputfield
    public TMP_InputField talkappinput; // talkapp
    public GameObject inputfield; //inputfield
    public GameObject parent;
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator InstantiateObject()
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        //Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);

        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["GameObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];
        Debug.Log(gameObjectString);

        if (GameObject.Find("ControlClass").GetComponent<animationControl>().IsOnTalkApp == false)
        {
            //GameObject app = (GameObject)Instantiate(prefab);
            GameObject.Find("ControlClass").GetComponent<animationControl>().IsOnTalkApp = true;
            //app.transform.SetParent(gameObject.transform.parent, false);

            for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
            {
                float posX = jsonNode["GameObject"][i]["position"]["posX"];
                float posY = jsonNode["GameObject"][i]["position"]["posY"];
                float posZ = jsonNode["GameObject"][i]["position"]["posZ"];

                float rotX = jsonNode["GameObject"][i]["rotation"]["rotX"];
                float rotY = jsonNode["GameObject"][i]["rotation"]["rotY"];
                float rotZ = jsonNode["GameObject"][i]["rotation"]["rotZ"];

                float scaleX = jsonNode["GameObject"][i]["scale"]["scaleX"];
                float scaleY = jsonNode["GameObject"][i]["scale"]["scaleY"];
                float scaleZ = jsonNode["GameObject"][i]["scale"]["scaleZ"];

                float viewNo = jsonNode["GameObject"][i]["view"];
                string name = jsonNode["GameObject"][i]["name"];
                string viewString = "View" + viewNo;

                //Transform view = GameObject.Find(viewString).transform;
                Vector3 position = new Vector3(posX, posY, posZ);
                Quaternion rotation = Quaternion.Euler(rotX, rotY, rotZ);


                //GameObject app = (GameObject)Instantiate(prefab, position, rotation, view);
                GameObject app = (GameObject)Instantiate(prefab);
                app.transform.SetParent(gameObject.transform.parent, false);
                app.tag = tag;
                app.transform.localPosition = position;
                app.transform.localRotation = rotation;
                app.transform.name = name;
                app.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                Transform getChild = app.transform.GetChild(0).GetChild(0);
                SpriteRenderer spIcon = getChild.GetComponent<SpriteRenderer>();
                //Transform getChild2 = app.transform.GetChild(0).GetChild(1);


                //app.GetComponent<InstantiateGameObject>().inputfield = this.gameObject;

            }


        }

        yield return true;

    }

    public void dwellInput()
    {
        //GameObject.Find("TalkAppUpdated(Clone)").GetComponent<InstantiateGameObject>().inputfield = this.gameObject;
        GameObject.Find("TalkAppUpdated(Clone)").GetComponent<Keyboardv2>().inputfield = this.gameObject;
    }
    public void exitTalkApp()
    {
        GameObject.Find("ControlClass").GetComponent<animationControl>().IsOnTalkApp = false;
        Destroy(GameObject.Find("TalkAppUpdated(Clone)"));
        Debug.Log(GameObject.Find("ControlClass").GetComponent<animationControl>().IsOnTalkApp);
    }
    public void submit()
    {

        inputfield.GetComponent<TMP_InputField>().text = talkappinput.text;
        GameObject.Find("ControlClass").GetComponent<animationControl>().IsOnTalkApp = false;
        exitTalkApp();
    }

    public void OnOffTalkApp()
    {
        IsTriggered = !IsTriggered;

        if (IsTriggered == true)
        {
            GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = true;
            //InstantiateObject();
            StartCoroutine(InstantiateObject());
            gameObject.GetComponent<EyeTrackingTarget>().dwellTimeInSec = 2.0f;
        }
        else
        {
            GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = false;
            exitTalkApp();

            gameObject.GetComponent<EyeTrackingTarget>().dwellTimeInSec = 0.8f;
        }
    }
}