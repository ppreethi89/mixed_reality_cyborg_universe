using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class O2LevelBarDesign : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage designVerticalBar;
    public GameObject line;
    public Image loadingbar;
    // public TextMeshPro oxygenLabel;
    public TextMesh oxygenLabel;
    public GameObject gear;
    // public TextMeshPro oxygenPercentLabel;
    public TextMesh oxygenPercentLabel;
    
    Vector3 currentEulerAngles;
    private const string reqUrl = "https://cyborgmedic.azurewebsites.net/getLastValue";
    float x;
    bool value_fetched=false;
    void Start()
    {
        //InvokeRepeating("RangeValue", 0f, 1f);
        // x = 0.89f;
        StartCoroutine(GetRequest());
    }
    void RangeValue()
    {
        //demo range oxygen level purpose
        // x = UnityEngine.Random.Range(0.5f, 0.85f);
         StartCoroutine(GetRequest());
        
    }

IEnumerator GetRequest()
    {
Debug.Log("Get Request");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(reqUrl))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

          
            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                
                if (webRequest.downloadHandler.text.Length > 3)
                {
                    Debug.Log(webRequest.downloadHandler.text);
                   
                    string[] valueArray=webRequest.downloadHandler.text.Split(',');
                    // JSONNode bpm = JSON.Parse(valueArray[0]);
                    Debug.Log( valueArray[1]);
                
                    x=float.Parse(valueArray[1].Replace("O2:","").Replace("\"",""));
                    value_fetched=true;
                    Debug.Log(valueArray[1].Replace("O2:","").Replace("\"",""));
                    Debug.Log(x);
                }
                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (value_fetched==false){
        x = x-0.001f;
        if (x < 0.5f)
        {
            x = 0.85f;
        }
        }
        loadingbar.GetComponent<Image>().fillAmount = x;
        Rect uvRect = designVerticalBar.uvRect;
        uvRect.y += 1f * Time.deltaTime;
        designVerticalBar.uvRect = uvRect;

        line.GetComponent<Transform>().localPosition = new Vector3(0, loadingbar.GetComponent<Image>().fillAmount * 100 - 60, -0.05f);
        
        if (loadingbar.GetComponent<Image>().fillAmount * 100 < 70)
        {
            oxygenLabel.color = new Color32(255, 0, 0, 255);
            oxygenPercentLabel.color = new Color32(255, 0, 0, 255);
            line.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
        }
        else
        {
            oxygenLabel.color = new Color32(255, 255, 255, 255);
            oxygenPercentLabel.color = new Color32(255, 255, 255, 255);
            line.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        
            
        

        if (value_fetched==false){
        oxygenLabel.text =(loadingbar.GetComponent<Image>().fillAmount *100).ToString("F0") + "%";
        }
        else{
oxygenLabel.text = x.ToString() +"%";
        }

        gear.transform.Rotate(0, (20f * Time.deltaTime), 0);
        
    }
}
