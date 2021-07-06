using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class GenerateQuickReplies : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject quickReply1;
    public GameObject quickReply2;
    public GameObject quickReply3;
    public GameObject quickReplyInputField;
    public List<string> replies;
    public AuthFlowUWR UWRFlow;

    public GameObject quickReplyRecipientField;
    public GameObject quickReplySubjectField;

    void Start()
    {       
    }
    public void shuffleResponses() {
        replies = QuickReplies.GetRandomResponses();
        quickReply1.GetComponentInChildren<TextMeshPro>().text = replies[0];
        quickReply2.GetComponentInChildren<TextMeshPro>().text = replies[1];
        quickReply3.GetComponentInChildren<TextMeshPro>().text = replies[2];
    }
    private void OnEnable()
    {
        quickReplyInputField.GetComponent<TMP_InputField>().text = "";
        quickReplyRecipientField.GetComponent<TextMeshPro>().text = StaticViewEmailBody.recipientField;
        quickReplySubjectField.GetComponent<TextMeshPro>().text = StaticViewEmailBody.replySubject;
        replies = QuickReplies.GetRandomResponses();
        quickReply1.GetComponentInChildren<TextMeshPro>().text = replies[0];
        quickReply2.GetComponentInChildren<TextMeshPro>().text = replies[1];
        quickReply3.GetComponentInChildren<TextMeshPro>().text = replies[2];

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void AddReply(GameObject quickReply)
    {
        //quickReplyInputField.GetComponent<TMP_InputField>().text += quickReply.GetComponentInChildren<TextMeshPro>().text;
        quickReplyInputField.GetComponent<TMP_InputField>().text = "";
        quickReplyInputField.GetComponent<TMP_InputField>().text += quickReply.GetComponentInChildren<TextMeshPro>().text;
    }
    public void SendEmail()
    {
        ConfirmationBox_General.showConfirmation(SendConfirmEmail, "Do you want send your message?");
    }
    public void SendConfirmEmail()
    {
        string recipient = StaticViewEmailBody.recipientField;
        string subject = StaticViewEmailBody.replySubject;
        string content = quickReplyInputField.GetComponent<TMP_InputField>().text;
        Debug.Log($"EmailContent is :{StaticViewEmailBody.emailContent}");
        Debug.Log($" Recipient is :{recipient}");
        Debug.Log($"Subject is :{subject}");
        Debug.Log($"Content is :{content}");
        UWRFlow.SendEmail(subject, content, recipient, replyThread: true);
        
        gameObject.SetActive(false);
    }

    public void CloseCurrentWindow() {
        if (!string.IsNullOrEmpty(quickReplyInputField.GetComponent<TMP_InputField>().text))
        {
            ConfirmationBox_General.showConfirmation(CloseWindow, "Do you want to discard your reply?");
        }
        else
        {
            CloseWindow();
        }
        
    }
    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

}
