using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class isDwellScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isDwell = false;
    public GameObject[] parentObject; //, parentObject2, parentObject3, parentObject4;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ParentDwelling()
    {
        isDwell = !isDwell;
        transform.GetComponentInParent<parentAnimationTrigger>().parentAnimTrigger(isDwell);
    }
    public void DwellMin()
    {
        isDwell = !isDwell;

        try
        {
            foreach (GameObject g in parentObject)
            {
                Animator animator = g.GetComponent<Animator>();
                animator.SetBool("isDwell", isDwell);
            }
        }
        catch
        {

        }
        

        /*Animator animator = parentObject.GetComponent<Animator>();
        animator.SetBool("isDwell", isDwell);

        Animator animator2 = parentObject2.GetComponent<Animator>();
        animator2.SetBool("isDwell", isDwell);

        Animator animator3 = parentObject3.GetComponent<Animator>();
        animator3.SetBool("isDwell", isDwell);

        Animator animator4 = parentObject4.GetComponent<Animator>();
        animator4.SetBool("isDwell", isDwell);*/
        /*try
        {
            Animator animator2 = parentObject2.GetComponent<Animator>();
            animator2.SetBool("isDwell", isDwell);
        }
        catch{}*/
    }
}
