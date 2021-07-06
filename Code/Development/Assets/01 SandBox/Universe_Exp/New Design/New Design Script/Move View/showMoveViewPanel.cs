using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class showMoveViewPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject moveViewPanelprefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showMoveViewPrefab()
    {
        gameObject.GetComponentInParent<TileToward>().isDwell = false;
        GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = false;
        string parentobj = gameObject.transform.parent.parent.name;
        GameObject.Find("ControlClass").GetComponent<animationControl>().movingViewGameObject = parentobj;

        string movingViewApp = GameObject.Find("ControlClass").GetComponent<animationControl>().movingViewGameObject;
        GameObject prefab = (GameObject)Instantiate(moveViewPanelprefab);
        prefab.GetComponentInChildren<TextMeshPro>().text = "Where do you want to move the " + movingViewApp + " ?";

    }
}
