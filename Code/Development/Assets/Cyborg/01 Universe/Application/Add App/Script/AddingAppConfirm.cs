using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddingAppConfirm : MonoBehaviour
{
    // Start is called before the first frame update
    public string appName;
    public string tag = "ConfirmBox";
    void Start()
    {
        appName = gameObject.transform.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void yesAdd()
    {
        appName = GameObject.Find("ControlClass").GetComponent<animationControl>().addGameObject;
        GameObject.Find(appName).GetComponent<EnableApp>().Enable();

        GameObject[] addconfirmboxes = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < addconfirmboxes.Length; i++)
        {
            GameObject.Destroy(addconfirmboxes[i]);
        }

    }
    public void noAdd()
    {
        GameObject.Find("ControlClass").GetComponent<animationControl>().addGameObject = "";

        GameObject[] addconfirmboxes = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < addconfirmboxes.Length; i++)
        {
            GameObject.Destroy(addconfirmboxes[i]);
        }
    }

    public void confirm()
    {
        string message = "Are you sure you want to add " + appName + " ?";
        ConfirmationBox_General.showConfirmation(yesAdd, message);
        GameObject.Find("ControlClass").GetComponent<animationControl>().addGameObject = appName;
    }
}
