using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCountReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void countReset()
    {
        GameObject textfield = GameObject.Find("InputField (TMP)");
        textfield.GetComponent<TextFieldBehaviour>().count = 0;
    }
}
