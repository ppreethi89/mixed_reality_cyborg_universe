using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.IO;
using SimpleJSON;
using Microsoft.MixedReality.Toolkit.Input;

public class CyborgTalkApp : MonoBehaviour
{
    // Start is called before the first frame update
    static public CyborgTalkApp instance;
    public static GameObject talkApplication;
    public static bool isActive;
    public static TMP_InputField activeInputField;

    public static event EventHandler OnConversationMode;
    public static bool isListen;
    [SerializeField] 
    GameObject listenButton;

    [SerializeField]
    GameObject nonStaticTalkApp;
    [SerializeField]
    TMP_InputField talkAppInput;
    [SerializeField]
    private string filepath = "config.json";
    Transform details;
    public Vector3 fullScreenScale;
    public Vector3 fullScreenPos;
    Vector3 originScale;
    Vector3 originPos;

    [SerializeField]
    GameObject listenModeTextField;

    [SerializeField]
    GameObject minimizeButton;
    [SerializeField]
    GameObject pauseButton;

    Animator animator;
    public bool convoOn = false;
    Material skyboxGalaxy;


    public static TMP_InputField staticTalkAppInputField;
    public TMP_InputField TalkAppInputField;
    private void Awake()
    {
        instance = this;
        instance.details = this.details;
    }
    public void OnEnable()
    {
        try
        {
            instance.talkAppInput.ActivateInputField();
            //activeInputField.ActivateInputField();
        }
        catch
        {
            
        }
    }
        

    void Start()
    {
        staticTalkAppInputField = TalkAppInputField;
        talkApplication = nonStaticTalkApp;
        talkApplication.SetActive(false);
        originScale = instance.nonStaticTalkApp.transform.localScale;
        originPos = instance.nonStaticTalkApp.transform.localPosition;
        animator = gameObject.GetComponent<Animator>();
        skyboxGalaxy = RenderSettings.skybox;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void callTalkApp(TMP_InputField tmpInputField)
    {
        talkApplication.SetActive(true);
        isActive = true;
        activeInputField = tmpInputField;
        
       /* try
        {
            activeInputField.ActivateInputField();
            instance.talkAppInput.ActivateInputField();
        }
        catch
        {

        }*/
       
    }
    public void hideShowTalkApp()
    {
        isActive = !isActive;

        if (!isActive)
        {
            talkApplication.SetActive(false);
        }
        else
        {
            talkApplication.SetActive(true);
            instance.talkAppInput.ActivateInputField();
            /*Transform talkAppTransform = SetTalkApp(talkApplication);
            talkApplication.transform.position = talkAppTransform.position;
            talkApplication.transform.rotation = talkAppTransform.rotation;
            talkAppTransform.transform.localScale = talkAppTransform.localScale;*/
        }
    }
    public void submitText()
    {
        if (EventHandlerScript.webViewPrefab != null)
        {
            //EventHandlerScript.webViewPrefab.WebView.HandleKeyboardInput(talkAppInput.text);

            for (int i =0; i < talkAppInput.text.Length; i++)
            {
                EventHandlerScript.webViewPrefab.WebView.HandleKeyboardInput(talkAppInput.text[i].ToString()); 
            }
            isActive = false;
            talkApplication.SetActive(false);
        }
        else
        {
            activeInputField.text = talkAppInput.text;
            isActive = false;
            talkApplication.SetActive(false);
        }
        
        

    }
    Transform SetTalkApp(GameObject app)
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);

        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        JSONNode gameObjectJSON = jsonNode["GameObject"];
        string[] gameObjectString = new string[gameObjectJSON.Count];

       
        for (int i = 0, j = gameObjectJSON.Count - 1; i < gameObjectJSON.Count; i++, j--)
        {

            if (app.transform.name == jsonNode["GameObject"][i]["name"])
            {
                float posX = jsonNode["GameObject"][i]["position"]["posX"];
                float posY = jsonNode["GameObject"][i]["position"]["posY"];
                float posZ = jsonNode["GameObject"][i]["position"]["posZ"];
                string name = jsonNode["GameObject"][i]["name"];

                float rotX = jsonNode["GameObject"][i]["rotation"]["rotX"];
                float rotY = jsonNode["GameObject"][i]["rotation"]["rotY"];
                float rotZ = jsonNode["GameObject"][i]["rotation"]["rotZ"];

                float scaleX = jsonNode["GameObject"][i]["scale"]["scaleX"];
                float scaleY = jsonNode["GameObject"][i]["scale"]["scaleY"];
                float scaleZ = jsonNode["GameObject"][i]["scale"]["scaleZ"];



                Vector3 position = new Vector3(posX, posY, posZ);
                Quaternion rotation = Quaternion.Euler(rotX, rotY, rotZ);
                Vector3 scale = new Vector3(scaleX, scaleY, scaleZ);

                details.position = position;
                details.rotation = rotation;
                details.localScale = scale;
                Debug.Log(details);

                Debug.Log("Save Success");
                
               


               

                //yield return appDetails;

                //GameObject app = (GameObject)Instantiate(prefab, position, rotation, view);


            }

        }
        Debug.Log(instance.details);
        return instance.details;


    }

    public void ConversationMode()
    {
        OnConversationMode?.Invoke(this, EventArgs.Empty);
        isListen = !isListen;
        animator.SetBool("ConvoOn", isListen);
        if (isListen)
        {
            minimizeButton.SetActive(false);
            pauseButton.SetActive(false);
            RenderSettings.skybox = (null);
            listenButton.GetComponent<DictationHandler>().StartRecording();
            Debug.Log("Start record");
            Debug.Log(instance.nonStaticTalkApp.transform.localScale);
            instance.nonStaticTalkApp.transform.localScale = fullScreenScale;
            instance.nonStaticTalkApp.transform.localPosition = fullScreenPos;
            listenModeTextField.SetActive(true);

        }
        else
        {
            minimizeButton.SetActive(true);
            pauseButton.SetActive(true);
            RenderSettings.skybox = skyboxGalaxy;
            listenButton.GetComponent<DictationHandler>().StopRecording();
            instance.nonStaticTalkApp.transform.localScale = originScale;
            instance.nonStaticTalkApp.transform.localPosition = originPos;
            listenModeTextField.SetActive(false);
        }
    }
}
