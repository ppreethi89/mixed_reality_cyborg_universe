using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Linq;

public class CalendarController : MonoBehaviour
{
    public GameObject _calendarPanel;
    public GameObject lineGraph_go;
    public Text _yearNumText;
    public Text _monthNumText;
    public GameObject eventObject;
    LineGraphController lineGraph;
    List<int> valueList;
    List<int> temp_valueList;
    Dictionary<string, string> My_dict1=new Dictionary<string, string>();
    //  public Text CurrentDate;
    LineGraphController.LineGraphParameter parameter;
    private const string reqUrl = "https://cyborgmedic.azurewebsites.net/getBPMDictionary";

    public GameObject _item;
    //public TextMeshProUGUI CurrentDate;
    public Text CurrentDate;

    public List<GameObject> _dateItems = new List<GameObject>();
    const int _totalDateNum = 42;
    
    private DateTime _dateTime;
    public static CalendarController _calendarInstance;

    void Start()
    {
             StartCoroutine(GetRequest());
       
    }

    void CreateCalendar()
    {
        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);

        int date = 0;
        for (int i = 0; i < _totalDateNum; i++)
        {
            Text label = _dateItems[i].GetComponentInChildren<Text>();
            _dateItems[i].SetActive(false);

            if (i >= index)
            {
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    _dateItems[i].SetActive(true);

                    label.text = (date + 1).ToString();
                    date++;
                }
            }
        }
        _yearNumText.text = _dateTime.Year.ToString();
        _monthNumText.text = _dateTime.ToString("MMMM");
    }

    int GetDays(DayOfWeek day)
    {
        switch (day)
        {
            case DayOfWeek.Monday: return 1;
            case DayOfWeek.Tuesday: return 2;
            case DayOfWeek.Wednesday: return 3;
            case DayOfWeek.Thursday: return 4;
            case DayOfWeek.Friday: return 5;
            case DayOfWeek.Saturday: return 6;
            case DayOfWeek.Sunday: return 0;
        }

        return 0;
    }
    public void YearPrev()
    {
        _dateTime = _dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void YearNext()
    {
        _dateTime = _dateTime.AddYears(1);
        CreateCalendar();
    }

    public void MonthPrev()
    {
        _dateTime = _dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void MonthNext()
    {
        _dateTime = _dateTime.AddMonths(1);
        CreateCalendar();
    }

    public void ShowCalendar(Text target)
    {
       
        lineGraph_go.SetActive(false);
        _calendarPanel.SetActive(true);
        _target = target;
    }

     Text _target;
    public void OnDateItemClick(string day)
    {
         GameObject scoreGameObject = GameObject.Find("CurrentDate");
        CurrentDate = scoreGameObject.GetComponent<Text>();
        String date_to_set =_yearNumText.text+ " , " + day.PadLeft(2,'0') + " " + _monthNumText.text ;
        CurrentDate.text = date_to_set;
        // _target.text = _yearNumText.text + "Year" + _monthNumText.text + "Month" + day+"Day";
        setLineGraph();
        // eventObject.SetActive(true);
        _calendarPanel.SetActive(false);

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
                   
                    string valueArray=webRequest.downloadHandler.text;
                   
                    My_dict1 = JsonConvert.DeserializeObject<Dictionary<string, string>>(valueArray);
                }
                
            }
        }

        populate_data();
    }
    void populate_data()
    {
         _calendarInstance = this;
        Vector3 startPos = _item.transform.localPosition;
        _dateItems.Clear();
        _dateItems.Add(_item);
     

        for (int i = 1; i < _totalDateNum; i++)
        {
            GameObject item = GameObject.Instantiate(_item) as GameObject;
            item.name = "Item" + (i + 1).ToString();
            item.transform.SetParent(_item.transform.parent);
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localPosition = new Vector3((i % 7) * 31 + startPos.x, startPos.y - (i / 7) * 25, startPos.z);

            _dateItems.Add(item);
        }

        _dateTime = DateTime.Now;
        GameObject scoreGameObject = GameObject.Find("CurrentDate");
        CurrentDate = scoreGameObject.GetComponent<Text>();
        String date_to_set = _dateTime.Year.ToString() + " , " + _dateTime.ToString("dd") + " " + _dateTime.ToString("MMMM");
        CurrentDate.text = date_to_set;
        CreateCalendar();

        _calendarPanel.SetActive(false);
          setLineGraph();
    }
    public void setLineGraph()
    {
        
        lineGraph_go.SetActive(true);
        lineGraph = lineGraph_go.GetComponent<LineGraphController>();
       
        parameter = LineGraphController.LineGraphParameter.Default;

        lineGraph.ChangeParam(parameter);
        
      
        valueList=new List<int>(10){};
        
    //     temp_valueList = new List<int>()
    //     {
    //         60,75,73,70,60,75,80,90,0,0,0,0
    //     };
    //     My_dict1.Add("2021 , 22 February", temp_valueList);
    //   temp_valueList = new List<int>()
    //     {
    //         120,80,60,75,73,70,60,75,80,90,100,120
    //     };
    //        My_dict1.Add("2021 , 23 February", temp_valueList);

        
       
        GameObject scoreGameObject = GameObject.Find("CurrentDate");
        CurrentDate = scoreGameObject.GetComponent<Text>();
        Debug.Log("date from calendar is"+ CurrentDate.text);
       foreach(KeyValuePair<string, string> ele2 in My_dict1) 
          {     
               Debug.Log("key is" + ele2.Key);
              if (ele2.Key==CurrentDate.text)
               { 
                valueList=new List<int>(ele2.Value.Split(',').Select(int.Parse));
                  
                break;
               }
           
          }
          if(valueList.Count>0)
          {
          for (int i = 0; i < 12; i++)
                    {
                        if(i < valueList.Count)
                        lineGraph.AddValue((i + 1).ToString(), valueList[i]);
                        else
                        lineGraph.AddValue((i + 1).ToString(), 0);
                    }
          }
           else{
             
              valueList=new List<int>(){ 0,0,0,0,0,0,0,0,0,0,0,0};
              for (int i = 0,j=0; i < valueList.Count; i++,j=j+2)
                    {
                      
                        lineGraph.AddValue(j.ToString(), valueList[i]);
                    }
          }
      

        lineGraph.SetXUnitText("Time");
        lineGraph.SetYUnitText("BPM");
    }


    public void closeEventClick()
    {
        eventObject.SetActive(false);
    }
}
