using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Linq;
using UnityEngine.UI;

public class SubmitKeyboardv2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject talkappinput;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void submit()
    {
        /*gameObject.transform.parent.parent.GetComponent<InputField>().text = "Hello";*///
        talkappinput.GetComponent<TMP_InputField>().text = "Hello";
        
    }
}
