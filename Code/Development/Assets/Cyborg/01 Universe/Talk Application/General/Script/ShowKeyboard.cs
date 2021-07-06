using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Microsoft.MixedReality.Toolkit.Examples.Demos;
using UnityEngine.EventSystems;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Microsoft.MixedReality.Toolkit;


[RequireComponent(typeof(TMP_InputField))]
public class ShowKeyboard : MonoBehaviour
{

    [Experimental]
    [SerializeField] private NonNativeKeyboard keyboard = null;
   
  

    public void onKeyboard()
    {
       //keyboard.PresentKeyboard();
       // keyboard.OnClosed += DisableKeyboard;
       // keyboard.OnTextSubmitted += DisableKeyboard;
        keyboard.OnTextUpdated += UpdateText;
    }

    private void UpdateText(string text)
    {
        GetComponent<TMP_InputField>().text = text;
    }

    private void DisableKeyboard(object sender, EventArgs e)
    {
        keyboard.OnTextUpdated -= UpdateText;
       // keyboard.OnClosed -= DisableKeyboard;
       // keyboard.OnTextSubmitted -= DisableKeyboard;

       // keyboard.Close();
        
           
    }

    

    







}
