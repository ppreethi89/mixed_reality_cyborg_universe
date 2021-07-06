using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MoveTalkAppv2 : MonoBehaviour
{

    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject talkApp;
    //public string filepath = "talkappConfig.json";


    // Start is called before the first frame update
    void Start()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
        upArrow.SetActive(false);
        downArrow.SetActive(false);
    }

    void Update()
    {

    }

    public void ShowMoveTalkApp()
    {
        rightArrow.SetActive(true);
        leftArrow.SetActive(true);
        upArrow.SetActive(true);
        downArrow.SetActive(true);

        ShowStatus.showStatus("Move");
        ShowStatus.consumeAction(SaveMoveTalkApp);
    }

    public void SaveMoveTalkApp()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
        upArrow.SetActive(false);
        downArrow.SetActive(false);
        GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = true;
        //GameObject.Find("ControlClass").GetComponent<SavePosition>().startSavePos();

        StartCoroutine(InitializeSave.SaveTransform(talkApp));
        Debug.Log(talkApp);

    }

    public void moveLeft()
    {
        talkApp.transform.position += new Vector3(-0.01f, 0f, 0f);
    }
    public void moveRight()
    {
        talkApp.transform.position += new Vector3(0.01f, 0f, 0f);
    }
    public void moveUp()
    {
        talkApp.transform.position += new Vector3(0f, 0.01f, 0f);
    }
    public void moveDown()
    {
        talkApp.transform.position += new Vector3(0f, -0.01f, 0f);
    }



    // Update is called once per frame
    /* public void DisplayMoveControl()
     {
         controlclass.GetComponent<animationControl>().isChosen = true;
         string parentobj = gameObject.transform.parent.parent.name;
         GameObject.Find("ControlClass").GetComponent<animationControl>().trigger(gameObject.transform.name, parentobj);
         Debug.Log(gameObject.transform.name);
         StartCoroutine(waitTriggerDisplayControl());

     }*/
    /*void Update()
    {
        if (controlclass.GetComponent<animationControl>().Move == false)
        {
            rightArrow.SetActive(false);
            leftArrow.SetActive(false);
            upArrow.SetActive(false);
            downArrow.SetActive(false);
        }
    }*/
    /*public IEnumerator waitTriggerDisplayControl()
    {
        yield return new WaitForSeconds(1f);
        rightArrow.SetActive(true);
        leftArrow.SetActive(true);
        upArrow.SetActive(true);
        downArrow.SetActive(true);
*/

}

