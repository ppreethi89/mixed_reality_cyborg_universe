using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public class ConversationMode : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isConversationModeOn;
    void Start()
    {
        CyborgTalkApp.OnConversationMode += TurnOffCollider;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TurnOffCollider(object sender, EventArgs e)
    {
        isConversationModeOn = !isConversationModeOn;
        Debug.Log("Turn Off Collider Convo On");
        Debug.Log(isConversationModeOn);
        GetAllChild(isConversationModeOn);
    }
    void GetAllChild(bool pauseState)
    {
        Transform[] children = this.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++)
        {
            GameObject c = children[i].gameObject;
                try
                {
                    c.GetComponent<MinimizeMaximize_General>().isMinimize = pauseState;
                    //Debug.Log(c.name);
                    c.GetComponent<MeshCollider>().enabled = !pauseState;

                }
                catch
                {

                }
                try
                {

                    //Debug.Log(c.name);
                    c.GetComponent<BoxCollider>().enabled = !pauseState;
                }
                catch
                {

                }
                try
                {

                    //Debug.Log(c.name);
                    c.GetComponent<SphereCollider>().enabled = !pauseState;
                }
                catch
                {

                }
            

        }

    }
}
