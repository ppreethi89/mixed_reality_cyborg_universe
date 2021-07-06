using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(this.gameObject == TileTest.activeObj)
        {
            Debug.Log("active GameObject the same with " + this.gameObject.name);
        }*/
        if(TileTest.activeObj != null && TileTest.activeObj != this.gameObject)
        {
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        }
    }
    public void dwell()
    {
        TileTest.activeObj = this.gameObject;
        
    }
}
