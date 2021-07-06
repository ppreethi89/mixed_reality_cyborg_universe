using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButtonStyle_General : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject selectedButtonStyle;
    [SerializeField]
    GameObject unselectedButtonStyle;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void selectedStyleOn() {

        unselectedButtonStyle.SetActive(false);
        selectedButtonStyle.SetActive(true);
    }

    public void selectStyleOff() {

        selectedButtonStyle.SetActive(false);
        unselectedButtonStyle.SetActive(true);
    
    }
}
