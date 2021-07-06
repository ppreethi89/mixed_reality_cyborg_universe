using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentAnimationTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public bool look = false;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isLookAt", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void parentAnimTrigger(bool isLookAt)
    {
        animator.SetBool("isLookAt", isLookAt);
       
    }
    public void AnimTrigger()
    {
        look = !look;
        animator.SetBool("isLookAt", look);
    }
}
