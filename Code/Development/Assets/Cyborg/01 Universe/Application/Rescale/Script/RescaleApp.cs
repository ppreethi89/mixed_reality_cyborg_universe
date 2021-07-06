using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescaleApp : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject parentObject;

    [SerializeField]
    float speed = 0.05f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void scaleLeft()
    {
        parentObject.transform.localScale += new Vector3(-0.03f, 0f, 0f);
        parentObject.transform.position += new Vector3(-0.03f, 0f, 0f);
    }
    public void scaleRight()
    {
        parentObject.transform.localScale += new Vector3(0.03f, 0f, 0f);
        parentObject.transform.position += new Vector3(0.03f, 0f, 0f);
    }
    public void scaleUp()
    {
        parentObject.transform.localScale += new Vector3(0, 0.03f, 0f);
        parentObject.transform.position -= new Vector3(0, 0.03f, 0f);
    }
    public void scaleDown()
    {
        parentObject.transform.localScale += new Vector3(0f, -0.03f, 0f);
        parentObject.transform.position -= new Vector3(0f, -0.03f, 0f);
    }
}
