using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider2 : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

// function that updates camera size 
    public void slideCameraSize(float size)
    {
       // Debug.Log(size);
        cam.orthographicSize = size;
    }
    // Function to implement Zoom In
    public void Zoomin()
    {
        float size = cam.orthographicSize;
        Debug.Log("ZOOMIN");
        if (size > 2.5F)
        {
            size -= 0.5F;
            cam.orthographicSize = size;
        }
    }

    //Function to implement Zoom Out 
    public void Zoomout()
    {
        float size = cam.orthographicSize;
        Debug.Log("ZoomOUT");
        if (size < 6.0F)
        {
            size += 0.5F;
            cam.orthographicSize = size;
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
