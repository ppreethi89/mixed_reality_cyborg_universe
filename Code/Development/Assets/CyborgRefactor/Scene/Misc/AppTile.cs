using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class AppTile 
{
    // Start is called before the first frame update
    public GameObject tile;
    public GameObject controls;
    private Color32 color;
    private Vector3 position;
    public void SelectApp()
    {
        //tile.SetActive(false);
        controls.SetActive(true);
    }
    public class AppControls
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
