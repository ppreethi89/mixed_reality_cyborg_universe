using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CyborgProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    RectTransform fxHolder;
    [SerializeField]
    Image circleImg;
    [SerializeField]
    Text txtProgress;

    [SerializeField]
    [Range(0, 1)] float progress = 0f;

    
    [SerializeField]
    GameObject progressBar;

    public static float value;
    static GameObject loadingBar;
    static Text staticTxtProgress; 
    static bool showText = true;
    private void Awake()
    {
        loadingBar = progressBar;
        staticTxtProgress = txtProgress;
    }
    void Start()
    {
        
    }
    public static void ShowLoadingBar(bool? isTextVisible = true)
    {
        showText = isTextVisible ?? true;
        if (showText)
        {
            staticTxtProgress.gameObject.SetActive(true);
        }
        else
        {
            staticTxtProgress.gameObject.SetActive(false);
        }
        loadingBar.SetActive(true);
    }
    public static void HideLoadingBar()
    {
        loadingBar.SetActive(false);
        value = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
        circleImg.fillAmount = value;
        
        txtProgress.text = Mathf.Floor(value * 100).ToString() + "%";
        fxHolder.rotation = Quaternion.Euler(new Vector3(0f, 0f, -value * 360));
       

    }
}
