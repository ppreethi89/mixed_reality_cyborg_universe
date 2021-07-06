using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGalleryV2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject AzureStorageServices;
    [SerializeField]
    private string azureContainer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeGallerySelected()
    {

        AzureStorageServices.GetComponent<AzureGetImage>().azureContainer = azureContainer;
        AzureStorageServices.GetComponent<AzureGetImage>().ListBlobs();

    }

}
