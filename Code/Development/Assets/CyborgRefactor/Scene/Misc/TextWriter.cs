using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextWriter : MonoBehaviour
{
    // Start is called before the first frame update

    private TextMeshPro messageText;
    private string textToWrite;
    private float timePerCharacter;
    private int characterIndex;
    private float timer;
    private bool invisibleCharacter;

    void Start()
    {
        
    }

    public void AddWriter(TextMeshPro messageText, string textToWrite, float timePerCharacter, bool invisibleCharacter)
    {
        this.messageText = messageText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        this.invisibleCharacter = invisibleCharacter;
        characterIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (messageText != null)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                if (invisibleCharacter)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }
                messageText.text = text;

                if (characterIndex >= textToWrite.Length)
                {
                    messageText = null;
                    Debug.Log("Entire Text Displayed");
                    return;
                }
            }

        }
    }
}
