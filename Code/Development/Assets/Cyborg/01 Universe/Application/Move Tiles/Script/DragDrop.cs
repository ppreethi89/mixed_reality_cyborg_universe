using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.IO;
using UnityEngine.Serialization;
using Microsoft.MixedReality.Toolkit.Physics;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityPhysics = UnityEngine.Physics;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.SceneManagement;

public class DragDrop : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 HitPosition;
    public Boolean IsHit = false;
    public float speed = 5f;
    public Rigidbody m_Rigidbody;
    public Vector3 HitPostPosition;
    public Boolean moveTrigger;

    void Start(){}

    // Update is called once per frame
    void Update()
    {
        
       // m_Rigidbody = this.GetComponent<Rigidbody>();
 
        if (GameObject.Find("ControlClass").GetComponent<animationControl>().Move == true && IsHit == true)
        {
            HitPosition = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GazeProvider>().HitPosition;
            float step = speed * Time.deltaTime;
            //m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            //m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
            HitPostPosition = new Vector3(HitPosition.x, HitPosition.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, HitPostPosition, step);
            gameObject.GetComponent<TileToward>().origin = transform.localPosition;
        }

    }

    public void hitcast()
    {
        
            IsHit = true;
            //Debug.Log("It is being hit by ray cast");
        
    }

    public void hitoff()
    {
        IsHit = false;
    }
}