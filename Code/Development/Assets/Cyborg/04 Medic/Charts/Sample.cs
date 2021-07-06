using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System;
// using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Linq;

public class Sample : MonoBehaviour
{
    LineGraphController lineGraph;
        public GameObject lineGraph_go;
    List<int> valueList;
    List<int> temp_valueList;
    
     public Text CurrentDate;
    LineGraphController.LineGraphParameter parameter;
    private System.DateTime _dateTime;
    private const string reqUrl = "https://cyborgmedic.azurewebsites.net/getBPMDictionary";
     Dictionary<string, string> My_dict1=new Dictionary<string, string>();
    void Start()
    {
          StartCoroutine(GetRequest());
              setLineGraph();
        
    }


    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         int value = Random.Range(0, 300);

    //         valueList.Add(value);
    //         lineGraph.AddValue(valueList.Count.ToString(), value);
    //     }

    //     if (Input.GetKeyDown(KeyCode.Z))
    //     {
    //         parameter.xSize = 10;
    //         lineGraph.ChangeParam(parameter);
    //     }
    //     if (Input.GetKeyDown(KeyCode.X))
    //     {
    //         parameter.ySize = 1;
    //         lineGraph.ChangeParam(parameter);
    //     }
    //     if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         parameter.yAxisSeparatorSpan = 50;
    //         lineGraph.ChangeParam(parameter);
    //     }
    //     if (Input.GetKeyDown(KeyCode.V))
    //     {
    //         parameter.valueSpan = 5;
    //         lineGraph.ChangeParam(parameter);
    //     }
    //     if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         Color blue = Color.blue;
    //         parameter.dotColor = blue;
    //         blue.a = 0.5f;
    //         parameter.connectionColor = blue;
    //         lineGraph.ChangeParam(parameter);
    //     }
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         parameter = LineGraphController.LineGraphParameter.Default;
    //         lineGraph.ChangeParam(parameter);
    //     }
    // }
    IEnumerator GetRequest()
    {
Debug.Log("Get Request for Readings");
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
    }
    public void setLineGraph()
    {
        
        lineGraph_go.SetActive(true);
        lineGraph = lineGraph_go.GetComponent<LineGraphController>();
       
        parameter = LineGraphController.LineGraphParameter.Default;

        lineGraph.ChangeParam(parameter);
        
      
        valueList=new List<int>(10){};

       
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
}
