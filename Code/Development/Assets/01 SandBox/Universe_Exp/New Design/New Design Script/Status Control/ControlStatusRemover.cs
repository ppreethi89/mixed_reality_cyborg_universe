using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlStatusRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void offControlTrigger()
    {
        if (gameObject.transform.name == "Move")
        {
            GameObject.Find("ControlClass").GetComponent<animationControl>().Move = false;
            Debug.Log("Remove Move Status");
            
            string activeGameobject = GameObject.Find("ControlClass").GetComponent<animationControl>().movingGameObject;
            try
            {
                GameObject.Find(activeGameobject).GetComponent<Animator>().SetBool("isChosen", false);
                GameObject.Find(activeGameobject).GetComponent<TileToward>().isDwell = false;
            }
            catch
            {
                Debug.Log("Deactivating non-Tile Object");
            }
            GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = false;
            GameObject.Find("ControlClass").GetComponent<SavePosition>().startSavePos();

            Destroy(this.gameObject);
        }
    }
}
