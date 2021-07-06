using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject prefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void instantiatePrefab()
    {
        prefab.SetActive(true);
    }
}
