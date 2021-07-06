using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PreInitializeUniverse : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> univGameObjects;
    [SerializeField]
    float delay;

    [SerializeField]
    bool intro;

    private void Awake()
    {
        if (intro)
        {
            foreach (GameObject go in univGameObjects)
            {
                go.SetActive(false);
            }
        }
       
       
    }
    public void Start()
    {
        StartCoroutine(StartEffect());
    }
    public IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Showing");
        
        foreach (GameObject go in univGameObjects)
        {
            go.SetActive(true);
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
