using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isdwelltriggerexamp : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void dwellAnim()
    {
        animator.SetBool("isDwell", true);
    }
}
