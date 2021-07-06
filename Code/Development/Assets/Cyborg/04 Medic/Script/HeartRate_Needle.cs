using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HeartRate_Needle : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMesh bpm;
    
    GameObject BPMNeedle;
    void Start()
    {
         
        
        
        valuecheck(Int32.Parse(bpm.text));
        // GameObject BPMNeedle=GameObject.Find("BPM_Needle");
        
    }
   public void valuecheck(int bpm_value){
       GameObject BPMNeedle=GameObject.Find("BPM_Needle");
       Vector3 tmpPos=new Vector3(0, 0, 0);
       float rotation=0.0f;
     Debug.Log("BPM Value"+bpm.text);
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
    // Update is called once per frame
    void Update()
    {
       
        // Vector3 eulerRotation = BPMNeedle.transform.rotation.eulerAngles;
        // BPMNeedle.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, -60.69f);
    }
}
