using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Web;
using UnityEngine.Networking;
using System.IO;
using SimpleJSON;
using System;

public class ConfirmTheme : MonoBehaviour
{
    // Start is called before the first frame update
    private string compositionImageBaseUrl = "https://cyborgartist.azurewebsites.net/get_potential_composition_images?theme=";
    [SerializeField]
    private GameObject confirmedTheme;

    [SerializeField]
    private static string filepath = "artistThemes.json";

    void OnEnable()
    {
        ArtistInfo.CompositionImagesNames.Clear();
        ArtistInfo.CompositionImagesTextures.Clear();
        ArtistInfo.CompositionImageSelected = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void confirm() {

        //Theme was entered
        if (confirmedTheme.GetComponent<TMP_InputField>().text.Length != 0)
        {
            string enteredTheme = confirmedTheme.GetComponent<TMP_InputField>().text;
            ArtistInfo.ThemeSelected = enteredTheme;
            StartCoroutine(SaveTransform(enteredTheme));
            string themeQueryParams = UnityWebRequest.EscapeURL(enteredTheme);
            string url = compositionImageBaseUrl + themeQueryParams;
            ArtistInfo.URL = url;
            //Added function
            GetImageAzureBlob.ThemeSelected();
            confirmedTheme.GetComponent<TMP_InputField>().text = null;
            gameObject.GetComponent<SetActiveObject_General>().setActiveTransition();
        }
        //No theme was entered
        else {
            AlertBox_General.ShowAlertBox("No theme was entered. Please enter a theme");        
        }       
    }

    public static IEnumerator SaveTransform(string themeName)
    {
        int themeLimit = 10;
        Debug.Log("run save transformation");
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);
        JSONNode gameObjectJSON = jsonNode["Theme"];

        int checker = CheckDuplicates(jsonNode["Theme"], themeName);
        if (checker > -1)
        {
            Debug.Log("duplicate theme addition");
            jsonNode["Theme"].Remove(checker);
            jsonNode["Theme"][gameObjectJSON.Count] = themeName;
        }
        else if (gameObjectJSON.Count >= themeLimit)
        {
            Debug.Log("reached limit theme addition");
            jsonNode["Theme"].Remove(0);
            jsonNode["Theme"][themeLimit] = themeName;
        }
        else
        {
            Debug.Log("normal theme addition");
            jsonNode["Theme"][gameObjectJSON.Count] = themeName;
        }

        File.WriteAllText(fileName, jsonNode.ToString());
        yield return true;
    }

    private static int CheckDuplicates(JSONNode jnode, string themeName)
    {
        int index = -1;
        for (int i = 0; i<jnode.Count; i++)
        {
            Debug.Log(jnode[i]);
            if (jnode[i] == themeName)
            {
                index = i;
            }
        }
        return index;
    }
}
