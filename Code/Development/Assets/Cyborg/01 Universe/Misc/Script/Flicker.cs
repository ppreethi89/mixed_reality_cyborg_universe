using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Flicker : MonoBehaviour
{
    // Start is called before the first frame update
 
    void Start()
    {
       

    }

    // Update is called once per frame
   public void SummonImage()
    {
        Debug.Log("Yow");

    }
    void OnBecameInvisible()
    {
        Debug.Log("Hide");
    }
}
