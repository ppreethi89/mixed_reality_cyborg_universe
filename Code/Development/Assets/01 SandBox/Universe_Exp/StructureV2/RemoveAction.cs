using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.Tilemaps;

public class RemoveAction : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject controlPrefab;

    [SerializeField]
    GameObject confirmationBox;

    [SerializeField]
    GameObject statusPrefab;
    
    public bool isMove;
    public string control;
    private Vector3 HitPosition;
    private Vector3 HitPostPosition;

    private float speed = 5f;
    void Start()
    {
       
    }
    public void Moving()
    {
        if (isMove)
        {

            HitPosition = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GazeProvider>().HitPosition;
            float step = speed * Time.deltaTime;
            //m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            //m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
            HitPostPosition = new Vector3(HitPosition.x, HitPosition.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, HitPostPosition, step);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void showRemove()
    {
        GameObject prefab = (GameObject)Instantiate(confirmationBox);
        prefab.transform.GetChild(0).GetChild(1).GetComponent<ActionTest>().action = removeThis;
    }
    public void removeThis()
    {
        Destroy(this.gameObject);
        Debug.Log(this.gameObject.transform.name);

        //StartCoroutine(Hello());
    }
    public void showStatusBar(string control)
    {
        GameObject prefab = (GameObject)Instantiate(statusPrefab);
        prefab.GetComponent<ActionTest>().action = disableMove;
        prefab.GetComponentInChildren<TextMeshPro>().text = control;
    }
    public void initializeMove()
    {
        showStatusBar("Move");
        isMove = true;
    }
    public void disableMove()
    {
        isMove = false;
        TileTest.ClearActives();
    }

    public void rotateThis()
    {
        this.gameObject.transform.Rotate(30f, 30f, 30f);
    }
    public void transferActions()
    {
        if (isMove == false && TileTest.activeObj == null)
        {
            TileTest.activeObj = this.gameObject;
            TileTest.isActiveControl = true;
            GameObject prefab = (GameObject)Instantiate(controlPrefab);
            prefab.transform.GetChild(0).GetComponent<Controls>().action = showRemove;
            prefab.transform.GetChild(1).GetComponent<Controls>().action = initializeMove;
            prefab.transform.GetChild(2).GetComponent<Controls>().action = rotateThis;
        }
        

    }

}

