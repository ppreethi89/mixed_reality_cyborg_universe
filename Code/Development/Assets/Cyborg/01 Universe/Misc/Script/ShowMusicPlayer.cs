using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabGallery;
    public GameObject prefabArt;
    public GameObject prefabMusic;

    private GameObject parentobjGallery;
    private GameObject parentobjArt;
    private GameObject parentobjMusic;

    public GameObject controlclass;
    public GameObject universeprefab;

    void Start()
    {
        controlclass = GameObject.Find("ControlClass");
        parentobjGallery = GameObject.Find("View4");
        parentobjArt = GameObject.Find("View5");
        parentobjMusic = GameObject.Find("View2");

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void openGallery()
    {

        if (gameObject.transform.parent.parent.name == "View Gallery")
        {

            GameObject app = (GameObject)Instantiate(prefabGallery);
            app.transform.SetParent(parentobjGallery.transform, false);
            universeprefab = GameObject.Find("Universe");
            universeprefab.transform.rotation = Quaternion.Euler(0, 180, 0);
            gameObject.transform.parent.parent.GetComponent<TileToward>().isDwell = false;
            controlclass.GetComponent<animationControl>().isChosen = false;

        }
        else if (gameObject.transform.parent.parent.name == "Create Art")
        {
            GameObject app = (GameObject)Instantiate(prefabArt);
            app.transform.SetParent(parentobjArt.transform, false);
            universeprefab = GameObject.Find("Universe");
            universeprefab.transform.rotation = Quaternion.Euler(0, 135, 0);
            gameObject.transform.parent.parent.GetComponent<TileToward>().isDwell = false;
            controlclass.GetComponent<animationControl>().isChosen = false;
        }
        else if (gameObject.transform.parent.parent.name == "Music Player")
        {

            GameObject app = (GameObject)Instantiate(prefabMusic);
            app.transform.SetParent(parentobjGallery.transform, false);
            universeprefab = GameObject.Find("Universe");
            universeprefab.transform.rotation = Quaternion.Euler(0, 180, 0);
            gameObject.transform.parent.parent.GetComponent<TileToward>().isDwell = false;
            controlclass.GetComponent<animationControl>().isChosen = false;

        }

    }
}
