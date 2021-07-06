using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GreyOutPause : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isDwell = false;
    //public Color32 greyColor;
    public List<Component> components;
    void Start()
    {
        //greyout();
        //greyColor = new Color32(156, 136, 136, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PausePlayGrey()
    {
        isDwell = !isDwell;

        if (isDwell)
        {
            GameObject[] gos;
            gos = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject go in gos)
            {
                try
                {
                    if (go.tag != "ParentControl")
                    {
                        if(go.GetComponent<RevertOriginalColor>() == null)
                        {
                            go.AddComponent<RevertOriginalColor>();
                        }
                        go.GetComponent<RevertOriginalColor>().originalColor = go.GetComponent<SpriteRenderer>().color;
                        go.GetComponent<SpriteRenderer>().color = new Color32(156, 136, 136, 255);
                         
                    }

                }
                catch
                {

                }

            }

            foreach (GameObject go in gos)
            {
                try
                {
                    if (go.tag != "ParentControl")
                    {
                        if (go.GetComponent<RevertOriginalColor>() == null)
                        {
                            go.AddComponent<RevertOriginalColor>();
                        }
                        try
                        {
                            go.GetComponent<RevertOriginalColor>().originalColor = go.GetComponent<MeshRenderer>().material.color;
                            go.GetComponent<MeshRenderer>().material.color = new Color32(156, 136, 136, 255);
                        }
                        catch
                        {

                        }
                        
                    }


                }
                catch
                {

                }

            }


            foreach (GameObject go in gos)
            {
                try
                {
                    if (go.tag != "ParentControl")
                    {
                        if (go.GetComponent<RevertOriginalColor>() == null)
                        {
                            go.AddComponent<RevertOriginalColor>();
                        }
                        try
                        {
                            
                            go.GetComponent<RevertOriginalColor>().originalColor = go.GetComponent<TextMeshPro>().color; ;
                            go.GetComponent<TextMeshPro>().color = new Color32(156, 136, 136, 255);
                        }
                        catch
                        {

                        }

                    }


                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    if (go.tag != "ParentControl")
                    {
                        if (go.GetComponent<RevertOriginalColor>() == null)
                        {
                            go.AddComponent<RevertOriginalColor>();
                        }
                        go.GetComponent<RevertOriginalColor>().originalColor = go.GetComponent<RawImage>().color;
                        go.GetComponent<RawImage>().color = new Color32(156, 136, 136, 255);

                    }

                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    if (go.tag != "ParentControl")
                    {
                        if (go.GetComponent<RevertOriginalColor>() == null)
                        {
                            go.AddComponent<RevertOriginalColor>();
                        }
                        go.GetComponent<RevertOriginalColor>().originalColor = go.GetComponent<Image>().color;
                        go.GetComponent<Image>().color = new Color32(156, 136, 136, 255);

                    }

                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    if (go.tag != "ParentControl")
                    {
                        if (go.GetComponent<RevertOriginalColor>() == null)
                        {
                            go.AddComponent<RevertOriginalColor>();
                        }
                        go.GetComponent<RevertOriginalColor>().originalColor = go.GetComponent<Text>().color;
                        go.GetComponent<Text>().color = new Color32(156, 136, 136, 255);

                    }

                }
                catch
                {

                }

            }
        }

        if (!isDwell)
        {
            GameObject[] gos;
            gos = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject go in gos)
            {
                try
                {
                    if (go.tag != "ParentControl")
                    {
                        go.GetComponent<SpriteRenderer>().color = go.GetComponent<RevertOriginalColor>().originalColor;
                        Destroy(go.GetComponent<RevertOriginalColor>());
                    }

                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    if (go.tag != "ParentControl")
                    {
                        try
                        {
                            go.GetComponent<MeshRenderer>().material.color = go.GetComponent<RevertOriginalColor>().originalColor;
                            Destroy(go.GetComponent<RevertOriginalColor>());
                        }
                        catch
                        {

                        }
                    }

                }
                catch
                {

                }

            }

            foreach (GameObject go in gos)
            {
                try
                {
                    if (go.tag != "ParentControl")
                    {
                        try
                        {
                            go.GetComponent<TextMeshPro>().color = go.GetComponent<RevertOriginalColor>().originalColor;
                            Destroy(go.GetComponent<RevertOriginalColor>());
                        }
                        catch
                        {

                        }
                    }

                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    if (go.tag != "ParentControl")
                    {
                        try
                        {
                            go.GetComponent<RawImage>().color = go.GetComponent<RevertOriginalColor>().originalColor;
                            Destroy(go.GetComponent<RevertOriginalColor>());
                        }
                        catch
                        {

                        }
                    }

                }
                catch
                {

                }

            }

            foreach (GameObject go in gos)
            {
                try
                {
                    if (go.tag != "ParentControl")
                    {
                        try
                        {
                            go.GetComponent<Image>().color = go.GetComponent<RevertOriginalColor>().originalColor;
                            Destroy(go.GetComponent<RevertOriginalColor>());
                        }
                        catch
                        {

                        }
                    }

                }
                catch
                {

                }

            }
            foreach (GameObject go in gos)
            {
                try
                {
                    if (go.tag != "ParentControl")
                    {
                        try
                        {
                            go.GetComponent<Text>().color = go.GetComponent<RevertOriginalColor>().originalColor;
                            Destroy(go.GetComponent<RevertOriginalColor>());
                        }
                        catch
                        {

                        }
                    }

                }
                catch
                {

                }

            }
        }
    }

}
