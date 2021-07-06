using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Identity.Client;
using System.Linq;
using System.Security;
using System.Threading;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Networking;
#if UNITY_WSA && !UNITY_EDITOR
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.Identity.Client;
#endif

public class GraphAuth : MonoBehaviour
{

    IEnumerable<string> Scopes;
    private string authority;
    private string ClientId;
    private string tenant;
    private string redirectUrl;
    private float timer = 0.0f;
    private string cacheAccessToken;
    private float refreshTime = 30.0f;

    

    public string[] authcode;
    public GameObject accesscode;
    public GameObject prefab;
    public GameObject composePanel;

     public TMP_InputField subjectfield;
    public TMP_InputField sendtoRecepient;
    public TMP_InputField bodyMessage;
    private string accessToken;
    // Start is called before the first frame update
    async void Start() {

         composePanel.SetActive(false);
        Scopes = new List<string> { "User.Read", "Mail.Read", "Mail.Send" };
        //authority = "https://login.microsoftonline.com/common";                    
        cacheAccessToken = "";

        //Reach out to admin for the credential below
        ClientId = ""; 
        

        //authority = $"https://login.microsoftonline.com/{tenant}";
        authority = $"https://login.microsoftonline.com/consumers";
        redirectUrl = "https://login.microsoftonline.com/common/oauth2/nativeclient";
        await GoGraph();
    }

    public void openComposePanel()
    {
        composePanel.SetActive(true);
    }


    public void disableCompose()
    {

        subjectfield.text = "";
        bodyMessage.text = "";
        sendtoRecepient.text = "";

        composePanel.SetActive(false);
    }


    public void initializeSend()
    {
        sendEmailUWR(accessToken);
    }

    private async Task<AuthenticationResult> GoGraph() {
        IPublicClientApplication pca = PublicClientApplicationBuilder
            .Create(ClientId)
            .WithAuthority(authority)
            .WithDefaultRedirectUri()
            .Build();
        var accounts = await pca.GetAccountsAsync();
        try
        {
            return await pca.AcquireTokenSilent(Scopes, accounts.FirstOrDefault())
                  .ExecuteAsync();
        }
        catch (MsalUiRequiredException ex)
        {
            // No token found in the cache or AAD insists that a form interactive auth is required (e.g. the tenant admin turned on MFA)
            // If you want to provide a more complex user experience, check out ex.Classification 
            Debug.Log($"UI Reqd Exception:{ex.Message}");
            return await AcquireByDeviceCodeAsync(pca);
        }
        catch (MsalException msaEx)
        {
            Debug.Log($"Failed to acquire token: {msaEx.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Debug.Log($"Failed to acquire token: {ex.Message}");
            return null;
        }
    }
    
        private async Task<AuthenticationResult> AcquireByDeviceCodeAsync(IPublicClientApplication pca) {
        Debug.Log($"Inside AcquireByDeviceCodeAsync!");
        try
        {

            var result = await pca.AcquireTokenWithDeviceCode(Scopes,
                deviceCodeResult =>
                {
                    UnityEngine.WSA.Application.InvokeOnAppThread(() =>
                    {
                        Debug.Log($"{deviceCodeResult.Message}");
                        authcode = deviceCodeResult.Message.Split(' ');
                        Debug.Log(authcode[16]);
                    }, true);
                    return Task.FromResult(0);
                }).ExecuteAsync();
            Debug.Log($"Access Token is:{result.AccessToken}");
            
            //getEmails(result.AccessToken);
            getEmailsUWR(result.AccessToken);
            //Debug.Log(result.Account.Username);
            //sendEmail(result.AccessToken);
            sendEmailUWR(result.AccessToken);
            accessToken = result.AccessToken;
            return result;
        }
        catch (MsalServiceException ex)
        {
            Debug.Log($"MsalServiceException:${ex}");
            return null;
        }
        catch (OperationCanceledException ex)
        {
            Debug.Log($"Operation Canceled Exception:${ex.Message}");
            return null;
        }
        catch (MsalClientException ex)
        {
            Debug.Log($"MsalClientException:{ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Debug.Log($"The last caught exception:{ex.Message}");
            return null;
        }
    }

    private async void getEmailsUWR(string accessToken) {
        string emailEndpoint = "https://graph.microsoft.com/v1.0/me/messages";       
        try
        {
            var request = UnityWebRequest.Get(emailEndpoint);
            request.SetRequestHeader("Authorization", $"Bearer {accessToken}");
            cacheAccessToken = accessToken;
            StartCoroutine(onResponse(request));
        }
        catch (Exception exception) {
            Debug.Log($"Something went wrong:${exception}");
        }

    }
    private IEnumerator onResponse(UnityWebRequest request) {
        yield return request.SendWebRequest();
        if (request.isNetworkError) {
            Debug.Log("Network error has occured: " + request.GetResponseHeader(""));
        }
        else {
            string jsonEmailContent = request.downloadHandler.text;
            jsonEmailContent = jsonEmailContent.Replace("@odata.context", "odata_context");
            jsonEmailContent = jsonEmailContent.Replace("@odata.nextLink", "odata_nextLink");
            jsonEmailContent = jsonEmailContent.Replace("@odata.etag", "odata_etag");
            var codeResponse = JsonConvert.DeserializeObject<RootObject>(jsonEmailContent);            
            foreach (var item in codeResponse.value)
            {                
                Debug.Log($"Subject:{item.subject} and Preview:{item.bodyPreview}");
                GameObject _emailData = (GameObject)Instantiate(prefab);
                _emailData.transform.SetParent(gameObject.transform, false);
                TextMeshProUGUI _emailSubject = _emailData.transform.GetChild(0).GetComponent<TextMeshProUGUI>();//.text = emailSubject;
                _emailSubject.text = item.subject;
                TextMeshProUGUI _emailBody = _emailData.transform.GetChild(1).GetComponent<TextMeshProUGUI>();//.text = emailBodyPreview;
                _emailBody.text = item.bodyPreview;
            }
        }

    }
   
    // Update is called once per frame
    void Update(){
        timer += Time.deltaTime;
        if (timer >= refreshTime) {
            getEmailsUWR(cacheAccessToken);
            timer = 0.0f;
        }
        try
        {
            accesscode.GetComponent<TextMeshPro>().text = authcode[16];
        }
        catch
        {
            Debug.Log("Text mesh pro for access code not yet initialize");
        }
            
    }

    private async void sendEmailUWR(string token) {
        string sendEmailUrl = "https://graph.microsoft.com/v1.0/me/sendMail";
        var itemPayload = new
        {
            Message = new
            {
                Subject = subjectfield.text,
                Body = new { ContentType = "Text", Content = bodyMessage.text },
                ToRecipients = new[] { new { EmailAddress = new { Address = sendtoRecepient.text } } }
            }
        };
        //Debug.Log($"Item payload is:{itemPayload}");       
        var _itemPayload=(string)JsonConvert.SerializeObject(itemPayload);
        Debug.Log($"_itemPayload is:{_itemPayload}");
        
        var request = new UnityWebRequest(sendEmailUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(_itemPayload);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {token}");
        StartCoroutine(onSendEmailResponse(request));

        disableCompose();
    }

    private IEnumerator onSendEmailResponse(UnityWebRequest request){
               
       yield return request.Send();
        if (request.isNetworkError) {
            Debug.Log("Something went wrong");
        }
        else {
            Debug.Log(request.downloadHandler.text);
            Debug.Log("Success!!!");
        }
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
}
