using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInstantiate_General : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject objectToDestroy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyInstantiate()
    {
        Destroy(objectToDestroy);
    }
}
