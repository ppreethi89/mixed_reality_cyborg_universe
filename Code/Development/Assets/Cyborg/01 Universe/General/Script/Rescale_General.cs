using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rescale_General : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject parentObj;
    [SerializeField]
    float scaleAmount;

    [SerializeField]
    GameObject scaleControls;



    bool isActive = false;
    void Start()
    {
        //scaleControls.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showHideControls()
    {
        isActive = !isActive;
        if (isActive)
        {
            scaleControls.SetActive(true);

        }
        else
        {
            scaleControls.SetActive(false);

        }
    }
    
    public void scaleUp()
    {
        parentObj.transform.localScale += new Vector3(scaleAmount, scaleAmount, 0f);
    }
    public void scaleDown()
    {
        parentObj.transform.localScale -= new Vector3(scaleAmount, scaleAmount, 0f);
    }
}
