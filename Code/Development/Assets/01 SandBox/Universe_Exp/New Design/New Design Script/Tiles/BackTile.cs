﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void backApplication()
    {
        gameObject.transform.parent.parent.GetComponent<TileToward>().isDwell = false;
        GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = false;
    }
}
