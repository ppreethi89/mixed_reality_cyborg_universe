using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTowardsPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public bool isDwell = false;
    public float count = 0;
    public GameObject point;
    public Vector3 origin;
    public bool onlookaway = false;
    Animator animator;
    public Vector3 downorigin;
    public GameObject controlPrefab;
    void Awake()
    {
        origin = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        Debug.Log(origin);
        point = GameObject.Find("point");
    }
    void Start()
    {

        animator = gameObject.GetComponent<Animator>();
        downorigin = new Vector3(0, -100f, 0);

        // Update is called once per frame
    }
    void Update()
    {
        GameObject control = GameObject.Find("ControlClass");
        if (isDwell && count%2 == 1 && onlookaway == true)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, point.transform.localPosition, Time.deltaTime * speed);
            Debug.Log("move");
            control.GetComponent<animationControl>().isChosen = true;
            if (transform.localPosition == point.transform.localPosition)
            {
                onlookaway = false;
                isDwell = false;
                Debug.Log(origin);
                animator.SetBool("isDone", true);
                Debug.Log("Stop");
                GameObject instantprefabcontrols = (GameObject)Instantiate(controlPrefab, gameObject.transform);
                instantprefabcontrols.transform.name = "ControlsPrefab";
               
            }
            
        }
        if (isDwell == true && count%2 == 0 && onlookaway == true )
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, origin, Time.deltaTime * speed);
            Debug.Log("back");
            try
            {

                Destroy(GameObject.Find("ControlsPrefab"));
            }
            catch
            {
                Debug.Log("Controls Already Destroyed");
            }
            control.GetComponent<animationControl>().isChosen = false;
            
            if (transform.localPosition == origin)
            {
                onlookaway = false;
                isDwell = false;
                Debug.Log("Stop");
                Debug.Log(origin);
                animator.SetBool("isDone", false);
            }
            
        }
        
        if (control.GetComponent<animationControl>().isChosen == true && isDwell == false && count % 2 == 0)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,downorigin , Time.deltaTime * (speed*2));
            if (transform.localPosition == downorigin)
            {
                
            }
        }
        if (control.GetComponent<animationControl>().isChosen == false && isDwell == false && count % 2 == 0 && control.GetComponent<animationControl>().Move == false)
        {
           
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, origin, Time.deltaTime * (speed * 2));
            
        }
        if (control.GetComponent<animationControl>().Move == true)
        {
            origin = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }


    }
    public void getDwell()
    {
        isDwell = true;
        onlookaway = true;
        count = count + 1;
       
    }
    public void getLookAway()
    {
        onlookaway = true;
    }
}
