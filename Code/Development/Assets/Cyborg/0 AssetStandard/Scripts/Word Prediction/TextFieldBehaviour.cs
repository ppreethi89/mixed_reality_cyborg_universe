using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using TMPro;
using System.Threading;
//using WindowsInput;
//using NewtonVR;

public class TextFieldBehaviour : MonoBehaviour, ISelectHandler
{
	public NGramGenerator NGramHandler;
	public float count = 0;
	//public NVRButton Space;
	int z =1;
	string textCheck;
	int pos;
	private TMP_InputField inputField;
	//private InputField inputField;

	void Start()
	{
		inputField = gameObject.GetComponent<TMP_InputField>();
		//inputField = gameObject.GetComponent<InputField>();
	}

	public void OnSelect(BaseEventData eventData)
	{
		StartCoroutine(DisableHighlight());
	}

	public void MoveCaretToEnd()
	{
		StartCoroutine(DisableHighlight());
	}

	IEnumerator DisableHighlight()
	{
		Color originalTextColor = inputField.selectionColor;
		originalTextColor.a = 0f;

		inputField.selectionColor = originalTextColor;

		//Wait for one frame
		yield return null;

		//Scroll the view with the last character
		inputField.MoveTextEnd(true);
		//Change the caret pos to the end of the text
		inputField.caretPosition = inputField.text.Length;

		originalTextColor.a = 1f;
		inputField.selectionColor = originalTextColor;
	}

	void Update()
	{
		//if(Input.GetKeyUp(KeyCode.Space) || Space.ButtonUp && inputField.isFocused)
		//if(Input.GetKeyUp(KeyCode.Space))
		
		
		//NGramHandler.PredictNextWords(lastWord);
		if (inputField.text.EndsWith(" "))
			{
			count = count + 1;
			if (count == 1)
			{
				string inputText = inputField.text.TrimEnd();
				string lastWord = inputText.Split(' ').Last();
				NGramHandler.PredictNextWords(lastWord);
				
			}
			
		}
		
        if (inputField.caretPosition < inputField.text.Length)
        {
			textCheck = inputField.text.Substring(inputField.caretPosition, 1);
			for (int zy = 1; textCheck != " "; zy++)
            {
				//Debug.Log(textCheck);
				try
				{
					

					if (inputField.caretPosition - zy >= 0 && textCheck != " ")
					{
						textCheck = inputField.text.Substring(inputField.caretPosition - zy, 1);
						z = zy;
						//Debug.Log(textCheck);
					}
					else
					{
						//Debug.Log("Break");
						break;

					}
					z = zy;
				}
                catch
                {
					break;
                }

			}
            
            textCheck = inputField.text.Substring(z-1, z);
			//Debug.Log(textCheck);
			//Debug.Log(inputField.caretPosition);
			NGramHandler.PredictNextWords(textCheck);
			
		}
    }
}