using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Action action;
    [SerializeField]
    GameObject parent;
    //public string detail;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void yes()
    {
        action();
        Destroy(parent);
    }
    public void no()
    {
        Destroy(parent);
    }
}
