using System.Collections;
using System.Collections.Generic;
//using System.Security.Policy;
using UnityEngine;
using System;

public class TileTest : MonoBehaviour
{
    // Start is called before the first frame update
    
    
   
    public static GameObject activeObj;
    public static bool isTalkAppActive;
    public static bool isPause;
    public static bool isActiveControl;
    public static void ClearActives()
    {
        isActiveControl = false;
        activeObj = null;
    }

    [SerializeField]
    GameObject prefab;


    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(activeObj == null)
        {
            Debug.Log("No Active object");
        }
    }
    public virtual void isDwell()
    {
        GameObject app = (GameObject)Instantiate(prefab);
    }

}
