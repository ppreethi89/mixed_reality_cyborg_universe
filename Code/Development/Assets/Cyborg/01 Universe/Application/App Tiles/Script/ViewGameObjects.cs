using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ViewGameObjects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> view;
    public static List<GameObject> staticView;
    void Awake()
    {
        /*foreach (GameObject go in view)
        {
            staticView.Add(go);
        }*/
        staticView = view;
    }

    public static Transform FindView(string view)
    {
        foreach(GameObject go in staticView)
        {
            if(go.name == view)
            {
                Debug.Log(go.name);
                return go.transform;
            }
                        
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
