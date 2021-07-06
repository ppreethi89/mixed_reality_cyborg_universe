using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MoveTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject prefab;
    void Start()
    {
        Instantiate(prefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onDwellMove()
    {
       
        
    }

}
