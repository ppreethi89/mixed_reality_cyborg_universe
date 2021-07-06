using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;


public class SliderEyeTest : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Slider slider;
    

    public void Gaze()
    {
        Vector3 hitpos = CoreServices.InputSystem.EyeGazeProvider.HitPosition;
        PointerEventData eve = new PointerEventData(EventSystem.current);
        eve.position = hitpos;

        slider.OnPointerDown(eve);
        
    }
}
