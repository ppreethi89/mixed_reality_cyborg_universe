using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DCFlow : MonoBehaviour
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
    async void Start()
    {
        init();
        
    }

   

    private void init() {

        map = new Dictionary<string, string>();

        //Reach out to admin for the credential below
        client_id = "";

        scopes = "offline_access User.read mail.read";
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

    private async Task<string> deviceCodeFlow() {

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
                GameObject _emailData = (GameObject)Instantiate(prefab);
                _emailData.transform.SetParent(gameObject.transform, false);
                _emailData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Login to Continue";
                _emailData.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = codeResponse.message;
                Debug.Log(codeResponse.device_code);
                deviceCode = codeResponse.device_code;                
                Debug.Log(codeResponse.message);
                return "200-"+deviceCode+"-"+codeResponse.message;
            }
            else
            {
                var codeResponse = JsonConvert.DeserializeObject<DeviceCodeErrorResposne>(json);
                Debug.Log(codeResponse.error + " " + codeResponse.error_description);
                return "404-"+codeResponse.error + "-" + codeResponse.error_description;
            }
        }

    }

    private async Task<bool> FetchJWT(string deviceCode)
    {
        if (deviceCode == "")
            return false;
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
                //Pushing token to  Azure
                using (var _client = new HttpClient())
                {
                    var _response = await client.PostAsync(
                        refTokenEndpoint,
                         //"http://localhost:8081/accessToken",
                         new StringContent(myJson, Encoding.UTF8, "application/json"));
                }
                _startPolling = false;
                return true;                
            }
            else
            {
                Debug.Log("Not yet Authenticated");
                return false;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
