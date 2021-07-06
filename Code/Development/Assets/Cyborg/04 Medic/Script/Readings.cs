using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Readings : MonoBehaviour
{
      public TextMesh O2_Value;
      public TextMesh BPM_Value;
    
    private const string reqUrl = "https://cyborgmedic.azurewebsites.net/getLastValue";
    // Start is called before the first frame update
    void Start()
    {
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
                    Debug.Log( valueArray[0]);
                
                    O2_Value.text=valueArray[1].Replace("O2:","").Replace("\"","");

                    BPM_Value.text=valueArray[0].Replace("\"BPM:","");
                    Debug.Log("the Value is set "+ BPM_Value.text);
                    valuecheck(Int32.Parse(BPM_Value.text));
                    O2_valuecheck(Int32.Parse(O2_Value.text));
                }
                
            }
        }
    }

     public void valuecheck(int bpm_value){
       GameObject BPMNeedle=GameObject.Find("BPM_Needle");
       Vector3 tmpPos=new Vector3(0, 0, 0);
       float rotation=0.0f;
   
        if(bpm_value>=71 && bpm_value <=74){
            tmpPos.y=-0.212f;
            tmpPos.x=-1.430588235f;
            rotation=-11f;

        }
         else if(bpm_value==75){
            tmpPos.y=-0.212f;
            tmpPos.x=-1.430588235f;
            rotation=-17f;

        }
         else if(bpm_value>=76 && bpm_value<=79){
            tmpPos.y=-0.212f;
            tmpPos.x=-1.430588235f;
            rotation=-24f;

        }
         else if(bpm_value==80){
            tmpPos.y=-0.217f;
            tmpPos.x=-1.430588235f;
            rotation=-34.5f;

        }
         else if(bpm_value>=81 && bpm_value<=84){
            tmpPos.y=-0.2301f;
            tmpPos.x=-1.385882353f;
            rotation=-44.466f;

        }
         else if(bpm_value==85){
            tmpPos.y=-0.2466f;
            tmpPos.x=-1.359411765f;
            rotation=-48.308f;

        }
          else if(bpm_value>=86 && bpm_value<=89){
            tmpPos.y=-0.251f;
            tmpPos.x=-1.346470588f;
            rotation=-54.116f;

        }
         else if(bpm_value==90){
            tmpPos.y=-0.279f;
            tmpPos.x=-1.324705882f;
            rotation=-60.69f;

        }
         else if(bpm_value>=91 && bpm_value<=94){
             tmpPos.y=-0.279f;
            tmpPos.x=-1.324705882f;
            rotation=-70.091f;

        }
          else if(bpm_value==95){
             tmpPos.y=-0.279f;
            tmpPos.x=-1.324705882f;
            rotation=-75.5f;

        }
         else if(bpm_value>=96 && bpm_value<=99){
             tmpPos.y=-0.308f;
            tmpPos.x=-1.311764706f;
            rotation=-75.419f;

        }
         else if(bpm_value==100){
             tmpPos.y=-0.308f;
            tmpPos.x=-1.311764706f;
            rotation=-75.419f;

        }
          else if(bpm_value>=101 && bpm_value<=104){
             tmpPos.y=0.31f;
            tmpPos.x=-1.303529412f;
            rotation=-91.228f;

        }
         else if(bpm_value==105){
             tmpPos.y=-0.12f;
            tmpPos.x=-1.303529412f;
            rotation=-91.7f;

        }
         else if(bpm_value>=106 && bpm_value<=109){
              tmpPos.y=-0.12f;
            tmpPos.x=-1.303529412f;
            rotation=-97.53f;

        }
         else if(bpm_value==110){
             tmpPos.y=-0.52f;
            tmpPos.x=-1.295294118f;
            rotation=-102.398f;

        }
        
  
        BPMNeedle.transform.position=tmpPos;
        Vector3 eulerRotation = BPMNeedle.transform.rotation.eulerAngles;
        BPMNeedle.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, rotation);

      
   }

 public void O2_valuecheck(int oxygen_value){
       GameObject O2_Needle=GameObject.Find("O2_Needle");
       Vector3 tmpPos=new Vector3(0, 0, 0);
       float rotation=0.0f;
       Debug.Log("x and y value at start" + O2_Needle.transform.position.x +"----"+O2_Needle.transform.position.y);
    //  Debug.Log("Oxygen Value"+oxygen.text);
        if(oxygen_value>=71 && oxygen_value <=74){
            tmpPos.y=-0.2262027f;
            tmpPos.x=0.4180513f;
            rotation=-44.498f;

        }
         else if(oxygen_value==75){
            tmpPos.y=-0.2262027f;
            tmpPos.x=0.4180513f;
            rotation=-53.926f;

        }
         else if(oxygen_value>=76 && oxygen_value<=79){
            tmpPos.y=-0.2489f;
            tmpPos.x=0.46719f;
            rotation=-53.841f;

        }
         else if(oxygen_value==80){
           tmpPos.y=-0.2489f;
            tmpPos.x=0.46719f;
            rotation=-65.894f;

        }
         else if(oxygen_value>=81 && oxygen_value<=84){
            tmpPos.y=-0.2830f;
            tmpPos.x=-0.48357f;
            rotation=-68.68501f;

        }
         else if(oxygen_value==85){
            tmpPos.y=-0.2830f;
            tmpPos.x=-0.48357f;
            rotation=-73.914f;

        }
          else if(oxygen_value>=86 && oxygen_value<=89){
           tmpPos.y=-0.2830f;
            tmpPos.x=0.48357f;
            rotation=-77.50101f;

        }
         else if(oxygen_value==90){
            tmpPos.y=-0.3285f;
            tmpPos.x=0.4890f;
            rotation=-85.30601f;

        }
         else if(oxygen_value>=91 && oxygen_value<=94){
            tmpPos.y=-0.3285f;
            tmpPos.x=0.4890f;
            rotation=-87.769f;

        }
          else if(oxygen_value==95){
             tmpPos.y=-0.3285f;
            tmpPos.x=0.4890f;
            rotation=-92.47701f;

        }
         else if(oxygen_value>=96 && oxygen_value<=99){
           tmpPos.y=-0.3285f;
            tmpPos.x=0.4890f;
            rotation=-100.876f;

        }
         else if(oxygen_value==100){
             tmpPos.y=-0.3285f;
            tmpPos.x=0.4890f;
            rotation=-106.354f;

        }
          
        
  
        O2_Needle.transform.position=tmpPos;
        Vector3 eulerRotation = O2_Needle.transform.rotation.eulerAngles;
        O2_Needle.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, rotation);

      
   }
    // Update is called once per frame
    void Update()
    {
        
    }
}
