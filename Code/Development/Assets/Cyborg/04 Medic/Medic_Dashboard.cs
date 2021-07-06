using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic_Dashboard : MonoBehaviour
{
    // Start is called before the first frame update
       [SerializeField]
    GameObject firstparentToActivate;
        [SerializeField]
    GameObject secondparentToActivate;
    [SerializeField]
    GameObject firstparentToHide;
     [SerializeField]
    GameObject secondparentToHide;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void setActiveTransition()
    {
        firstparentToActivate.SetActive(true);
        secondparentToActivate.SetActive(true);
        firstparentToHide.SetActive(false);
        secondparentToHide.SetActive(false);
    }
}
