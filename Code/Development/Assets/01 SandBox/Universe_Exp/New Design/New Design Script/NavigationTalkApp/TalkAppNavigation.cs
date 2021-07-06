using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkAppNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField input;
    
    void Start()
    {
       input = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void navigateLeft()
    {
        Debug.Log("Navigate left");
        if(input.caretPosition != 0)
        {
            input.caretPosition = input.caretPosition - 1;
            input.ActivateInputField();
        }
    }

    public void navigateRight()
    {
        Debug.Log("Navigate right");
        if(input.caretPosition != input.text.Length)
        {
            input.caretPosition = input.caretPosition + 1;
            input.ActivateInputField();
        }
    }

    public void navigateUp()
    {
        Debug.Log("Navigate up");
        if(input.caretPosition > 15)
        {
            input.caretPosition = input.caretPosition - 15;
            input.ActivateInputField();
        }
    }

    public void navigateDown()
    {
        Debug.Log("Navigate down");
        if(input.caretPosition + 15 < input.text.Length)
        {
            input.caretPosition = input.caretPosition + 15;
            input.ActivateInputField();
        }
    }

    public void navigatePgUp()
    {
        Debug.Log("Navigate pgup");
        if(input.caretPosition > 45)
        {
            input.caretPosition = input.caretPosition - 45;
            input.ActivateInputField();
        }
        else
        {
            input.MoveTextStart(false);
        }
    }

    public void navigatePgDown()
    {
        Debug.Log("Navigate pgdown");
        if(input.caretPosition + 45 > input.text.Length)
        {
            input.caretPosition = input.caretPosition + 45;
            input.ActivateInputField();
        }
        else
        {
            input.MoveTextEnd(false);
        }
    }

    public void navigateHome()
    {
        Debug.Log("Navigate home");
        input.MoveTextStart(false);
    }

    public void navigateEnd()
    {
        Debug.Log("Navigate end");
        input.MoveTextEnd(false);
    }

    public void navigateWordLeft()
    {
        Debug.Log("Navigate w-");
        string delimiterChars = " ,.:";
        string text = input.text;
        if(input.caretPosition > 0)
        {
            int i = input.caretPosition - 2;
            while((i >= 0) && !delimiterChars.Contains(text[i].ToString()))
            {
                i--;
            }
            input.caretPosition = i + 1;
        }
    }

    public void navigateWordRight()
    {
        Debug.Log("Navigate w+");
        string delimiterChars = ",.: ";
        string text = input.text;
        if(input.caretPosition < input.text.Length - 1)
        {
            int i = input.caretPosition;
            while(!delimiterChars.Contains(text[i].ToString()) && (i < input.text.Length - 1))
            {
                i++;
            }
            input.caretPosition = i + 1;
        }  
    }
}
