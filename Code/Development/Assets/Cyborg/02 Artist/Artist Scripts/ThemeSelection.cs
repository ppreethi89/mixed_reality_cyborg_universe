using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThemeSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void changeText()
    {

        string update_text = gameObject.GetComponentInChildren<TMP_Text>().text;
        GameObject theme = GameObject.Find("BaseTheme");
        theme.GetComponent<TMP_InputField>().text = update_text;
        
    }    



}

