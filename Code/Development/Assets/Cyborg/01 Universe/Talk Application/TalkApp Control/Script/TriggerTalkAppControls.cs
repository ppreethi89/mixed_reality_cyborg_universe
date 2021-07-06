using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TriggerTalkAppControls : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> nonStatictalkAppControls;
    public GameObject alphaKeyboard;
    public GameObject numericalKeyboard;

    public static List<GameObject> staticTalkAppControls;
    public static GameObject staticAlphaKeyboard;
    public static GameObject staticNumericalKeyboard;
    void Start()
    {
        /* foreach(GameObject gos in nonStatictalkAppControls)
         {
             staticTalkAppControls.Add(gos);
         }*/
        staticAlphaKeyboard = alphaKeyboard;
        staticTalkAppControls = nonStatictalkAppControls;
        staticNumericalKeyboard = numericalKeyboard;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void activateControl(GameObject talkAppControls)
    {
        foreach(GameObject control in staticTalkAppControls)
        {
           
            if(control != talkAppControls)
            {
                control.SetActive(false);
                
            }
            else
            {
                control.SetActive(true);
                
            }
        }
    }
}
