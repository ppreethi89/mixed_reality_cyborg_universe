using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Vuplex.WebView.Demos;
public class OpenApp_General : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject appTile;
    string appDomain;
    public GameObject vuplexBrowser;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openApplication()
    {
        EventHandlerScript.activeObj = null;
        
        appTile.GetComponent<ForwardTile>().thisAppState = AppState.InActive;
        switch (appTile.transform.tag)
        {
            case "Built-in":
                switch (appTile.transform.name)
                {
                    case "Create Art":
                        appDomain = "Create Art";
                        ArtistInfo.AppSelected = appTile.transform.name;
                        Debug.Log(ArtistInfo.AppSelected);
                        EventHandlerScript.OpenCyborgProjectApp(appDomain);
                        //ArtistInfo.SavedPotentialCompositionImages = false;
                        //StartCoroutine(openScene(appDomain));
                        break;
                    case "View Gallery":
                        appDomain = "View Gallery";
                        ArtistInfo.AppSelected = appTile.transform.name;
                        Debug.Log(ArtistInfo.AppSelected);
                        EventHandlerScript.OpenCyborgProjectApp(appDomain);
                        //ArtistInfo.SavedPotentialCompositionImages = false;
                        //StartCoroutine(openScene(appDomain));
                        break;
                    case "Mobility":
                        appDomain = "Mobility";
                        EventHandlerScript.OpenCyborgProjectApp(appDomain);
                        //StartCoroutine(openScene(appDomain));
                        break;
                    case "Medic":
                        appDomain = "Medic";
                        EventHandlerScript.OpenCyborgProjectApp(appDomain);
                        //StartCoroutine(openScene(appDomain));
                        break;
                    case "Settings":
                        appDomain = "Settings";
                        EventHandlerScript.OpenCyborgProjectApp(appDomain);
                        //StartCoroutine(openScene(appDomain));
                        break;
                }
                
                break;
            case "Web-App":
                //For Vuplex Browser
                string url = appTile.GetComponent<AppDetails>().url;
                EventHandlerScript.url = url;
                Debug.Log("The url is " + url);
                GameObject universe = GameObject.Find("Universe");
                GameObject point = GameObject.Find("Main Camera");
                //GameObject vuplex = (GameObject)Instantiate(vuplexBrowser, appTile.transform.parent.transform, true);
                GameObject vuplex = (GameObject)Instantiate(vuplexBrowser, point.transform, true);

                //vuplex.GetComponent<AdvancedWebViewDemo>().url = url;
                break;
        }
    }

    IEnumerator openScene(string appScene)
    {
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(appScene, LoadSceneMode.Additive);

        

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;

        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(appScene));
        Debug.Log("Active Scene (openapp) " + appScene);
        SceneManager.UnloadSceneAsync("Universe_Main");

    }
}
