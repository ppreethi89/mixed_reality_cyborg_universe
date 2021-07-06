using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keyboardv2 : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField talkappinput; // talkapp
    public GameObject inputfield; //inputfield
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void intantiateKeyboardv2()
    {
        /* prefab = (GameObject)Instantiate(keyboardv2);
         prefab.transform.SetParent(gameObject.transform);

         prefab.GetComponentInChildren<Keyboardv2>().inputfield = this.gameObject;*/
        //GameObject.Find("TalkAppUpdated(Clone)").GetComponent<Keyboardv2>().inputfield = this.gameObject;
    }
    public void submit()
    {
        
        inputfield.GetComponent<TMP_InputField>().text = talkappinput.text;
        GameObject talkicon = GameObject.Find("TalkIcon");
        talkicon.GetComponent<InstantiateGameObject>().OnOffTalkApp();
        //Destroy(parent);
    }
    public void dwellInput()
    {
        GameObject.Find("TalkAppUpdated(Clone)").GetComponent<Keyboardv2>().inputfield = this.gameObject;
    }
}
