using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenApp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject vuplex;
    void Start()
    {
        Instantiate(vuplex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
