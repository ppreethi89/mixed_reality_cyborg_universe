using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RotatingBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject gear;

    [SerializeField]
    Image circleImg;
    float value = 0;
    [SerializeField]
    [Range(1, 100)] float progress = 0f;

    [SerializeField]
    [Range(0, 1)] float speed = 0f;

    [SerializeField]
    GameObject progressBarV4;

    static GameObject staticProgressBarV4;


    [SerializeField]
    private TextWriter textWriter;

    [SerializeField]
    private string text;
    public TextMeshPro messageText;
    private void Awake()
    {
        staticProgressBarV4 = progressBarV4;
        text = messageText.text;
    }
    void Start()
    {
        
    }
    private void OnEnable()
    {
        InvokeRepeating("TextEffect", 1f, 2f);
    }
    void TextEffect()
    {
        textWriter.AddWriter(messageText, text, 0.1f, true);
    }
    public static void ShowHideLoadingBarV4(bool x)
    {
        staticProgressBarV4.SetActive(x);
    }
    // Update is called once per frame
    void Update()
    {
        if(value < 1)
        {
            value = value + speed;
            circleImg.fillAmount = value;
        }
        else
        {
            value = 0;
        }
        gear.transform.Rotate(new Vector3(0f, 0f, 1f) * Time.deltaTime*progress);
    }
}
