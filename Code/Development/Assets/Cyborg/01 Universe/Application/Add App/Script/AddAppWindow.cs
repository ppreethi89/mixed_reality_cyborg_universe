using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAppWindow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject addwindowprefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showAddAppWindow()
    {
        GameObject parentUniverse = GameObject.Find("Universe");
        GameObject prefab = (GameObject)Instantiate(addwindowprefab);
        prefab.transform.SetParent(parentUniverse.transform, false);
        prefab.transform.localPosition = new Vector3(0, 0.71f, 9f);
        GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = true;
    }
}
