using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuplex.WebView;
using TMPro;
using UnityEngine.UI;

public class ResolutionChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public WebViewPrefab webview;
    [SerializeField]
    GameObject resolText;
    void Start()
    {
        StartCoroutine(startNow());
    }

    // Update is called once per frame
    void Update()
    {
        resolText.GetComponent<TextMeshPro>().text = webview.InitialResolution.ToString();
    }
    IEnumerator startNow()
    {
        yield return new WaitForSeconds(5f);
        webview = GameObject.Find("WebViewPrefab(Clone)").GetComponent<WebViewPrefab>();
        resolText = GameObject.Find("title");

    }
}
