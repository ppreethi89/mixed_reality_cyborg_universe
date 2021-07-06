using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class AvatarSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject avatarPanel;
    public TMP_InputField avatarIPInputField;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowAvatarIPPanel()
    {
        avatarPanel.SetActive(true);
    }
    public void AvatarConfirm()
    {
        EventHandlerScript.avatarIPAddress = avatarIPInputField.text;
        avatarPanel.SetActive(false);
    }
    public void AvatarCancel()
    {
        avatarPanel.SetActive(false);
    }
}
