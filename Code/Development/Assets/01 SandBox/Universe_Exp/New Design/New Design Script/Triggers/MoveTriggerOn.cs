using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTriggerOn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject showcontrolprefab;
    public GameObject controlclass;
    private float waitTime = 2;
    public string controlname;

    void Start()
    {
        controlclass = GameObject.Find("ControlClass");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showControl()
    {
        try
        {
            gameObject.GetComponentInParent<TileToward>().isDwell = false;
            
        }
        catch
        {

        }

        Debug.Log("Okay");
        controlclass.GetComponent<animationControl>().isChosen = false;
        string parentobj = gameObject.transform.parent.parent.name;
        GameObject.Find("ControlClass").GetComponent<animationControl>().trigger(gameObject.transform.name,parentobj);
        //GameObject.Find("ControlClass").GetComponent<animationControl>().Move = true;
        Debug.Log(gameObject.transform.name);
        //controlname = gameObject.transform.name;
        //StartCoroutine(waitControlTrigger(controlname));
        



    }
    IEnumerator waitControlTrigger(string control)
    {
        //yield return true;

        // yield return seconds;
        yield return new WaitForSeconds(0.3f);
        
        yield return new WaitForSeconds(2);
        activateMoveTrigger(control);
        
        

        
    }
    public void activateMoveTrigger(string control)
    {
        
        if (control == "Move")
        {
            GameObject instantprefabcontrols = (GameObject)Instantiate(showcontrolprefab);
            instantprefabcontrols.transform.name = "Move";

            try
            {
                gameObject.GetComponentInParent<TileToward>().isDwell = false;
            }
            catch
            {

            }
            controlclass.GetComponent<animationControl>().isChosen = false;
            GameObject.Find("ControlClass").GetComponent<animationControl>().Move = true;
            Debug.Log("MoveOn");
            //GameObject.Find("ControlClass").GetComponent<animationControl>().trigger(gameObject.transform.name);

        }


    }
}
