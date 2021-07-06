using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resize : MonoBehaviour
{
    public GameObject parentObj;
    private GameObject addButton;
    private GameObject miniButton;
    // Start is called before the first frame update
    void Start()
    {
       // parentObj = gameObject.transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resizeAdd()
    {
        parentObj.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void resizeMinus()
    {
        parentObj.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void showButton()
    {
       
    }

}
