using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOtherSpeech : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject appGameObject;
    public GameObject prefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Open()
    {
       GameObject app = (GameObject)Instantiate(prefab);
       app.transform.SetParent(appGameObject.transform, false);
    }
    public void Close()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
