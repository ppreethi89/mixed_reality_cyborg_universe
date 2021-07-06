using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab_General : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Instantiate()
    {
        GameObject app = (GameObject)Instantiate(prefab);
        app.transform.SetParent(parentObject.transform, false);
    }
}
