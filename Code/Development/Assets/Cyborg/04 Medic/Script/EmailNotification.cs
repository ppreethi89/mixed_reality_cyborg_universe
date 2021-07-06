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

public class EmailNotification : MonoBehaviour
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
    public void SendSmtpMail()
    {
        String bpm_threshold_alert = threshold_calculation("bpm", Int32.Parse(bpm.text));

        String oxygen_threshold_alert = threshold_calculation("oxygen", Int32.Parse(oxygen.text.Replace("%", "")));


        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("cyborg.psm@outlook.com");

        mail.To.Add("cyborg.psm@outlook.com");

        mail.Subject = "Urgent attention required - Health data";

        mail.Body = "URGENT ! Need immediate Attention.   The BPM value is " + bpm_threshold_alert + " at " + bpm.text + ". And the oxygen value is " + oxygen_threshold_alert + " at " + oxygen.text;

        // you can use others too.
        SmtpClient smtpServer = new SmtpClient("smtp.office365.com");

        smtpServer.Port = 587;

        smtpServer.Credentials = new System.Net.NetworkCredential("cyborg.psm@outlook.com", "Cyborg@PSM");

        smtpServer.EnableSsl = true;

        ServicePointManager.ServerCertificateValidationCallback =

        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)

        { return true; };

        smtpServer.Send(mail);

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
