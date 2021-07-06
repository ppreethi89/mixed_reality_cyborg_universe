using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class BackToUniverse : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parentObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initializeBackUniverse()
    {
        //StartCoroutine(BackToMainUniv());
        backUnivMain();
    }

    void backUnivMain()
    {
        EventHandlerScript.ShowHeaderUniv();
        parentObj.SetActive(false);
    }

    IEnumerator BackToMainUniv()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Universe_Main", LoadSceneMode.Additive);



        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Universe_Main"));
        
    }
}
