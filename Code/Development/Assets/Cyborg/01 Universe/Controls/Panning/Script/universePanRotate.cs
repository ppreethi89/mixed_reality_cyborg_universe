using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using System;

public class universePanRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject universe;
    public float speed = 0.5f;
    public float viewDegree = 0;
    public string direction;
    public string islook = "notlook";
    [SerializeField]
    GameObject rightIcon;
    [SerializeField]
    GameObject leftIcon;
    
    
    void Start()
    {
       // universe = GameObject.Find("Universe");
    }

    // Update is called once per frame
    void Update()
    {
        if (islook == "lookright")
        {
            /*universe.transform.Rotate(0f, -speed, 0f);
            rightIcon.SetActive(true);
            viewDegree += speed;
            Debug.Log(viewDegree);*/
            direction = "right";
            if (viewDegree < 45)
            {
                universe.transform.Rotate(0f, -speed, 0f);
                rightIcon.SetActive(true);
                viewDegree += speed;
                Debug.Log(viewDegree);
            }
            else
            {
                rightIcon.SetActive(false);
            }
        }
        if (islook == "lookleft")
        {
            direction = "left";
            if (viewDegree < 45)
            {
                universe.transform.Rotate(0f, speed, 0f);
                leftIcon.SetActive(true);
                viewDegree += speed;
                Debug.Log(viewDegree);
            }
            else
            {
                leftIcon.SetActive(false);
            }
        }
        if (islook == "notlook")
        {
            if (viewDegree < 45 && direction == "right")
            {
                universe.transform.Rotate(0f, -speed, 0f);
                viewDegree += speed;
                Debug.Log(viewDegree);
            }
            else if (viewDegree < 45 && direction == "left")
            {
                universe.transform.Rotate(0f, speed, 0f);
                viewDegree += speed;
                Debug.Log(viewDegree);
            }
            else
            {
                viewDegree = 0;
                direction = "";
            }

        }
        

    }
    public void rotateright()
    {
        islook = "lookright";
        //gameObject.GetComponent<SpriteRenderer>().color.a 
        //gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 162, 255, 255);
    }
    public void rotateleft()
    {
        islook = "lookleft";
        //gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 162, 255, 255);
    }
    public void notlook()
    {
        islook = "notlook";
        //gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 226, 255);
        /*if (viewDegree < 45)
        {
            universe.transform.Rotate(0f, -speed, 0f);
            viewDegree += speed;
            Debug.Log(viewDegree);
            Update();
        }
        else
        {
            viewDegree = 0;
            direction = "";
        }*/
        rightIcon.SetActive(false);
        leftIcon.SetActive(false);
    }

}