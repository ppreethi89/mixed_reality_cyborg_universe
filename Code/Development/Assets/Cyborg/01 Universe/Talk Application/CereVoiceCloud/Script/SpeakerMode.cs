using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeakerMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpeakerDwell()
    {
        EventHandlerScript.speakerMode = !EventHandlerScript.speakerMode;
        /*if (speakerMode)
        {
            CereVoiceAuth.SavedSpeechSpeak(pick.text);
        }*/

    }
}
