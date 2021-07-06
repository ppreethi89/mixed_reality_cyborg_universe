using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMap : MonoBehaviour
{
    public Camera orthocam;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam.CopyFrom(orthocam);
        cam.RenderWithShader(Shader.Find("Unlit/ColorMap"), " ");
       // Camera.main.SetReplacementShader(Shader.Find("Unlit/ColorMap"), " ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
