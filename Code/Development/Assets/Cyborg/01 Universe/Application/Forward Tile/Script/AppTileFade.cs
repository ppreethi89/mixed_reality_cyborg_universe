using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AppTileFade : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tile;
    public bool isNotChosen;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventHandlerScript.activeObj == null)
        {
            this.gameObject.GetComponent<Animator>().SetBool("isNotChosen", false);
        }
        if (EventHandlerScript.activeObj == this.gameObject.transform.name)
        {
            this.gameObject.GetComponent<Animator>().SetBool("isNotChosen", false);
        }
        if (EventHandlerScript.activeObj != this.gameObject.transform.name && EventHandlerScript.activeObj != null)
        {
            this.gameObject.GetComponent<Animator>().SetBool("isNotChosen", true);
        }
        
    }
    public void animate()
    {
        
    }
}
