using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class sliderValue : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public Boolean isDwell;
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void volumelookaway()
    {
        isDwell = false;
    }
    public void volumeDwell()
    {
        isDwell = true;
    }
    public void increaseVolume()
    {
        if(isDwell)
        {
            
            slider.value = slider.value + 0.01f;
            
        }
        
    }
    public void decreaseVolume()
    {
        if (isDwell)
        {
            slider.value = slider.value - 0.01f;
        }
        
    }
}
