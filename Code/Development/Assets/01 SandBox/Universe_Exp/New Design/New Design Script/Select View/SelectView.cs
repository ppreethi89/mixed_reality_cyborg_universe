using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectView : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject universeprefab;
    public float activeViewDegree;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void selectView()
    {
        universeprefab = GameObject.Find("Universe");
        universeprefab.transform.rotation = Quaternion.Euler(0, activeViewDegree, 0);
        Debug.Log("Rotate");
    }
}
