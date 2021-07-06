using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MessageField : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextWriter textWriter;

    [SerializeField]
    private string text;
    public TextMeshPro messageText;
    private void Awake()
    {
        
    }
    void OnEnable()
    {
        
        textWriter.AddWriter(messageText, messageText.text, 0.03f,true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
