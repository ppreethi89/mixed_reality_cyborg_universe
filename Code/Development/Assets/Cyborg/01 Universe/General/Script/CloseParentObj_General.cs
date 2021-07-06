using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseParentObj_General : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectToClose;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void closeParent()
    {
        //GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = false;

        Destroy(objectToClose);
    }
}
