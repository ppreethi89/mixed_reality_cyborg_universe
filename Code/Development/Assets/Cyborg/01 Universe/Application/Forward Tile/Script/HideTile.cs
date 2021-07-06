using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HideTile : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject thisGameObject;


    void Start()
    {
        EventHandlerScript.HideOnChoose += HideApp;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        HideApp(this, EventArgs.Empty);

    }


    private void HideApp(object sender, EventArgs e)
    {
        if (gameObject.transform.name == EventHandlerScript.activeObj || EventHandlerScript.activeObj != null)
        {
            Debug.Log("Showing Showing");
            this.gameObject.SetActive(true);
        }
        
        if (gameObject.transform.name != EventHandlerScript.activeObj)
        {
            this.gameObject.SetActive(false);
        }
        
    }
}
