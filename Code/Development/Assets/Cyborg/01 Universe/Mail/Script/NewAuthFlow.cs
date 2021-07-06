using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class NewAuthFlow : MonoBehaviour
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
    bool noCode;
    float emailTimer;
    bool emailsReady;
    public GameObject prefab;
    public TextMeshProUGUI textMeshPro;
    private float emailRefreshTime;
    private Dictionary<string, string> map;


    // Start is called before the first frame update
    void Start()
    {
        initParameters();
        checkIfTokenExists();
        /*
        if(deviceCode=="")
            deviceCodeAuthAsync();        
        */
    }
    private async void checkIfTokenExists()
    {

        using (var client = new HttpClient())
        {
            var request = new HttpRequestMessage(HttpMethod.Get, refTokenEndpoint);
            var response = await client.SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();
            //Debug.Log(json);
            json = json.Replace("\"", "");
            json = json.Replace("'", "\"");
            json = json.Replace(" ", "");
            Debug.Log(json);
            var codeResponse = JsonConvert.DeserializeObject<RefreshTokenLocalResponse>(json);
            Debug.Log(codeResponse.refreshToken);
            if (codeResponse.refreshToken == "")
            {
                noCode = true;
                deviceCodeAuthAsync();
            }
            else
            {
                refreshToken = codeResponse.refreshToken;
                getNewTokenWithRefreshToken();
            }
        }
    }
    private void initParameters()
    {
        map = new Dictionary<string, string>();

        //Reach out to admin for the credential below
        client_id = "";

        scopes = "offline_access User.read mail.read Calendars.Read Calendars.ReadWrite";
        urlParameters = "";
        deviceCodeEndpoint = $"https://login.microsoftonline.com/consumers/oauth2/v2.0/devicecode";
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
        emailRefreshTime = 15.0f;
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
                //emailTimer = -200.0f;
                //emailTimer = 0.0f;

            }

        }
    }

    private void startPolling()
    {
        FetchJWT(deviceCode);
    }
    private async void deviceCodeAuthAsync()
    {

        using (var client = new HttpClient())
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {

                ["client_id"] = client_id,
                ["scope"] = scopes
            });

            var request = new HttpRequestMessage(HttpMethod.Post, deviceCodeEndpoint);
            request.Content = content;
            var response = await client.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var codeResponse = JsonConvert.DeserializeObject<DeviceCodeResponse>(json);
                _startPolling = true;
                GameObject _emailData = (GameObject)Instantiate(prefab);
                _emailData.transform.SetParent(gameObject.transform, false);
                _emailData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Login to Continue";
                _emailData.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = codeResponse.message;
                Debug.Log(codeResponse.device_code);
                deviceCode = codeResponse.device_code;
                Debug.Log(codeResponse.message);
            }
            else
            {
                var codeResponse = JsonConvert.DeserializeObject<DeviceCodeErrorResposne>(json);
                Debug.Log(codeResponse.error + " " + codeResponse.error_description);
            }
        }

    }

    private async void getNewTokenWithRefreshToken()
    {
        if (refreshToken == "")
        {
            Debug.Log("No Refresh token");
            return;
        }
        else
        {
            Debug.Log($"RefreshToken is {refreshToken}");
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {

                    ["client_id"] = client_id,
                    ["grant_type"] = "refresh_token",
                    ["refresh_token"] = refreshToken
                });


                var request = new HttpRequestMessage(HttpMethod.Post, getTokenEndpoint);
                request.Content = content;
                var response = await client.SendAsync(request);

                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var codeResponse = JsonConvert.DeserializeObject<RefTokenResponse>(json);
                    _startPolling = false;
                    accessToken = codeResponse.access_token;
                    Debug.Log(codeResponse.access_token);
                    emailsReady = true;
                }
                else
                {
                    Debug.Log(response.Content);
                    Debug.Log(response.ReasonPhrase);
                    Debug.Log(response.RequestMessage);
                    Debug.Log("Something went wrong!");
                }

            }

        }
    }

    private async void getEmails()
    {
        /*
        List<NotificationFormat> responseNotifications =(await NotificationService.getNotifications());
        
        foreach (var responseItem in responseNotifications) {
            Debug.Log($"Notification:{responseItem.subject} and {responseItem.content}");
        }
        */
        //textMeshPro.SetText("");
        //string emailEndpoint = "https://graph.microsoft.com/v1.0/me/messages";
        map = new Dictionary<string, string>();
        string emailEndpoint = "https://graph.microsoft.com/v1.0/me/mailFolders/inbox/messages?$orderby=InferenceClassification, createdDateTime DESC&filter=InferenceClassification eq 'Focused'";
        var tokenRequest = new HttpRequestMessage(HttpMethod.Get, emailEndpoint);
        HttpClient client = new HttpClient();
        Debug.Log(accessToken);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var emailData = await client.SendAsync(tokenRequest);
        if (emailData.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            emailsReady = false;
            getNewTokenWithRefreshToken();
        }
        else
        {
            string jsonEmailContent = await emailData.Content.ReadAsStringAsync();
            Debug.Log(jsonEmailContent);
            jsonEmailContent = jsonEmailContent.Replace("@odata.context", "odata_context");
            jsonEmailContent = jsonEmailContent.Replace("@odata.nextLink", "odata_nextLink");
            jsonEmailContent = jsonEmailContent.Replace("@odata.etag", "odata_etag");
            var codeResponse = JsonConvert.DeserializeObject<RootObject>(jsonEmailContent);
            string toDisplay = "";
            foreach (var item in codeResponse.value)
            {

                DateTime currentTS = DateTime.UtcNow;
                GameObject _emailData = (GameObject)Instantiate(prefab);
                _emailData.transform.SetParent(gameObject.transform, false);
                _emailData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.subject;
                _emailData.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.bodyPreview;

                //if ((currentTS - item.receivedDateTime).TotalSeconds <= 45.0f) {
                //Debug.Log($"TSCheck - {item.bodyPreview} and {(currentTS - item.receivedDateTime).TotalSeconds}");
                if ((currentTS - item.receivedDateTime).TotalSeconds <= (emailRefreshTime + 3.0))
                {
                    //Debug.Log($"A new email arrived with preview-{item.bodyPreview}");    
                    NotificationService.pushNotificationNew("Email", item.subject, item.bodyPreview);
                }
                //item.receivedDateTime
                //_emailData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = emailSubject;
                //_emailData.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = emailBodyPreview;
                string emailContent = item.body.content;
                emailContent = Regex.Replace(emailContent, @"<[^>]*>", String.Empty);
                emailContent = emailContent.Trim();
                //map[_emailData.GetHashCode().ToString()] = item.body.content;
                map[_emailData.GetHashCode().ToString()] = emailContent;
                toDisplay += $"{item.subject} and ${item.bodyPreview}\n";
                //Debug.Log($"Subject:{item.subject} and Preview:{item.bodyPreview}");



                _emailData.GetComponent<EmailContent>().emailContentBody = getBody(_emailData);


            }
            //Debug.Log(toDisplay);
            //textMeshPro.SetText(toDisplay);
        }
    }

    private string getBody(GameObject emailData)
    {
        return map[emailData.GetHashCode().ToString()];

    }


    private async void FetchJWT(string deviceCode)
    {
        if (deviceCode == "")
            return;
        using (var client = new HttpClient())
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["client_id"] = client_id,
                ["grant_type"] = "urn:ietf:params:oauth:grant-type:device_code",
                ["device_code"] = deviceCode
            });

            var request = new HttpRequestMessage(HttpMethod.Post, getTokenEndpoint);
            request.Content = content;
            var response = await client.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();
            var codeResponse = JsonConvert.DeserializeObject<TokenV2Response>(json);



            if (response.IsSuccessStatusCode)
            {
                refreshToken = codeResponse.refresh_token;
                accessToken = codeResponse.access_token;
                string myJson = "{ \"accessToken\":\"" + refreshToken + "\" }";
                //string myJson = "{'accessToken':"+accessToken+"}";

                using (var _client = new HttpClient())
                {
                    var _response = await client.PostAsync(
                        refTokenEndpoint,
                         //"http://localhost:8081/accessToken",
                         new StringContent(myJson, Encoding.UTF8, "application/json"));
                }
                _startPolling = false;
                getEmails();
            }
            else
            {
                Debug.Log("Not yet Authenticated");
            }

        }
    }

}

