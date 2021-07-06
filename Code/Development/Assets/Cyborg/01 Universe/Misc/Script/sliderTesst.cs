using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using Microsoft.MixedReality.Toolkit;

public class sliderTesst : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro label;
    public AudioSource audioSource;
    public GameObject BackGroundFill;
    public Vector3 hitpos;
    //public Camera mainCam;
    public GameObject knob;
    public float percent;
    public float timeRemaining;
    public float length;
    float x;
    float nextTime = 0;
    int interval = 1;
    void Start()
    {
        length = audioSource.clip.length;
        timeRemaining = length;
        x = (1 / length);
        Debug.Log(x);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
/*        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            
            knob.transform.position += new Vector3(x, 0, 0);
        }
*/
        if (Time.time >= nextTime && timeRemaining > 0)
        {
            timeRemaining = timeRemaining - 1;
            label.text = timeRemaining.ToString();
            knob.transform.position += new Vector3(x, 0, 0);
            //do something here every interval seconds

            nextTime += interval;
            Debug.Log(Time.time);
        }
    }
    public void pointUp()
    {
        hitpos = CoreServices.InputSystem.EyeGazeProvider.HitPosition;
        //hitpos = mainCam.GetComponent<GazeProvider>().HitPosition;
        knob.transform.position = new Vector3(hitpos.x, knob.transform.position.y, knob.transform.position.z);
        percent = (knob.transform.localPosition.x / 1);
        if (percent < 0)
        {
            percent = 0;
        }
        if (percent > 100)
        {
            percent = 100;
        }
        timeRemaining = length * percent;

        timeRemaining = length - Mathf.Round(timeRemaining);
       
       audioSource.time = timeRemaining;
    }
}
