using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CalendarDateItemO2 : MonoBehaviour {

    public void OnDateItemClick()
    {
        CalendarControllerOxygen._calendarInstance.OnDateItemClick(gameObject.GetComponentInChildren<Text>().text);
    }

    public void closeEventClick()
    {
        CalendarControllerOxygen._calendarInstance.closeEventClick();
    }
}