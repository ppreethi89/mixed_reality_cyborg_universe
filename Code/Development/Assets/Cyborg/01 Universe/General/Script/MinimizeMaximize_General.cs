using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MinimizeMaximize_General : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject point;

    float speed = 100f;
    public bool isMinimize = false;
    Vector3 origin;
    void Start()
    {
        EventHandlerScript.OnDwellMinimize += TestingMinimizeSubscriber;
        origin = gameObject.transform.localPosition;
        point = GameObject.Find("MinimizePoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (isMinimize)
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(transform.localPosition, point.transform.localPosition, Time.deltaTime * speed); 
        }
        else
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(transform.localPosition, origin, Time.deltaTime * speed);
            
        }
    }
    private void TestingMinimizeSubscriber(object sender, EventArgs e)
    {
        isMinimize = !isMinimize;
    }
}
