using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.EventSystems;
using System;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class zoom : MonoBehaviour
{
    public GameObject finalImage;
   [SerializeField]
   //public bool panAutoScrollIsActive = true;
  


    // Start is called before the first frame update
    void Start()
    {

        
    }
    

    public void Zoomin()
        {
            // if (finalImage.gameObject.transform.localScale != new Vector3(1.8f, 1.8f, 1.8f))
            //{

            finalImage.gameObject.transform.localScale += new Vector3(0.1F, 0.1f, 0.1f);
            //Thread.Sleep(1000);
            //}
        }

        public void ZoomOut()
        {
            if (finalImage.gameObject.transform.localScale != new Vector3(1.0f, 1.0f, 1.0f))
            {
                finalImage.gameObject.transform.localScale -= new Vector3(0.1F, 0.1f, 0.1f);
                //Thread.Sleep(1000);
            }
        }

    void OnEnable()
    {
        finalImage.GetComponent<RawImage>().texture = ArtistInfo.ThumbnailArtImage;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
