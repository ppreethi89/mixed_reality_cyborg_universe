using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMedicApp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabLanding;
    
    private string appName;


    void OnEnable()
    {
        Debug.Log("OnEnable called");
        appName = MedicInfo.AppSelected;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called after scene is fully loaded

    //temporary fixed for the instantiation of artist prefab, as instantiation of gameobject relies on what is the current active scene.

    //Pending: this can be fixed by: instead of instantiation use set active on both prefabs, to be discussed.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Medic"));
        openApp();



    }
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openApp() {

       // Instantiate(prefabLanding);
        prefabLanding.SetActive(true);


    }

}
