using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showAddConfirmBox : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject addConfirmBoxPrefab;
    public string addAppName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showAddConfirmBoxPrefab()
    {
        GameObject confirmPrefab = (GameObject)Instantiate(addConfirmBoxPrefab);
        addAppName = gameObject.transform.name;
        confirmPrefab.transform.GetChild(1).GetComponent<TextMeshPro>().text = "Are you sure you want to add " + addAppName + " ?";
       
        GameObject.Find("ControlClass").GetComponent<animationControl>().addGameObject = addAppName;
    }
}
