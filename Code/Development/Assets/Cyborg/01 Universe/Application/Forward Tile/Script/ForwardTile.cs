using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ForwardTile : MonoBehaviour
{
    // Uses defined set of state in CyborgEnums.cs
    public AppState thisAppState;

    //point destination of tile when forwarding
    [SerializeField]
    GameObject point;
    //speed of tile (forwawrd/backward)
    [SerializeField]
    float speed;

    //prefab for tile controls (back,open,move,move view, remove)
    [SerializeField]
    GameObject controls;
    //prefab for move controls (right,left,up,down)
    [SerializeField]
    GameObject moveControls;

    private bool isChosen = false;
    private Vector3 origin;
    void Start()
    {
        //disabled remove function for built-in applications
        if (gameObject.tag == "Built-in")
        {
            Destroy(controls.transform.GetChild(4).gameObject);
        }

        //since Vuplex Browser is not yet fully purchased, disabled first Open Function for Web Applications
        /*if (gameObject.tag == "Web-App")
        {
            Destroy(controls.transform.GetChild(1).gameObject);
        }*/

        //get original position as a reference when returning (after backAppTile() executed)
        origin = gameObject.transform.localPosition;
        point = GameObject.Find("point");
    }

    void changeOrigin()
    {
        //get original position as a reference when returning (during removeMoveAction())
        origin = this.gameObject.transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        switch (thisAppState)
        {
            case AppState.Moving:
                
                break;
            case AppState.Active:
                //Debug.Log("App Stop moving");
                gameObject.transform.localPosition = Vector3.MoveTowards(transform.localPosition, point.transform.localPosition, Time.deltaTime * speed); //go to point location
                //to show controls
                break;
            case AppState.InActive:
                controls.SetActive(false);//to hide controls
                gameObject.transform.localPosition = Vector3.MoveTowards(transform.localPosition, origin, Time.deltaTime * speed); //go back to original position (origin)
                break;  
        }
    }

    //set chosen tile to move tile to point
    public void isAppSelected()
    {
        isChosen = true;
        if (isChosen && thisAppState == AppState.InActive && EventHandlerScript.activeObj == null)
        {
            thisAppState = AppState.Active;
            StartCoroutine(showControls());

            //set the active Game Object in PersistentManager
            EventHandlerScript.activeObj = this.gameObject.transform.name;
        }

    }
    
    //set chosen tile to original position
    public void backAppTile()
    {
        isChosen = false;
        thisAppState = AppState.InActive;

        //set the active Game Object in Persistent Manager to null
        EventHandlerScript.activeObj = null;
        

    }
    // display controls with 1 sec delay
    IEnumerator showControls()
    {
        yield return new WaitForSeconds(1);
        controls.SetActive(true);
    }


    //set chosen app to inactive to return to original position then show control upon return
    public void moveTrigger()
    {
        thisAppState = AppState.InActive;
        

        StartCoroutine(showMoveControls());
    }

    //display move arrows
    IEnumerator showMoveControls()
    {

        yield return new WaitForSeconds(1.5f);
        thisAppState = AppState.Moving;
        moveControls.SetActive(true);
       
        ShowStatus.showStatus("Move");
        ShowStatus.consumeAction(removeMoveAction);

    }

    //to be passed to statusbar to initiate saving position as well as hiding the move controls
    public void removeMoveAction()
    {
        moveControls.SetActive(false);
        changeOrigin();
        thisAppState = AppState.InActive;

        //insert Save functionality here
        
        StartCoroutine(InitializeSave.SaveTransform(this.gameObject));
    }
    
}
