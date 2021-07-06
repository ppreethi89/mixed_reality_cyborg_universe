using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraLookAt : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float speedPosition = 1f, speedRotation = 1f;
    public Vector3 difference;

    // Use this for initialization
    void Start()
    {

        difference = target.position - transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position - difference, speedPosition * Time.deltaTime);;
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, speedRotation * Time.deltaTime);
    }
}
