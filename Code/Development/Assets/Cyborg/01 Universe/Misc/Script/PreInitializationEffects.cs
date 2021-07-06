using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreInitializationEffects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextWriter textWriter;

    [SerializeField]
    private string text;
    public TextMeshPro messageText;
    [SerializeField]
    float speed;

    [SerializeField]
    float delay;

    [SerializeField]
    float delayHide;

    [SerializeField]
    GameObject parentObject;
    void Start()
    {
        messageText.faceColor = new Color32(255, 255, 255, 0);
        StartCoroutine(StartEffect());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TextEffect()
    {
        textWriter.AddWriter(messageText, text, speed, true);
        StartCoroutine(HideStarterEffect());
    }
    IEnumerator HideStarterEffect()
    {
        yield return new WaitForSeconds(delayHide);
        try
        {
            parentObject.SetActive(false);

        }
        catch
        {
            Debug.Log("No Parent Object");
        }
    }
    IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(delay);
        messageText.faceColor = new Color32(255, 255, 255, 255);
        TextEffect();

    }
}
