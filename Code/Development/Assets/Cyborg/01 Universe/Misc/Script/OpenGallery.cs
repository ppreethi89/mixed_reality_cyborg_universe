using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGallery : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void openGallery()
    {

        if (gameObject.transform.parent.parent.name == "View Gallery")
        {
            ArtistInfo.AppSelected = "ViewGallery";
            StartCoroutine(LoadYourAsyncScene());
            
        }
        else if (gameObject.transform.parent.parent.name == "Create Art")
        {          
            ArtistInfo.AppSelected = "CreateArt";
            StartCoroutine(LoadYourAsyncScene());
        }
        else if (gameObject.transform.parent.parent.name == "Mobility")
        {
            SceneManager.LoadSceneAsync("Mobility");
        }

    }

    IEnumerator LoadYourAsyncScene() {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Artist");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }

}
