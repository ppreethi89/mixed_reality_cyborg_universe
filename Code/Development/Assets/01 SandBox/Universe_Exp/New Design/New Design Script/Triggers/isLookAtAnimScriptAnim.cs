using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isLookAtAnimScriptAnim : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public bool isLookAt;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void isLookAtOn()
    {
        
        animator.SetBool("isLookAt", true);
        //StopAllCoroutines();
    }
    public void islookAtOff()
    {
        
                StartCoroutine(delayisLookAt());
               //animator.SetBool("isLookAt", false);
        
        
        
    }
    public void islookAtChildOn()
    {
        gameObject.transform.parent.GetComponent<Animator>().SetBool("isLookAt", true);
        
    }
    public void isLookAtChildOff()
    {
        /*try
        {
            if (gameObject.activeSelf == true)
            {
                StartCoroutine(delayisLookAtChild());
            }
            
        }
        catch
        {
            Debug.Log("Already Look Off");
        }*/


    }
    public IEnumerator delayisLookAt()
    {

        
            Debug.Log("Coroutine");
            yield return new WaitForSeconds(15f);
            animator.SetBool("isLookAt", false);
        
    }
        
    public IEnumerator delayisLookAtChild()
    {
        yield return new WaitForSeconds(2f);
        gameObject.transform.parent.GetComponent<Animator>().SetBool("isLookAt", false);
    }
}
