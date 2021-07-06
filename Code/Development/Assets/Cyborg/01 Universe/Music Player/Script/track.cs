using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class track : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;
    private UnityEvent onProgressComplete;
 
    void Start () {
        // Initialize onProgressComplete and set a basic callback
        if (onProgressComplete == null)
            onProgressComplete = new UnityEvent();
        onProgressComplete.AddListener(OnProgressComplete);
    }
	
    void Update () {
        try
        {
            slider.maxValue = audioSource.clip.length;
            slider.value = audioSource.time;
        }
        catch
        {
            Debug.Log("Music is not yet ready");
        }
        
    }

    // The method to call when the progress bar fills up
    void OnProgressComplete() {
        Debug.Log("Progress Complete");
    }

    public void progress()
    {
        audioSource.time = slider.value;
    }
    

    /*public void progress()
    {
      //  slider.value = song.clip.length;
     
      // AsyncOperation asyncOperation = SceneManager.;
           slider.gameObject.SetActive(true);
     slider.value = (float)(audioSource.time / audioSource.clip.length);
      audioSource.time = slider.value;
    }*/
}