public class TokenV2Response
{
    public string token_type { get; set; }
    public string scope { get; set; }
    public int expires_in { get; set; }
    public int ext_expires_in { get; set; }
    public string access_token { get; set; }
    public string refresh_token { get; set; }
}

public class DeviceCodeErrorResposne
{

    public string error { get; set; }
    public string error_description { get; set; }
    public string[] error_codes { get; set; }
    public string timestamp { get; set; }
    public string trace_id { get; set; }
    public string correlation_id { get; set; }
    public string error_uri { get; set; }
}

public class RefTokenResponse
{
    public string token_type { get; set; }
    public string scope { get; set; }//": "User.Read Mail.Read Calendars.Read Mail.Send",
    public string expires_in { get; set; }//: 3600,
    public int ext_expires_in { get; set; }//: 3600,
    public string access_token { get; set; }
    public string refresh_token { get; set; }
}

public class Body
{
    public string contentType { get; set; }
    public string content { get; set; }
}

public class EmailAddress
{
    public string name { get; set; }
    public string address { get; set; }
}

public class Sender
{
    public EmailAddress emailAddress { get; set; }
}

public class From
{
    public EmailAddress emailAddress { get; set; }
}

public class ToRecipient
{
    public EmailAddress emailAddress { get; set; }
}

