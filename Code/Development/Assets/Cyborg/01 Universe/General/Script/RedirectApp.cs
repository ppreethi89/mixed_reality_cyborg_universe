using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RedirectApp : MonoBehaviour
{
    // Start is called before the first frame update
    public string identifier;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DwellNotif()
    {
        switch (identifier)
        {
            case "Cyborg Artist":
                {
                    try
                    {
                        EventHandlerScript.activeApp.SetActive(false);
                    }
                    catch
                    {
                        Debug.Log("No Active App Selected");
                    }
                    
                    EventHandlerScript.activeObj = null;
                    ArtistInfo.AppSelected = "Create Art";
                    Debug.Log(ArtistInfo.AppSelected);
                    EventHandlerScript.OpenCyborgProjectApp("Create Art");
                    

                    Destroy(this.gameObject);

                    break;
                }
        }
    }
}
