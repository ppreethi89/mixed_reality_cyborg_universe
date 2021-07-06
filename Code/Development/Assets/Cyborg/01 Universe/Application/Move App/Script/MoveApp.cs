using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveApp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject parentObject;
    [SerializeField]
    GameObject moveControls;
    [SerializeField]
    float speed = 0.05f;

    bool isActive = false;
    Vector3 targetPosition;

    
   
    
    public moveState changeState;
    void Start()
    {
        changeState = moveState.notLooking;
    }
    public void showHideMoveControls()
    {
        isActive = !isActive;
        if (isActive)
        {
            moveControls.SetActive(true);
        }
        else
        {
            moveControls.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (changeState)
        {
            case moveState.left:
                targetPosition = new Vector3(parentObject.transform.position.x - 0.03f, parentObject.transform.position.y, parentObject.transform.position.z);
                transform.position = Vector3.MoveTowards(parentObject.transform.position, targetPosition, speed);
                break;
            case moveState.right:
                targetPosition = new Vector3(parentObject.transform.position.x + 0.03f, parentObject.transform.position.y, parentObject.transform.position.z);
                transform.position = Vector3.MoveTowards(parentObject.transform.position, targetPosition, speed);
                break;
            case moveState.up:
                targetPosition = new Vector3(parentObject.transform.position.x, parentObject.transform.position.y + 0.03f, parentObject.transform.position.z);
                transform.position = Vector3.MoveTowards(parentObject.transform.position, targetPosition, speed);
                break;
            case moveState.down:
                targetPosition = new Vector3(parentObject.transform.position.x, parentObject.transform.position.y - 0.03f, parentObject.transform.position.z);
                transform.position = Vector3.MoveTowards(parentObject.transform.position, targetPosition, speed);
                break;
        }
            
        
    }
    public void lookAway()
    {
        changeState = moveState.notLooking;
       

    }
    public void moveLeft()
    {
        changeState = moveState.left;
        
    }
    public void moveRight()
    {
        changeState = moveState.right;
       
    }
    public void moveUp()
    {
        changeState = moveState.up;
       

    }
    public void moveDown()
    {
        changeState = moveState.down;
        

    }
}
