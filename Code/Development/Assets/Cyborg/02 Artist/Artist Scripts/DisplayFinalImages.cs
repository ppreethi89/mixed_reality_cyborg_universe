using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFinalImages : MonoBehaviour
{
    // Start is called before the first frame update
    private bool conatinerOccupied = false;
    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void emptyImageConatiner()
    {
        gameObject.GetComponent<RawImage>().texture = null;
        gameObject.GetComponent<RawImage>().enabled = false;
        conatinerOccupied = false;
    }

    public bool checkContainer() {

        return conatinerOccupied;    
    }

    public void fillImageContainer() {

        conatinerOccupied = true;
    }

}
