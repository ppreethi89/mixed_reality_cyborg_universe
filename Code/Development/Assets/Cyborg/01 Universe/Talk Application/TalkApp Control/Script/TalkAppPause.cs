using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TalkAppPause : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPause = false;
    public GameObject talkApp;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseTalkApp()
    {
        isPause = !isPause;
        GetAllChild(isPause);
        
    }
    void GetAllChild(bool pauseState)
    {
        Transform[] children = this.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i <children.Length; i++)
        {
            GameObject c = children[i].gameObject;
            if (c.name != "Pause_Button" && c.name != "TalkAppQuadBG")
            {
                try
                {

                    Debug.Log(c.name);
                    c.GetComponent<MeshCollider>().enabled = !pauseState;

                }
                catch
                {

                }
                try
                {

                    Debug.Log(c.name);
                    c.GetComponent<BoxCollider>().enabled = !pauseState;
                }
                catch
                {

                }
            }
            
        }

    }
}
