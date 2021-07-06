using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileToward : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public bool isDwell = false;
    public GameObject point;
    public Vector3 origin;
    Animator animator;
    public Vector3 downorigin;
    public GameObject controlWebPrefab;
    public GameObject controlBuiltPrefab;
    public bool chosen = false;
    public float count;
    void Awake()
    {

    }
    void Start()
    {
        origin = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        point = GameObject.Find("point");

        animator = gameObject.GetComponent<Animator>();
        downorigin = new Vector3(0, -100f, 0);

        // Update is called once per frame
    }
    void Update()
    {
        GameObject initialization = GameObject.Find("Initialization");
        if (initialization.GetComponent<ConfigurationScript>().startnow == true)
        {


            GameObject control = GameObject.Find("ControlClass");
            //GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = isDwell;
            if (isDwell && control.GetComponent<animationControl>().Move == false) //&& control.GetComponent<animationControl>().isChosen == true)
            {

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, point.transform.localPosition, Time.deltaTime * speed);

                if (transform.localPosition == point.transform.localPosition && GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen == false)
                {
                    GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = true;
                    count = 0;
                    //StartCoroutine(instantiatecontrolPrefab());
                    while (count < 1)
                    {
                        count++;
                        // StartCoroutine(instantiatecontrolPrefab());
                        //Debug.Log(gameObject.tag);
                        //Debug.Log(gameObject.name);

                        //Instantiate the right controls for each type of App. 

                        if (gameObject.tag == "Web-App")
                        {

                            GameObject instantprefabcontrols = (GameObject)Instantiate(controlWebPrefab, gameObject.transform);
                            instantprefabcontrols.transform.name = "ControlsTilesPrefab";
                            Debug.Log("Instantiate Web");

                        }
                        if (gameObject.tag == "Built-in")
                        {

                            GameObject instantprefabcontrols = (GameObject)Instantiate(controlBuiltPrefab, gameObject.transform);
                            instantprefabcontrols.transform.name = "ControlsTilesPrefab";
                            Debug.Log("Instantiate Built");
                        }


                    }

                    //control.GetComponent<animationControl>().isChosen = false;
                    animator.SetBool("isDone", true);
                    //Debug.Log("Stop");


                }
            }
            if (isDwell == false && control.GetComponent<animationControl>().isChosen == false && control.GetComponent<animationControl>().Move == false)
            {

                if (GameObject.Find("PauseControl").GetComponent<PausePlay>().isDwell == false)
                {
                    gameObject.GetComponent<SphereCollider>().enabled = true;
                }

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, origin, Time.deltaTime * speed);

                try
                {

                    //transform.GetChild(2).gameObject.SetActive(false);
                    // StartCoroutine(destroyControlPrefab());
                    Destroy(transform.GetChild(2).gameObject);
                }
                catch
                {
                }
                if (transform.localPosition == origin)
                {
                    animator.SetBool("isDone", false);
                }


            }
            //FadeOut Tile Animation
            if (control.GetComponent<animationControl>().isChosen == true && isDwell == false && control.GetComponent<animationControl>().Move == false)
            {
                Animator thisAnimator = gameObject.GetComponent<Animator>();
                thisAnimator.SetBool("notChosen", true);
                //Debug.Log("Fade" + gameObject.transform.name);
            }
            //FadeIn Tile Animation
            if (control.GetComponent<animationControl>().isChosen == false && isDwell == false)
            {
                Animator thisAnimator = gameObject.GetComponent<Animator>();
                thisAnimator.SetBool("notChosen", false);
                //Debug.Log("Fadeback" + gameObject.transform.name);
            }
            if (isDwell && control.GetComponent<animationControl>().Move == true && control.GetComponent<animationControl>().movingGameObject == gameObject.transform.name)
            {

            }
            if (control.GetComponent<animationControl>().isChosen == false && control.GetComponent<animationControl>().movingGameObject != gameObject.transform.name && control.GetComponent<animationControl>().Move == true)
            {
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }


    }
    public IEnumerator instantiatecontrolPrefab()
    {

        GameObject instantprefabcontrols = (GameObject)Instantiate(controlWebPrefab, gameObject.transform);
        instantprefabcontrols.transform.name = "ControlsPrefab";

        Debug.Log("Instantiate");
        yield return true;
    }
    public void getDwell()
    {
        isDwell = !isDwell;
        // GameObject.Find("ControlClass").GetComponent<animationControl>().isChosen = isDwell;
        //count = 0;



    }
    IEnumerator destroyControlPrefab()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Destroying");
        try
        {
            Destroy(transform.GetChild(2).gameObject);
        }
        catch
        {

        }

    }
}
