using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveViewIdentifier : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject universeGameobject;
    public float activeIdentifier1;
    public float activeIdentifier2;
    public Material inactiveMat;
    public Material activeMat;
    public Color32 viewColor;
    public Color32 greyColor;
    Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        viewColor = new Color32(0, 252, 255, 255);
        greyColor = new Color32(156, 136, 136, 255);
    }

    // Update is called once per frame
    void Update()
    {
        /*universeGameobject = GameObject.Find("UniverseParentCont");
        float univRotate = universeGameobject.GetComponent<Transform>().rotation.eulerAngles.y;
        if (univRotate > 330 || univRotate < 30)
        {
           
            Debug.Log("Active is View0");
        }
        if (univRotate > 270 && univRotate < 329)
        {

            Debug.Log("Active is View1");
        }
        if (univRotate > 210 && univRotate < 269)
        {

            Debug.Log("Active is View2");
        }
        if (univRotate > 150 && univRotate < 209)
        {

            Debug.Log("Active is View3");
        }
        if (univRotate > 90 && univRotate < 149)
        {

            Debug.Log("Active is View4");
        }
        if (univRotate > 31 && univRotate < 89)
        {

            Debug.Log("Active is View5");
        }*/
        //activeView();
        IdentifyActive();
    }
  
    public void IdentifyActive()
    {
        universeGameobject = GameObject.Find("Universe");
        //float univRotate = universeGameobject.GetComponent<Transform>().rotation.eulerAngles.y;
        float univRotate = universeGameobject.GetComponent<Transform>().localRotation.eulerAngles.y;
        if (GameObject.Find("ControlClass").GetComponent<animationControl>().pause == true)
        {

        }
        if (univRotate > activeIdentifier1 && univRotate < activeIdentifier2 && GameObject.Find("ControlClass").GetComponent<animationControl>().pause == false)
        {
            //gameObject.GetComponentInChildren<MeshRenderer>().material = activeMat;
            gameObject.GetComponent<SpriteRenderer>().color = viewColor;
            //animator.SetBool("isViewActive", true);
            //Debug.Log("Active is " + gameObject.transform.name);
        }
        else
        {
            if (GameObject.Find("ControlClass").GetComponent<animationControl>().pause == true)
            {
                gameObject.GetComponent<SpriteRenderer>().color = greyColor;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }
            //gameObject.GetComponentInChildren<MeshRenderer>().material = inactiveMat;
           
            //gameObject.GetComponent<SpriteRenderer>().color = 
            //animator.SetBool("isViewActive", false);
            // gameObject.GetComponent<MeshRenderer>().material = inactiveMat;
        }
        if ((univRotate > activeIdentifier1 || univRotate < activeIdentifier2) && gameObject.transform.name == "View1Control" && GameObject.Find("ControlClass").GetComponent<animationControl>().pause == false)
        {
            // gameObject.GetComponent<MeshRenderer>().material = activeMat;
            //gameObject.GetComponentInChildren<MeshRenderer>().material = activeMat;
            gameObject.GetComponent<SpriteRenderer>().color = viewColor;
            //animator.SetBool("isViewActive", true);
            // Debug.Log("Active is " + gameObject.transform.name);
        }
    }
}
