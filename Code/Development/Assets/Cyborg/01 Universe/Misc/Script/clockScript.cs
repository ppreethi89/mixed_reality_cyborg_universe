using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Globalization;


public class clockScript : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshPro textClock;
/*    private TextMesh textClock;
*/
    void Awake()
    {
/*        textClock = GetComponent<Text>();
*/       

    }

    // Update is called once per frame
    void Update()
    {

        //string Month_day_Year = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        string Month_day_Year = DateTime.Now.ToString("ddd, dd MMM");
        string hour_minutes = DateTime.Now.ToString("h:mm tt");

        gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = Month_day_Year;
        gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = hour_minutes;

    }
    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}
