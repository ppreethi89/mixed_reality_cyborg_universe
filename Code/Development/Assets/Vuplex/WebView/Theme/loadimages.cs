using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadimages : MonoBehaviour
{

    public void PlayGame()
    {
        Debug.Log("triggered");
        SceneManager.LoadScene("relatedImages");
       
    }

    public void Goback()
    {
        Debug.Log("triggered");
        SceneManager.LoadScene("theme");

    }
}