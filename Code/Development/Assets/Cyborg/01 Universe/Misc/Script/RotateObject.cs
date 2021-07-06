using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isPlaying;
    [SerializeField]
    float speed;

    [SerializeField]
    Vector3 direction;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            //transform.Rotate(Vector3.right * Time.deltaTime);
            transform.Rotate(new Vector3(direction.x, direction.y, direction.z) * (Time.deltaTime*speed));

        }

    }
}
