using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using TMPro;
using System.IO;

public class AddEmotion : MonoBehaviour
{

    public TextMeshPro label;
    public TMP_InputField textField;


    void Start()
    {
        textField = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
    }


    public void addSpeechToInput()
    {
        textField.text += "<" + label.text + "> [ ] ";
        //textField.text += "<voice emotion='" + label.text + "'>  </voice>";
        /*if (label.text == "Happy")
        {
            //textField.text += "<voice emotion='happy'>  </voice>";
            textField.text += "Happy";
        }
        else if(label.text == "Sad")
        {
            //textField.text += "<voice emotion='sad'>  </voice>";
            textField.text += "Sad";
        }
        else if (label.text == "Calm")
        {
            //textField.text += "<voice emotion='calm'>  </voice>";
            textField.text += "Calm";
        }
        else if (label.text == "Cross")
        {
            //textField.text += "<voice emotion='cross'>  </voice>";
            textField.text += "angry voice";
        }*/
        textField.caretPosition = textField.text.Length - 3;
        textField.ActivateInputField();
    }

}
