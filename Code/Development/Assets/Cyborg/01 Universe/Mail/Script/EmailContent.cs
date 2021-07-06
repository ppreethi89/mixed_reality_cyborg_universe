using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class EmailContent : MonoBehaviour
{
    // Start is called before the first frame update
    public string emailContentBody;
    public GameObject emailGameObject;
    public string emailSubject;
    public string emailFrom;
    public List<string> emailReplyTo;
    public List<string> emailRecipient;
    public string emailSender;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DwellViewContentEmail()
    {
        StaticViewEmailBody.OpenStaticViewEmailPrefab(emailContentBody);
        StaticViewEmailBody.recipientField = emailFrom;
        StaticViewEmailBody.replySubject = emailSubject;
        StaticViewEmailBody.emailContent = emailContentBody;
        //Debug.Log(emailContentBody);
    }
}
