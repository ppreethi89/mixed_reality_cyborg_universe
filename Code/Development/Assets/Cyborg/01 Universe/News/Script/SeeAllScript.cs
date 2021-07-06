using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeAllScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string url;
    void Start()
    {
        url = "https://www.bbc.com/news";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openBBC()
    {
        Application.OpenURL(url);
    }
}
