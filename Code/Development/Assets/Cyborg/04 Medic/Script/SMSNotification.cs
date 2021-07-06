using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Linq;
using System.Text;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;


public class SMSNotification : MonoBehaviour
{
    public TextMesh bpm;
    public TextMesh oxygen;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SendText()
    {
        //String phoneNumber = "+12675308382";
        String phoneNumber = EventHandlerScript.staticContactNo;
        String bpm_threshold_alert = threshold_calculation("bpm", Int32.Parse(bpm.text));

        String oxygen_threshold_alert = threshold_calculation("oxygen", Int32.Parse(oxygen.text.Replace("%", "")));

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;

        mail.From = new MailAddress("cyborg.psm@outlook.com");

        //See carrier destinations below
        //message.To.Add(new MailAddress("5551234568@txt.att.net"));

        mail.To.Add(new MailAddress(phoneNumber + "@txt.att.net"));
        mail.To.Add(new MailAddress(phoneNumber + "@mms.us.lycamobile.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@three.co.uk"));
        mail.To.Add(new MailAddress(phoneNumber + "@mms.ee.co.uk"));


        mail.Subject = "Urgent attention required - Health data";

        mail.Body = "URGENT ! Need immediate Attention.   The BPM value is " + bpm_threshold_alert + " at " + bpm.text + ". And the oxygen value is " + oxygen_threshold_alert + " at " + oxygen.text;


        SmtpServer.Port = 587;

        SmtpServer.Credentials = new System.Net.NetworkCredential("cyborg.psm@outlook.com", "Cyborg@PSM") as ICredentialsByHost; SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(mail);
    }

   
    public string threshold_calculation(String type, int value)
    {
        String threshold = "";
        if (type == "bpm")
        {
            if (value < 60)
            {
                threshold = "low";
            }
            else if (value > 100)
            {
                threshold = "high";
            }
            else
                threshold = "normal";
        }

        else if (type == "oxygen")
        {

            if (value < 93)
            {
                threshold = "low";
            }
            else
                threshold = "normal";

        }
        return threshold;

    }
}

