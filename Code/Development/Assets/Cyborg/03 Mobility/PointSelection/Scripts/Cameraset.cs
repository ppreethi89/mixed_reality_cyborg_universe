using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.SetReplacementShader(Shader.Find("Mapshader"), "RenderType" );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
