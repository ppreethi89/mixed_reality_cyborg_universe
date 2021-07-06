using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuplex.WebView;
using TMPro;
using System;
public class VuplexKeyboardDwell : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> keyActions;

    public WebViewPrefab webview;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Dwell(TextMeshPro key)
    {
        EventHandlerScript.webViewPrefab.WebView.HandleKeyboardInput(key.text);

        //for (int i =0; i<)
        //webview.WebView.HandleKeyboardInput(key.text);
    }
    public void DoKeyAction(string key)
    {
        EventHandlerScript.webViewPrefab.WebView.HandleKeyboardInput(key);
    }
}
