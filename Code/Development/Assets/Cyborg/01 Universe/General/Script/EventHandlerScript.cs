using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Vuplex.WebView;
public class EventHandlerScript: MonoBehaviour
{
    // Start is called before the first frame update
  
    public static string activeObj;
    public static GameObject activeApp;
    public static bool isTalkAppActive;
    public static bool isPause;
    public static bool isActiveControl;
    public static bool removeModeActive;
    public static bool speakerMode;
    public GameObject statusPrefab;
    public static string avatarIPAddress;
    public static string url;
    public static event EventHandler OnDwellMinimize;
    public static event EventHandler HideOnChoose;
    public static event EventHandler DeleteModeOn;
    public static WebViewPrefab webViewPrefab;
    //Hide Header Var
    public List<GameObject> objToHide;
    public static List<GameObject> staticObjToHide;
    public static bool isCyborgProjectActive;

    //Show CyborgApp Var
    public List<GameObject> cyborgApplication;
    public static List<GameObject> staticCyborgApplication;
    public List<string> cyborgProject;
    public static List<string> staticCyborgProject;

    public GameObject universe;
    public static GameObject staticUniverse;
    public GameObject header;
    public static GameObject staticHeader;
    public GameObject createArt;
    public static GameObject staticCreateArt;
    public GameObject viewGallery;
    public static GameObject staticViewGallery;
    public GameObject mobility;
    public static GameObject staticMobility;
    public GameObject medic;
    public static GameObject staticMedic;
    public GameObject settings;
    public static GameObject staticSettings;
    [SerializeField]
    public static string staticContactNo;
    [SerializeField]
    string nonStaticContactNo;

    private void Awake()
    {
        //Debug.developerConsoleVisible = false;

        staticCreateArt = createArt;
        staticViewGallery = viewGallery;
        staticMobility = mobility;
        staticMedic = medic;
        staticHeader = header;
        staticUniverse = universe;
        staticSettings = settings;
        staticContactNo = nonStaticContactNo;

        /*foreach (GameObject obj in this.objToHide)
        {
            staticObjToHide[0]= obj;
            Debug.Log(staticObjToHide[0]);
        }*/
        /*foreach (GameObject obj in cyborgApplication)
        {
            staticCyborgApplication.Add(obj);
            Debug.Log(obj);
        }*/
    }
    
    public static void OpenCyborgProjectApp(string appName)
    {
        //isCyborgProjectActive = true;
        HideHeaderUniv();
        /*foreach (GameObject obj in staticCyborgApplication)
        {
            if (obj.ToString() == appName)
            {
                obj.SetActive(true);
            }
        }*/
        Debug.Log(appName);
        if (appName == "View Gallery")
        {
            staticViewGallery.SetActive(true);
            activeApp = staticViewGallery;
           
        }
        if (appName == "Create Art")
        {
            staticCreateArt.SetActive(true);
            activeApp = staticCreateArt;

        }
        if (appName == "Mobility")
        {
            staticMobility.SetActive(true);
            activeApp = staticMobility;
        }
        if (appName == "Medic")
        {
            staticMedic.SetActive(true);
            activeApp = staticMedic;
        }
        if (appName == "Settings")
        {
            staticSettings.SetActive(true);
            activeApp = staticSettings;
        }

    }
    public static void HideHeaderUniv()
    {
       
        staticHeader.SetActive(false);
        staticUniverse.SetActive(false);
       
    }
    public static void ShowHeaderUniv()
    {
        staticHeader.SetActive(true);
        staticUniverse.SetActive(true);
    }
    void Start()
    {
        activeObj = null;
        avatarIPAddress = "http://192.168.3.137:8004";
        Debug.developerConsoleVisible = false;

        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void peopleDeleteModeOn()
    {
        DeleteModeOn?.Invoke(this, EventArgs.Empty);
    }
    public void minimize()
    {
 
         OnDwellMinimize?.Invoke(this, EventArgs.Empty);
        
       
    }
    
    public void HideApp()
    {
        HideOnChoose?.Invoke(this, EventArgs.Empty);
    }
    
   
}
