using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text;

public class AddCorrectWord : MonoBehaviour
{
	private AutocompleteWordPicker wordPicker;

	string predictedWord;
	int m_CaretPosition;
	string textCheck;
	int p;
	string o;
	void Start()
	{
		wordPicker = gameObject.GetComponentInParent<AutocompleteWordPicker>();
	}

	public void WordChosen()
	{
        //wordPicker.ReplaceWord(gameObject.GetComponentInChildren<Text>().text);
        try
        {
			predictedWord = gameObject.GetComponentInChildren<Text>().text;
			m_CaretPosition = wordPicker.TextField.caretPosition;
			textCheck = wordPicker.TextField.text.Substring(m_CaretPosition - 1, 1);
			o = wordPicker.TextField.text.Substring(m_CaretPosition, 1);
		}
        catch
        {
			Debug.Log("No input yet");
        }
		
		
		if (textCheck == " " || textCheck == null)
        {
			wordPicker.TextField.text = wordPicker.TextField.text.Insert(m_CaretPosition, predictedWord +" ");
			wordPicker.TextField.caretPosition = wordPicker.TextField.text.Length;
		}

		
		int z = 0;
		if (textCheck != " ")
        {
			for (int zy = 1; textCheck != " "; zy++)
            {

				try
				{
					textCheck = wordPicker.TextField.text.Substring(m_CaretPosition - zy, 1);

					if (m_CaretPosition - zy >= 0 && textCheck != " ")
					{
						textCheck = wordPicker.TextField.text.Substring(m_CaretPosition - zy, 1);
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
            //var theString = "ABCDEFGHIJ";

            try
            {
				for (int x = 0; o != " "; x++)
				{
					p = x;
					o = wordPicker.TextField.text.Substring(m_CaretPosition + x, 1);

					//Debug.Log("P  is " + p);
				}
			}
            catch
            {

            }
            
			//Debug.Log(z);
			var aStringBuilder = new StringBuilder(wordPicker.TextField.text);
			int e = z + p;
			aStringBuilder.Remove(m_CaretPosition - (z), e);
			aStringBuilder.Insert(m_CaretPosition - (z), " " + predictedWord + " ");
			
			Debug.Log(aStringBuilder.ToString());
			wordPicker.TextField.text = aStringBuilder.ToString();
			/*string textTransform = wordPicker.TextField.text.Substring(m_CaretPosition - (z - 1), z-1);
			wordPicker.TextField.text = wordPicker.TextField.text.Substring(0)*/
		}



		//wordPicker.TextField.caretPosition = (m_CaretPosition - (z)) + predictedWord.Length;
		//wordPicker.TextField.caretPosition = wordPicker.TextField.text.Length;
		GameObject textfield = GameObject.Find("InputField (TMP)");
		textfield.GetComponent<TextFieldBehaviour>().count = 0;
	}
}