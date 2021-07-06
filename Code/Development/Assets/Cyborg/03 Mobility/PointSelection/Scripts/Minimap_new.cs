using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_new : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Material _material;
   // [Range(0, 1)]
  //  public float distance = 0f;

    


    void Awake()
    {
        // dynamically create a material that will use our shader
       // _material = new Material(Shader.Find("Custom/Minimap_shader"));
        // tell the camera to render depth and normals

       // GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
    }

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
       // Debug.Log(newPosition.y);
        
    }
    void OnRenderImage(RenderTexture src, RenderTexture dest)

    {
        //Vector3 newPosition = player.position;
       // _material.SetVector("_orthographicCamera", newPosition);
        //Debug.Log(newPosition);
        Graphics.Blit(src, dest, _material);

    }

        void Start()
    {
       // _material = new Material(Shader.Find("Custom/Minimap_shader"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
