using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vuplex.WebView;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
public class DwellVuplexBrowser : MonoBehaviour
{
    // Start is called before the first frame update
    public WebViewPrefab webview;
    private Vector3 firstGaze;
    private Vector3 secondGaze;
    private Vector3 thirdGaze;
    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    public void DwellPoint()
    {
        Vector3 eyeGazePoint = CoreServices.InputSystem.EyeGazeProvider.HitPosition;
        Vector2 point = _convertToScreenPosition(eyeGazePoint);
        firstGaze = eyeGazePoint;

        if(firstGaze != secondGaze)
        {
            webview._webView.Click(point);
            secondGaze = firstGaze;
            try
            {
                EventHandlerScript.webViewPrefab = this.webview;
                Debug.Log(EventHandlerScript.webViewPrefab + "YOW");
            }
            catch
            {

            }
        }
        else
        {
            Debug.Log("You are gazing on the same point");
            
        }
        //Debug.Log("The Eyegaze point is " + eyeGazePoint);
        
        //Debug.Log("The vector2 point is " + point);
       
        //this.GetComponent<EyeTrackingTarget>().IsDwelledOn = false;
        //this.GetComponent<EyeTrackingTarget>().IsLookedAt = false;
        this.GetComponent<EyeTrackingTarget>().OnLookAway.Invoke();
        this.GetComponent<EyeTrackingTarget>().OnEyeFocusStop();

        
    }
    Vector2 _convertToScreenPosition(Vector3 worldPosition)
    {

        var localPosition = webview._viewResizer.transform.InverseTransformPoint(worldPosition);
        return new Vector2(1 - localPosition.x, -1 * localPosition.y);
    }





}
