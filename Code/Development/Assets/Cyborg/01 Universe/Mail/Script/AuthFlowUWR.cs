using Newtonsoft.Json;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class AuthFlowUWR : MonoBehaviour
{

    string client_id;
    string scopes;
    string urlParameters;
    string deviceCodeEndpoint;
    string getTokenEndpoint;
    string refreshToken;
    string accessToken;
    float expiryTime;
    bool _startPolling;
    float startTimer;
    string deviceCode;
    string refTokenEndpoint;
    string sendEmailEndpoint;
    bool noCode;
    float emailTimer;
    bool emailsReady;
    public GameObject prefab;
    public TextMeshProUGUI textMeshPro;
    private float emailRefreshTime;
    private Dictionary<string, string> map;
    private float cushionTime;
    private List<GameObject> gcList;
    private string APPOINTMENTS_URL;




    // Start is called before the first frame update

    void Start()
    {
        initParameters();
        checkIfTokenExists();
        //Logout();
        /*
        if(deviceCode=="")
            deviceCodeAuthAsync();        
        */
    }

    private IEnumerator TokenExistenceResponse(UnityWebRequest request)
    {
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.responseCode != 200)
        {
            Debug.Log("Something went wrong");
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Before removing slashes etc..,");
            Debug.Log(jsonResponse);
            jsonResponse = jsonResponse.Replace("\"", "");
            jsonResponse = jsonResponse.Replace("'", "\"");
            jsonResponse = jsonResponse.Replace(" ", "");
            Debug.Log(jsonResponse);
            var codeResponse = JSON.Parse(jsonResponse);
            refreshToken = codeResponse["refreshToken"].Value;
            if (refreshToken == "")
            {
                Debug.Log("Here in refreshToken==\"\"");
                deviceCodeAuthAsync();
            }
            if (!_startPolling)
                StartCoroutine(getNewTokenWithRefreshToken());

        }
    }
    private void checkIfTokenExists()
    {

        var request = UnityWebRequest.Get(refTokenEndpoint);
        StartCoroutine(TokenExistenceResponse(request));
       
    }
    private void initParameters()
    {
        map = new Dictionary<string, string>();
        client_id = KeyProvider.CLIENT_ID;
        scopes = KeyProvider.scopes;
        //scopes = "offline_access User.read mail.read Calendars.Read Calendars.ReadWrite";
        urlParameters = "";
        deviceCodeEndpoint = $"https://login.microsoftonline.com/consumers/oauth2/v2.0/devicecode";
        //deviceCodeEndpoint = $"https://login.microsoftonline.com/common/oauth2/v2.0/devicecode";
        //getTokenEndpoint = $"https://login.microsoftonline.com/common/oauth2/v2.0/token";
        getTokenEndpoint = $"https://login.microsoftonline.com/consumers/oauth2/v2.0/token";
        _startPolling = false;
        startTimer = 0.0f;
        deviceCode = "";
        //refTokenEndpoint = $"http://localhost:8081/accessToken"; //CHANGE THIS LATER!!!
        //refTokenEndpoint = $"https://outlookjwtendpoint.herokuapp.com/accessToken";
        refTokenEndpoint = $"https://cyborgartist.azurewebsites.net/refreshToken";
        noCode = false;
        refreshToken = "";
        emailTimer = 0.0f;
        emailsReady = false;
        //emailsReady = true;
        emailRefreshTime = 65.0f;
        //For Debug enable below
        //emailRefreshTime = 65.0f;
        cushionTime = 5.8f;
        gcList = new List<GameObject>();
        sendEmailEndpoint = "https://graph.microsoft.com/v1.0/me/sendMail";
        APPOINTMENTS_URL = "https://graph.microsoft.com/v1.0/me/events?$select=subject,body,bodyPreview,organizer,attendees,start,end,location";
    }
    public string getAccessToken()
    {
        return accessToken;
    }
    // Update is called once per frame
    void Update()
    {

        if (_startPolling)
        {
            startTimer += Time.deltaTime;
            if (startTimer > 10.0)
            {
                startPolling();
                startTimer = 0.0f;
            }
        }



        if (emailsReady)
        {
            emailTimer += Time.deltaTime;

            //if (emailTimer > 5.0f) {
            if (emailTimer > 0.0f)
            {
                getEmails();
                emailTimer = emailRefreshTime * -1;
            }

        }
    }

    private void startPolling()
    {
        FetchJWT(deviceCode);
    }
    private void deviceCodeAuthAsync()
    {

        Debug.Log("Inside DeviceCodeAuthAsync");
        WWWForm form = new WWWForm();
        form.AddField("client_id", client_id);
        form.AddField("scope", scopes);
        UnityWebRequest request = UnityWebRequest.Post(deviceCodeEndpoint, form);

        StartCoroutine(onDeviceCodeResponse(request));
    }

    private IEnumerator onDeviceCodeResponse(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        if (request.responseCode == 200)
        {

            string jsonResponse = request.downloadHandler.text;
            Debug.Log(jsonResponse);
            var codeResponse = JSON.Parse(jsonResponse);
            deviceCode = codeResponse["device_code"];
            Debug.Log(codeResponse["message"]);

            GameObject _emailData = (GameObject)Instantiate(prefab);
            _emailData.transform.SetParent(gameObject.transform, false);
            _emailData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Please Login to continue";
            _emailData.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = 10.0f;
            _emailData.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = codeResponse["message"];

            _startPolling = true;
        }
        else
        {
            Debug.Log("Something went wrong!");
        }

    }

    private IEnumerator getNewTokenWithRefreshToken()
    {
        Debug.Log("Inside getNewTokenWithRefreshToken");
        if (refreshToken == "")
        {
            Debug.Log("No Refresh token");
            // Need to Authenticate Again
            //deviceCodeAuthAsync();            
        }
        else
        {
            Debug.Log($"RefreshToken is {refreshToken}");
            var request = new UnityWebRequest(getTokenEndpoint, "POST");
            //JSON.Parse(itemPayload);

            WWWForm form = new WWWForm();
            form.AddField("client_id", client_id);
            form.AddField("grant_type", "refresh_token");
            form.AddField("refresh_token", refreshToken);
            using (UnityWebRequest www = UnityWebRequest.Post(getTokenEndpoint, form))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError)
                {
                    Debug.Log("Something went wrong");
                    Debug.Log(www.downloadHandler.text);
                }
                else
                {
                    //var codeResponse = JsonConvert.DeserializeObject<RefTokenResponse>(json);

                    string jsonResponse = www.downloadHandler.text;
                    Debug.Log(jsonResponse);
                    var codeResponse = JSON.Parse(jsonResponse);
                    _startPolling = false;
                    accessToken = codeResponse["access_token"].Value;
                    Debug.Log(codeResponse["access_token"].Value);
                    getEmails();
                    emailsReady = true;
                }
            }


        }
    }

    private IEnumerator onEmailResponse(UnityWebRequest request)
    {
        map = new Dictionary<string, string>();

        if (gcList.Count != 0)
        {
            for (int i = 0; i < gcList.Count; i++)
            {
                Destroy(gcList[i]);
                //gcList[i] = null;
                //Garbage Collection!
            }
            gcList = new List<GameObject>();
        }


        yield return request.SendWebRequest();
        if (request.responseCode == 401)
        {
            // get new Access Token now
            Debug.Log("Reached 401 here!");
            var request_new = UnityWebRequest.Get(refTokenEndpoint);
            StartCoroutine(TokenExistenceResponse(request_new));
        }
        if (request.isNetworkError)
        {
            Debug.Log("Email Response - something went wrong");
        }
        else
        {
            Debug.Log("Started rendering emails");
            emailsReady = true;
            //Debug.Log("Here too!");
            string jsonEmailContent = request.downloadHandler.text;
            jsonEmailContent = jsonEmailContent.Replace("@odata.context", "odata_context");
            jsonEmailContent = jsonEmailContent.Replace("@odata.nextLink", "odata_nextLink");
            jsonEmailContent = jsonEmailContent.Replace("@odata.etag", "odata_etag");
            var codeResponse = JSON.Parse(jsonEmailContent);
            Debug.Log(codeResponse);
            //Debug.Log(jsonEmailContent);
            foreach (var item in codeResponse["value"].Values)
            {
                DateTime currentTS = DateTime.UtcNow;
                //Debug.Log($"{(currentTS - DateTimeOffset.Parse(item["receivedDateTime"].Value)).TotalSeconds} and {DateTimeOffset.Parse(item["receivedDateTime"].Value)} and Current TS={currentTS}");
                //Debug.Log($"Right now:{(currentTS - DateTimeOffset.Parse(item["receivedDateTime"].Value)).TotalSeconds} and {emailRefreshTime + cushionTime}");                
                if ((currentTS - DateTimeOffset.Parse(item["receivedDateTime"].Value)).TotalSeconds <= (emailRefreshTime + cushionTime))
                {
                    //Debug.Log("Inside notification pusher");
                    StartCoroutine(NotificationService.pushNotificationNew("Email", item["subject"].Value, item["bodyPreview"].Value));
                }

                GameObject _emailData = (GameObject)Instantiate(prefab);
                _emailData.transform.SetParent(gameObject.transform, false);
                _emailData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item["subject"];
                _emailData.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item["bodyPreview"];


                //Debug.Log(item["bodyPreview"]);

                // Hashing email and setting up map
                string emailContent = item["body"]["content"].Value;
                Debug.Log(emailContent);
                emailContent = Regex.Replace(emailContent, @"<[^>]*>", String.Empty);
                emailContent = emailContent.Replace("&nbsp;", "");
                emailContent = emailContent.Replace("nbsp;", "");
                emailContent = emailContent.Replace("nbsp", "");
                emailContent = emailContent.Trim();

                map[_emailData.GetHashCode().ToString()] = emailContent;

                gcList.Add(_emailData);
                _emailData.GetComponent<EmailContent>().emailContentBody = map[_emailData.GetHashCode().ToString()];
                _emailData.GetComponent<EmailContent>().emailSubject = item["subject"];
                _emailData.GetComponent<EmailContent>().emailFrom = item["from"]["emailAddress"]["address"];
                var listItemReply = JSON.Parse(item["toRecipients"]);
                foreach (var itemReply in listItemReply.Values)
                {
                    string z = itemReply["emailAddress"]["address"];
                    Debug.Log(z);
                    //string x = listItem["emailAddress"];
                    _emailData.GetComponent<EmailContent>().emailReplyTo.Add(z);
                }
                //_emailData.GetComponent<EmailContent>().emailReplyTo = item["sender"]["emailAddress"]["address"];
                //_emailData.GetComponent<EmailContent>().emailRecipient = item["toRecipients"]["emailAddress"]["address"];
                //_emailData.GetComponent<EmailContent>().emailReplyTo = item["replyTo"]["emailAddress"]["address"];
            }
            //Logout();
        }

    }

    private void getEmails()
    {

        string emailEndpoint = "https://graph.microsoft.com/v1.0/me/mailFolders/inbox/messages?$orderby=InferenceClassification, createdDateTime DESC&filter=InferenceClassification eq 'Focused'";
        Debug.Log(accessToken);
        if (accessToken == null || accessToken == "") { emailsReady = false; return; }
        try
        {
            var request = UnityWebRequest.Get(emailEndpoint);
            request.SetRequestHeader("Authorization", $"Bearer {accessToken}");
            Debug.Log("Requesting for emails");
            StartCoroutine(onEmailResponse(request));
            //cacheAccessToken = accessToken;
        }
        catch (Exception exception)
        {
            Debug.Log($"Something getEmails went wrong:{exception}");
        }
    }

    public void getAppointments()
    {

        try
        {
            var request = UnityWebRequest.Get(APPOINTMENTS_URL);
            request.SetRequestHeader("Authorization", $"Bearer {accessToken}");
            StartCoroutine(onAppointmentsResponse(request));
        }
        catch (Exception exception)
        {
            Debug.Log($"Something went Wrong!:{exception}");
        }
    }

    private IEnumerator onAppointmentsResponse(UnityWebRequest request)
    {
        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log("Something went Wrong");
        }
        else
        {
            string jsonAppointmentsResponse = request.downloadHandler.text;
            jsonAppointmentsResponse = jsonAppointmentsResponse.Replace("@odata.context", "odata_context");
            jsonAppointmentsResponse = jsonAppointmentsResponse.Replace("@odata.etag", "odata_etag");
            var codeResponse = JSON.Parse(jsonAppointmentsResponse);
            foreach (var item in codeResponse["value"].Values)
            {

                string subject = item["subject"].Value;
                string startTime = item["start"]["dateTime"].Value;
                string endTime = item["end"]["dateTime"].Value;
                string organizer = item["organizer"]["emailAddress"]["name"];
                Debug.Log($"subject={subject}; startTime={startTime}; endTime={endTime}; organizer={organizer}");

            }
        }
    }

    public void Logout()
    {
        Debug.Log("Logging Out");
        _startPolling = false;
        startTimer = 0.0f;
        deviceCode = "";
        noCode = false;
        refreshToken = "";
        emailTimer = 0.0f;
        emailsReady = false;
        emailRefreshTime = 65.0f;
        cushionTime = 5.8f;
        gcList = new List<GameObject>();
        flushRefreshToken();
        checkIfTokenExists();
    }

    private void flushRefreshToken()
    {

        string myJson = "{ \"accessToken\":\"" + refreshToken + "\" }";
        Debug.Log(myJson);
        string updateEndpoint = "https://cyborgartist.azurewebsites.net/accessToken";
        var NotificationRequest = new UnityWebRequest(updateEndpoint, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(myJson);
        NotificationRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        NotificationRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        NotificationRequest.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(onflushResponse(NotificationRequest));

    }

    private IEnumerator onflushResponse(UnityWebRequest request)
    {

        yield return request.SendWebRequest();
        if (request.isNetworkError || request.responseCode != 200)
        {

            Debug.Log("Something went wrong");

        }
        else
        {
            checkIfTokenExists();
        }

    }

    
    public void SendEmail(string subject, string EmailContent, string recepientEmail, bool replyThread = false)
    {


        if (accessToken == "")
        {
            return;
        }

        var SendEmailRequest = new UnityWebRequest(sendEmailEndpoint, "POST");
        string jsonBody;
        if (!replyThread)
        {
            jsonBody = "{\"message\": {\"subject\": \"" + subject + "\",\"body\": {\"contentType\": \"Text\",\"content\": \"" + EmailContent + "\"},\"toRecipients\": [{\"emailAddress\": {\"address\": \"" + recepientEmail + "\"}}]}}";
        }
        else
        {
            //HTML Content
            string chain = StaticViewEmailBody.emailContent;
            //string updatedChain = $"<span style=\"font-style:italic;font-size:14px;\">{chain}</span>";
            //string updatedContent = $"<span style=\"font-weight:bold;font-size:18px;\">{EmailContent}</span>";
            string updatedChain = $"<i>{chain}</i>";
            string updatedContent = $"<strong>{EmailContent}</strong>";
            jsonBody = "{\"message\": {\"subject\": \"" + subject + "\",\"body\": {\"contentType\": \"HTML\",\"content\": \"" + updatedChain + "<hr>" + updatedContent + "\"},\"toRecipients\": [{\"emailAddress\": {\"address\": \"" + recepientEmail + "\"}}]}}";
            Debug.Log(jsonBody);
        }

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        SendEmailRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        SendEmailRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        SendEmailRequest.SetRequestHeader("Content-Type", "application/json");
        SendEmailRequest.SetRequestHeader("Authorization", $"Bearer {accessToken}");
        StartCoroutine(SendEmailHandler(SendEmailRequest));

    }

    private IEnumerator SendEmailHandler(UnityWebRequest request)
    {
        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log("Something Went Wrong");
        }
        else
        {
            Debug.Log("Email Sent");
            AlertBox_General.ShowAlertBox("Email sent!");
        }
    }

    private string getBody(GameObject emailData)
    {
        return map[emailData.GetHashCode().ToString()];

    }


    private IEnumerator JWTResponseHandler(UnityWebRequest request)
    {
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.responseCode != 200)
        {
            Debug.Log("Something went wrong - Auth pending");
        }
        else
        {
            string jsonContent = request.downloadHandler.text;
            Debug.Log(jsonContent);
            var codeResponse = JSON.Parse(jsonContent);
            refreshToken = codeResponse["refresh_token"];
            accessToken = codeResponse["access_token"];
            string updateEndpoint = "https://cyborgartist.azurewebsites.net/accessToken";
            string myJson = "{ \"accessToken\":\"" + refreshToken + "\" }";
            Debug.Log(myJson);
            var NotificationRequest = new UnityWebRequest(updateEndpoint, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(myJson);
            NotificationRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            NotificationRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            NotificationRequest.SetRequestHeader("Content-Type", "application/json");

            StartCoroutine(PostNotificationHandler(NotificationRequest));

        }
    }

    private IEnumerator PostNotificationHandler(UnityWebRequest request)
    {
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
        if (request.isNetworkError || request.responseCode != 200)
        {
            Debug.Log("Not yet Authenticated");
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            _startPolling = false;
            getEmails();
        }
    }

    private void FetchJWT(string deviceCode)
    {
        if (deviceCode == "")
            return;
        Debug.Log("Waiting for Authentication");
        Debug.Log($"Device Code is:{deviceCode}");
        WWWForm form = new WWWForm();
        form.AddField("client_id", client_id);
        form.AddField("grant_type", "urn:ietf:params:oauth:grant-type:device_code");
        form.AddField("device_code", deviceCode);
        UnityWebRequest request = UnityWebRequest.Post(getTokenEndpoint, form);
        StartCoroutine(JWTResponseHandler(request));
    }

}


