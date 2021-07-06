using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

public class CalendarScript : MonoBehaviour
{
    // Start is called before the first frame update
    string refreshToken;
    string client_id;
    string accessToken;
    string getTokenEndpoint;
    bool calendarReady;

    private string getCalendarEndpoint = $"https://graph.microsoft.com/v1.0/me/events?$select=subject,body,bodyPreview,organizer,attendees,start,end,location";
    private string createMeetingEndpoint = $"https://graph.microsoft.com/v1.0/me/events";

    void Start()
    {
        refreshToken = "";

        //Reach out to admin for the credential below
        client_id = "";


        getTokenEndpoint = $"https://login.microsoftonline.com/consumers/oauth2/v2.0/token";
        calendarReady = false;
    }

    // Update is called once per frame
    void Update(){
        
        
    }


    private void getCalendarEvents() { 
    }

    private async Task<bool> ScheduleMeeting(string title, DateTime startDateTime, DateTime endDateTime) {
        using (var client = new HttpClient()) {
            var itemPayload = new
            {
                subject = title,
                start = new{
                    datetime=startDateTime,
                    timeZone="UTC"
                },
                end = new{
                    datetime=endDateTime,
                    timeZone="UTC"
                }
            };
            HttpContent payload = new StringContent(JsonConvert.SerializeObject(itemPayload));
            payload.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
            HttpResponseMessage result = await client.PostAsync(createMeetingEndpoint, payload);
            return result.IsSuccessStatusCode;
        }        
    }

    private async void getMeetings() {
        using (var client = new HttpClient()) {
            HttpResponseMessage result = await client.GetAsync(getCalendarEndpoint);
            string jsonString = await result.Content.ReadAsStringAsync();
            //jsonString.
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
                    //_startPolling = false;
                    accessToken = codeResponse.access_token;
                    Debug.Log(codeResponse.access_token);
                    calendarReady = true;
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

}

/*
  {
    "subject": "My event",
    "start": {
        "dateTime": "2020-11-23T18:10:51.318Z",
        "timeZone": "UTC"
    },
    "end": {
        "dateTime": "2020-11-30T18:10:51.318Z",
        "timeZone": "UTC"
    }
}
*/