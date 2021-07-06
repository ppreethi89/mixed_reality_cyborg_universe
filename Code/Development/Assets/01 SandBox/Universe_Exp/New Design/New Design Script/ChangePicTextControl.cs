using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangePicTextControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite sprite1;
    public string text1;
    public Material mat1;

    public Sprite sprite2;
    public string text2;
    public Material mat2;

    public bool isDwell = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changestatus()
    {
        isDwell = !isDwell;
        if (isDwell)
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite2;
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = text2;
            gameObject.transform.GetChild(3).GetComponent<MeshRenderer>().material = mat2;

        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite1;
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = text1;
            gameObject.transform.GetChild(3).GetComponent<MeshRenderer>().material = mat1;
        }
    }

}
