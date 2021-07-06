using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
public class HeartRate : MonoBehaviour
{
    private BPM bpm;
    private Image heartBeat;
    private float heartBeatHeight;
    private RectTransform rT;
    public TextMesh value;
    private const string reqUrl = "https://cyborgmedic.azurewebsites.net/getLastValue";
    float x;
    bool value_fetched = false;

     void Start()
    {
        //InvokeRepeating("RangeValue", 0f, 1f);
        // x = 0.89f;
        // StartCoroutine(GetRequest());
         InvokeRepeating("RangeValue", 5.0f, 5f);
    }
     void RangeValue()
    {
        //demo range oxygen level purpose
        // x = UnityEngine.Random.Range(0.5f, 0.85f);
         StartCoroutine(GetRequest());
        
    }
    // Start is called before the first frame update
    private void Awake()
    {
        heartBeat = transform.Find("heart-beat-icon").GetComponent<Image>();
        rT = transform.Find("heart-beat-icon").GetComponent<RectTransform>();

        //heartBeat.fillAmount = .3f;

        bpm = new BPM();

        //bpm.TryHeartRate();
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

                    string[] valueArray = webRequest.downloadHandler.text.Split(',');
                    // JSONNode bpm = JSON.Parse(valueArray[0]);
                    Debug.Log(valueArray[0]);

                    value.text = valueArray[0].Replace("BPM:", "").Replace("\"", "");
                    value_fetched = true;
                    Debug.Log(valueArray[0].Replace("BPM:", "").Replace("\"", ""));
                    Debug.Log(x);
                }

            }
        }
    }


    private void Update()
    {
      
        bpm.value = float.Parse(value.text);
        if (bpm.value < 5)
        {
            bpm.value = 5;
        }
        double number = bpm.value * 0.0442;
        float variable = (float)number;
        if (number < 0.5)
        {
            variable = 0.5f;
        }
        else if (number > 3)
        {
            variable = 3f;
        }
        rT.sizeDelta = new Vector2(rT.sizeDelta.x, variable);
        Debug.Log(rT);
        bpm.Update();
        bpm.TryHeartRate();
        heartBeat.fillAmount = bpm.GetHeartNormalized();
    }
}

public class BPM
{
    public const int heart_max = 100;
    private float heartAmount;
    private float heartRate;
    public float value;


    public BPM()
    {
        heartAmount = 0;
        heartRate = 30f;
    }

    public void Update()
    { 
        heartAmount += value * Time.deltaTime;
        heartAmount = Mathf.Clamp(heartAmount, 0f, heart_max);
    }

    public void TryHeartRate()
    {
        if (heartAmount == 100)
        {
            heartAmount = 0;
        }
    }

    public float GetHeartNormalized()
    {
        return heartAmount / heart_max;
        
    }
}