public class Flag
{
    public string flagStatus { get; set; }
}

public class Value
{
    public string odata_etag { get; set; }
    public string id { get; set; }
    public DateTime createdDateTime { get; set; }
    public DateTime lastModifiedDateTime { get; set; }
    public string changeKey { get; set; }
    public IList<object> categories { get; set; }
    public DateTime receivedDateTime { get; set; }
    public DateTime sentDateTime { get; set; }
    public bool hasAttachments { get; set; }
    public string internetMessageId { get; set; }
    public string subject { get; set; }
    public string bodyPreview { get; set; }
    public string importance { get; set; }
    public string parentFolderId { get; set; }
    public string conversationId { get; set; }
    public string conversationIndex { get; set; }
    public object isDeliveryReceiptRequested { get; set; }
    public bool isReadReceiptRequested { get; set; }
    public bool isRead { get; set; }
    public bool isDraft { get; set; }
    public string webLink { get; set; }
    public string inferenceClassification { get; set; }
    public Body body { get; set; }
    public Sender sender { get; set; }
    public From from { get; set; }
    public IList<ToRecipient> toRecipients { get; set; }
    public IList<object> ccRecipients { get; set; }
    public IList<object> bccRecipients { get; set; }
    public IList<object> replyTo { get; set; }
    public Flag flag { get; set; }
}

public class RootObject
{
    public string odata_context { get; set; }
    public string odata_nextLink { get; set; }
    public IList<Value> value { get; set; }
}

public class RefreshTokenLocalResponse
{
    public string refreshToken { get; set; }
}


[Serializable]
class DeviceCodeResponse
{
    public string user_code;
    public string device_code;
    public string verification_url;
    public string expires_in;
    public string interval;
    public string message;
}

/*
public class JwtTokenExpired {
    public ErrorStruct error;
}

public class ErrorStruct {
    public string code;
    public string message;
    public InnerError innerError;
}

public class InnerError {
    public DateTime date;
    public request-id
}
*/
