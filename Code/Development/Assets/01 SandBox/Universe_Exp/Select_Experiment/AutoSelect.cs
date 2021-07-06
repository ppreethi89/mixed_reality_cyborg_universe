using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;


public class AutoSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public bool change = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (change == false)
        {
            StartCoroutine(activateSelect());
        }

    }
    IEnumerator activateSelect()
    {
        change = true;
        yield return new WaitForSeconds(3);
        try
        {
            GameObject target = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GazeProvider>().GazeTarget;
            GameObject.Find(target.transform.name).GetComponent<EyeTrackingTarget>().OnSelected.Invoke();
            change = false;
        }
        catch
        {
            Debug.Log("No Game Object currently gaze at");
            change = false;
        }
        yield return true;
    }
}
