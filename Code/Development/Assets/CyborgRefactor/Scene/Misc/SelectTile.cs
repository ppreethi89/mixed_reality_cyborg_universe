using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SelectTile : MonoBehaviour
{
    // Start is called before the first frame update
    AppTile app;
    public GameObject controls;
    
    void Start()
    {
        app = new AppTile();
        app.tile = this.gameObject;
        app.controls = controls;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DwellTile()
    {
        app.SelectApp();
    }

}
