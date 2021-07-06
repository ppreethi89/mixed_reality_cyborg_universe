using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class animationControl : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isChosen = false;
    public bool Move = false;
    public bool Resize = false;
    public bool MoveToAnotherView = false;
    public GameObject showcontrolStatus;
    public string movingGameObject;
    public bool Remove = false;
    public string removeGameObject;
    public string addGameObject;
    public string movingViewGameObject;
    public string url;
    public bool IsOnTalkApp;
    public bool pause;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void trigger(string control, string parentTile)
    {
        
        StartCoroutine(waittrigger(control, parentTile));
        
    }
    public IEnumerator waittrigger(string control2, string parentTile2)
    {
        yield return new WaitForSeconds(1);
        if (control2 == "Move")
        {
            //yield return new WaitForSeconds(2);
            GameObject.Find("ControlClass").GetComponent<animationControl>().Move = true;
            GameObject.Find("ControlClass").GetComponent<animationControl>().movingGameObject = parentTile2;
            /*    GameObject.Find(control2).transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 149, 255);
                GameObject.Find(control2).transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 149, 255);*/
            Debug.Log(parentTile2);
            try
            {
                GameObject.Find(parentTile2).GetComponent<Animator>().SetBool("isChosen", true);

            }
            catch
            {

            }
            GameObject instantprefabcontrols = (GameObject)Instantiate(showcontrolStatus);
            instantprefabcontrols.transform.name = "Move";
            instantprefabcontrols.transform.GetComponentInChildren<TextMeshPro>().text = "Move";

        }
        else
        {
            GameObject.Find("ControlClass").GetComponent<animationControl>().Move = false;
            GameObject.Find(parentTile2).GetComponent<Animator>().SetBool("isChosen", false);
        }
        
    }
}
