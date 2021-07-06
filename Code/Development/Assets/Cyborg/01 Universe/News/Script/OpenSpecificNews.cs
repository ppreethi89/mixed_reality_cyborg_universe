using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSpecificNews : MonoBehaviour
{
    // Start is called before the first frame update
    public string newsURL;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openNews()
    {
        //Application.OpenURL(newsURL);
        NewsApi.ShowNewsVuplex(newsURL);
    }
}
