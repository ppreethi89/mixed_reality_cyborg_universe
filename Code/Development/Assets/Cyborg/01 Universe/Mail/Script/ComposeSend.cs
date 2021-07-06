using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ComposeSend : MonoBehaviour
{
    // Start is called before the first frame update
    AuthFlowUWR auth;
    [SerializeField]
    TMP_InputField recipient;

    [SerializeField]
    TMP_InputField subject;

    [SerializeField]
    TMP_InputField emailContent;

    void Start()
    {
        
    }
    public void SendEmail()
    {
        auth.SendEmail(subject.text, emailContent.text, recipient.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
