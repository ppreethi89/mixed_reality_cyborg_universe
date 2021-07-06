using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject app;
    public Action action;
    public GameObject parent;
    public static void SavePos(GameObject obj)
    {

    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void isDwell()
    {
        action();
        Destroy(parent);
        
    }
    public void Move()
    {
        app.transform.position += new Vector3(10f, 5f, 1f);
        TileTest.activeObj = null;
        Destroy(parent);
    }
}

