using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parentObject;
    private string message;
    void Start()
    {
        //message to be pass to the confirmation box
        message = "Are you sure you want to destroy " + parentObject.transform.name + " ?";
    }
    public void initializeRemoveApp()
    {
        //set the tile to inactive to go back to original position
        parentObject.GetComponent<ForwardTile>().thisAppState = AppState.InActive;

        //call the confirmation box and passed the (action/method , message)
        ConfirmationBox_General.showConfirmation(removeApp, message);

    }
    public void removeApp()
    {
        StartCoroutine(InitializeSave.SaveRemoveApp(parentObject));
        Destroy(parentObject);
        AlertBox_General.ShowAlertBox("App successfully removed!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
