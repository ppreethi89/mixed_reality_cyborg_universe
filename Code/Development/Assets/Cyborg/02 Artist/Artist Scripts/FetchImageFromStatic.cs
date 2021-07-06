using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FetchImageFromStatic : MonoBehaviour
{
    // Start is called before the first frame update

    int i = 0;
    int pageNo = 0;
    public List<RawImage> imageContainer;

    public GetImageAzureBlob getImageAzureBlob;
    public static FetchImageFromStatic instance;

    private void Awake()
    {
        instance = this;
    }
    void OnEnable()
    {
        pageNo = 0;
        foreach (RawImage img in imageContainer)
        {
            img.texture = null;
        }
        DisplayPerPage();

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextPage()
    {
        if(pageNo < ArtistInfo.maxPage-1)
        {
            pageNo++;
            Debug.Log(pageNo);
            Debug.Log("max page artist is " + ArtistInfo.maxPage);
            DisplayPerPage();
        }
        
    }
    public void PreviousPage()
    {
        if (pageNo > 0)
        {
            pageNo--;
            Debug.Log(pageNo);
            Debug.Log("max page artist is " + ArtistInfo.maxPage);
            DisplayPerPage();
        }
        
       
    }
    public void DisplayPerPage()
    {
        Debug.Log("Displaying");
        GetImageAzureBlob.DownloadImage(pageNo);

    }
}
