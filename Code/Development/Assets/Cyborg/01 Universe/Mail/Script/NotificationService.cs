using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class NotificationService
{
    //private const string GET_NOTIFICATION = "https://cyborgartist.azurewebsites.net/get_recent_notification";
    private const string POST_NOTIFICATION = "https://cyborgartist.azurewebsites.net/push_new_notifications";
  

    public static IEnumerator pushNotificationNew(string source, string subject, string _content) {
        _content = _content.Replace(System.Environment.NewLine," ");
        _content = _content.Replace("&nbsp;","");
        Debug.Log("Inside Push Notification");
        string myJson = "{ \"source\":" + "\"" + source + "\", \"subject\":\"" + subject + "\", \"content\":" + "\"" + _content + "\"}";
        Debug.Log(myJson);
        var request = new UnityWebRequest(POST_NOTIFICATION,"POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(myJson);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");    
        
        yield return request.SendWebRequest();
        //Debug.Log("Is it done?");
        if (request.isNetworkError){
            //Debug.Log("Failed!");
        }
        else {
            string jsonContent = request.downloadHandler.text;
            var codeResponse = JSON.Parse(jsonContent);
        }

    }
    /*
    public static async Task<List<NotificationFormat>> getNotifications()
    {
        List<NotificationFormat> notificationsList = new List<NotificationFormat>();
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(GET_NOTIFICATION);
            string getNotificationsString = await response.Content.ReadAsStringAsync();
            notificationsList = JsonConvert.DeserializeObject<List<NotificationFormat>>(getNotificationsString);
        }
        return notificationsList;
    }
    /*
    public static IEnumerator getNotificationList() {
        var request = new UnityWebRequest(GET_NOTIFICATION, "GET");
        List<JSONNode> notificationList = new List<JSONNode>();
        yield return request.SendWebRequest();
        if (request.isNetworkError) { }
        else {
            var codeResponse = JSON.Parse(request.downloadHandler.text);
            foreach (var notification in codeResponse.Values) {
                notificationList.Add(notification);
            }
        }
    }
    */
    /*
    public static async Task<bool> pushNotification(string source, string subject, string _content) {
                
        using (var client = new HttpClient()) {
            var itemPayload = new
            {
                source = source,
                subject = subject,
                content = _content
            };
            HttpContent payload = new StringContent(JsonConvert.SerializeObject(itemPayload));
            payload.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
            HttpResponseMessage result = await client.PostAsync(POST_NOTIFICATION, payload);
            return result.IsSuccessStatusCode;
        }
        
    }
    */

    
}



public class NotificationFormat{
    public string source;
    public string subject;
    public string content;    
}
