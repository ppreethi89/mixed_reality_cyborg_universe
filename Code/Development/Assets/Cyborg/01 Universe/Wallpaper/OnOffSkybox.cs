using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.IO;
using System;

public class OnOffSkybox : MonoBehaviour
{
    // Start is called before the first frame update
    public Boolean IsTriggered = false;
    public Material skyboxGalaxy;
    public Boolean currentState;
    public Boolean IsMinimized = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateSkybox()
    {
        // IsTriggered = !IsTriggered;
        // currentState = !currentState;

        /*if(IsTriggered == true)
        {
            RenderSettings.skybox = (null);
        }
        else
        {
            RenderSettings.skybox = skyboxGalaxy;
        }*/

        if (RenderSettings.skybox == (null))
        {
            RenderSettings.skybox = skyboxGalaxy;
            currentState = true;
        }
        else
        {
            RenderSettings.skybox = (null);
            currentState = false;
        }
    }

    public void minimizeSkybox()
    {

        IsMinimized = !IsMinimized;
        /* currentState = IsTriggered;
         IsTriggered = !IsTriggered;

         if (IsMinimized == true)
         {
             RenderSettings.skybox = (null);
         }
         else
         {
             if (currentState == true)
             {
                 RenderSettings.skybox = skyboxGalaxy;
             }
             else if (currentState == false)
             {
                 RenderSettings.skybox = (null);
             }
         }*/
        
        if (IsMinimized == true)
        {
            RenderSettings.skybox = (null);
        }
        else
        {
            currentState = RenderSettings.skybox;

            if (currentState == false)
            {
                RenderSettings.skybox = (null);

            }
            else if (currentState == true)
            {
                RenderSettings.skybox = skyboxGalaxy;
            }
        }
        
    }
}
