using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isDwell = true;
    public Material skyboxMat;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void skyboxTrigger()
    {
        isDwell = !isDwell;
        if (isDwell)
        {
            RenderSettings.skybox = skyboxMat;
        }
        else
        {
            RenderSettings.skybox = (null);
        }
    }
    
}
