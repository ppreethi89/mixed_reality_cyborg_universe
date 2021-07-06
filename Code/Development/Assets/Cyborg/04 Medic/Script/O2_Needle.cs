using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class O2_Needle : MonoBehaviour
{
    public TextMesh oxygen;
    
    GameObject O2Needle;
    void Start()
    {
         
        
        String oxygen_value=oxygen.text.Replace("%","");
        Debug.Log("Oxygen Value"+oxygen_value);
        valuecheck(Int32.Parse(oxygen_value));
        // GameObject BPMNeedle=GameObject.Find("BPM_Needle");
        
    }
   public void valuecheck(int oxygen_value){
       GameObject O2_Needle=GameObject.Find("O2_Needle");
       Vector3 tmpPos=new Vector3(0, 0, 0);
       float rotation=0.0f;
       Debug.Log("x and y value at start" + O2_Needle.transform.position.x +"----"+O2_Needle.transform.position.y);
     Debug.Log("Oxygen Value"+oxygen.text);
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
