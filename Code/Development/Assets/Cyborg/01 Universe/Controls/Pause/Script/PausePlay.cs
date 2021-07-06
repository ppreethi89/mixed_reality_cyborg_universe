using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PausePlay : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isDwell = false;
    public Material matPause;
    public Material matPlay;
    public Sprite spritePause;
    public Sprite spritePlay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pauseplay()
    {
        isDwell = !isDwell;
        if (isDwell)
        {
            GameObject.Find("ControlClass").GetComponent<animationControl>().pause = true;
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = matPause;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spritePause;
            gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().text = "Play";

            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("SubControl");
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<BoxCollider>().enabled = false;

                }
                catch
                {
                    
                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<SphereCollider>().enabled = false;
                }
                catch
                {

                }
                
            }
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<MeshCollider>().enabled = false;
                }
                catch
                {

                }

            }




           
            gos = GameObject.FindGameObjectsWithTag("Web-App");
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<BoxCollider>().enabled = false;

                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<SphereCollider>().enabled = false;
                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<MeshCollider>().enabled = false;
                }
                catch
                {

                }

            }


            gos = GameObject.FindGameObjectsWithTag("Built-in");
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<BoxCollider>().enabled = false;

                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<SphereCollider>().enabled = false;
                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<MeshCollider>().enabled = false;
                }
                catch
                {

                }

            }
        }
           
        else
        {
                GameObject.Find("ControlClass").GetComponent<animationControl>().pause = false;
                GameObject[] gos;
                gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = matPlay;
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spritePlay;
                gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().text = "Pause";
                gos = GameObject.FindGameObjectsWithTag("SubControl");
                foreach (GameObject go in gos)
                {
                    try
                    {
                        go.GetComponent<BoxCollider>().enabled = true;

                    }
                    catch
                    {

                    }

                }
                foreach (GameObject go in gos)
                {
                    try
                    {
                        go.GetComponent<SphereCollider>().enabled = true;
                    }
                    catch
                    {

                    }

                }
                foreach (GameObject go in gos)
                {
                    try
                    {
                        go.GetComponent<MeshCollider>().enabled = true;
                    }
                    catch
                    {

                    }

                }

            gos = GameObject.FindGameObjectsWithTag("Web-App");
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<BoxCollider>().enabled = true;

                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<SphereCollider>().enabled = true;
                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<MeshCollider>().enabled = true;
                }
                catch
                {

                }

            }



            gos = GameObject.FindGameObjectsWithTag("Built-in");
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<BoxCollider>().enabled = true;

                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<SphereCollider>().enabled = true;
                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    go.GetComponent<MeshCollider>().enabled = true;
                }
                catch
                {

                }

            }
        }
        
    }
}
