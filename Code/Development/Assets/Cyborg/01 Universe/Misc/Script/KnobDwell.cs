using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit;
using TMPro;


public class KnobDwell : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    public Image img;
    [SerializeField]
    RectTransform arrow;
    public TextMeshProUGUI label;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GazeKnob()
    {
        Vector3 hitpos = CoreServices.InputSystem.EyeGazeProvider.HitPosition;
       


        float angle = 0.0f;
        angle = Mathf.Atan2(hitpos.y - obj.transform.position.y, hitpos.x - obj.transform.position.x) * 180 / Mathf.PI;

        if (angle < 0)
        {
            angle = 180 + (180 + angle);
            
        }
        /*else
        {
            Debug.Log(angle);
        }*/
        Debug.Log(angle);
        img.fillAmount = angle/360;
        Debug.Log(angle * 360);
        arrow.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        label.text = Mathf.Floor(angle).ToString() + "°";

    }
}
