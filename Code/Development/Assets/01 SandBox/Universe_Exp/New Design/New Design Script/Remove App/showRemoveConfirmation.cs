using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showRemoveConfirmation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject removePrefab;
    public GameObject controlclass;


    void Start()
    {
        controlclass = GameObject.Find("ControlClass");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showRemovePrefab()
    {
        
        gameObject.GetComponentInParent<TileToward>().isDwell = false;
        controlclass.GetComponent<animationControl>().isChosen = false;
        
        string parentobj = gameObject.transform.parent.parent.name;
        controlclass.GetComponent<animationControl>().removeGameObject = gameObject.transform.parent.parent.name;
        string removingGameobject = controlclass.GetComponent<animationControl>().removeGameObject;
        StartCoroutine(showprefabIEnumerator(parentobj));
    }
    IEnumerator showprefabIEnumerator(string appname)
    {
        GameObject prefab = (GameObject)Instantiate(removePrefab);
        prefab.transform.GetChild(1).GetComponent<TextMeshPro>().text = "Are you sure you want to remove " + appname + " ?";



        yield return true;
    }
}
