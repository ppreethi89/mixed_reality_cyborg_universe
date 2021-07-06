using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class flask : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void GetData()
    {
        Debug.Log("Forward");
        StartCoroutine(GetRequest("http://169.254.19.18:5000/ArduinoUp"));
   //     var colors = GetComponent<Button>().colors;
     //   colors.normalColor = Color.red;
       // GetComponent<Button>().colors = colors;
    }

    public void GetDataBackward()
    {
        Debug.Log("Backward");
        StartCoroutine(GetRequest("http://169.254.19.18:5000/ArduinoDown"));
    }

    public void GetDataLeft()
    {
        Debug.Log("Left");
        StartCoroutine(GetRequest("http://169.254.19.18:5000/ArduinoLeft"));
    }

    public void GetDataRight()
    {
        Debug.Log("Right");
        StartCoroutine(GetRequest("http://169.254.19.18:5000/ArduinoRight"));
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
