using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Postprocessing : MonoBehaviour
{
    // public Texture asource;
    // public RenderTexture rresult;
     public Material mat;

    // Start is called before the first frame update
    void Awake()
    {
        // Camera.main.SetReplacementShader(Shader.Find("Unlit/ColorMap"), "Opaque");
        mat = new Material(Shader.Find("Unlit/Voronoi_shader"));

    }



    // Update is called once per frame
    void Update()
    {
       // Graphics.Blit(asource, rresult);
    }


     void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
    private void OnEnable()
    {
        
    }
}
