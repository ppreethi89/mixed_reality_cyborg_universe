using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class bgMusicVolume : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeBGMusicVolume()
    {
        AudioListener.volume = slider.value;
    }
}
