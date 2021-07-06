using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Startup : MonoBehaviour
{
    // Start is called before the first frame update
    public string startupText;
    void Start()
    {
        gameObject.GetComponent<TMP_InputField>().text = startupText;
    }
    void Awake()
    {
        gameObject.GetComponent<TMP_InputField>().text = startupText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
