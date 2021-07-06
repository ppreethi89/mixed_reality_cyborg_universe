using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class SavedPeopleDeleteMode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite deleteSprite;
    [SerializeField] Sprite addSprite;


    [SerializeField] Color32 deleteColor = new Color32(209, 27, 9, 255);
    [SerializeField] Color32 normalColor = new Color32(255, 255, 255, 255);
    bool isDeleteModeOn;
    void Start()
    {
        EventHandlerScript.DeleteModeOn += deleteModeOn;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeleteModeOn)
        {
            this.gameObject.GetComponent<TextMeshPro>().color = deleteColor;
            //EventHandlerScript.removeModeActive = true;
        }
        else
        {
            //this.gameObject.GetComponent<SpriteRenderer>().sprite = addSprite;
            this.gameObject.GetComponent<TextMeshPro>().color = normalColor;
            //EventHandlerScript.removeModeActive = false;
        }
    }
    private void deleteModeOn(object sender, EventArgs e)
    {
        isDeleteModeOn = !isDeleteModeOn;
    }
}
