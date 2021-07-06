using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using RESTClient;
using Azure.StorageServices;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;
using TMPro;
using Tacticsoft;
using System;
using Microsoft.MixedReality.Toolkit;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[RequireComponent(typeof(AudioSource))]

public class CyborgMusicPlayer : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    //Azure Set up
    private AudioClip musicClip;
    public List<AudioClip> musicList;
    public int currentTrack = 0;

    [Header("Azure Storage Service")]

    // Please reach out to admin for the 3 credentials below
    [SerializeField]
    private string storageAccount;
    [SerializeField]
    private string accessKey;
    [SerializeField]
    private string container;

    private StorageServiceClient client;
    private BlobService blobService;

    [Header("List Demo")]
    public int count = 0;

    public AudioSource audioSource;
    private List<Blob> items;

    public bool playPause;
    [SerializeField]
    TextMeshProUGUI title;

    [SerializeField]
    Sprite pause;
    [SerializeField]
    Sprite play;

    [SerializeField]
    Image playPauseControl;

    [SerializeField]
    Slider slider;

    [SerializeField]
    RotateObject gear;

    [SerializeField]
    ColorLerp icon;
    public void OnPointerClick(PointerEventData eventData)
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }
    void Start()
    {
        client = StorageServiceClient.Create(storageAccount, accessKey);
        blobService = client.GetBlobService();

        ListBlobs();
       
       


    }
    public void ListBlobs()
    {
        StartCoroutine(blobService.ListBlobs(ListBlobsCompleted, container));
    }

    private void ListBlobsCompleted(IRestResponse<BlobResults> response)
    {
        if (response.IsError)
        {
            Debug.Log("Failed to get list of blobs List blob error: " + response.ErrorMessage);
            return;
        }
        else
        {
            Debug.Log(response.Data.Blobs);
        }

        StartCoroutine(ReloadTable(response.Data.Blobs));
    }

    public IEnumerator ReloadTable(Blob[] blobs)
    {
       
        var i = 0;
        while (i < 5 && count <= blobs.Length - 1)
        {

            string resourcePath = container + "/" + blobs[count].Name;
            i++;
            count++;
            StartCoroutine(blobService.GetAudioBlob(GetAudioBlobComplete, resourcePath));

            void GetAudioBlobComplete(IRestResponse<AudioClip> response)
            {
                AudioClip audioClip = response.Data;
                
                musicList.Add(audioClip);
                musicList[musicList.Count - 1].name = response.Url;
 
            }
        }
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
        try
        {
            float timeRemaining = audioSource.time / audioSource.clip.length;
            
            if (timeRemaining > 0)
            {
                
                slider.value = timeRemaining;
            }
            if (audioSource.clip.length <= 0 || audioSource.time >= audioSource.clip.length)
            {
                Debug.Log("This music is not responding");
                NextMusic();
                //currentTrack++;
                //PlayMusic();
                //StartCoroutine(WaitForMusicEnd());
            }
            
        }
        catch
        {

        }
    }
    public void PlayMusic()
    {
        playPause = !playPause;

        if (playPause)
        {
            audioSource.clip = musicList[currentTrack];
            string[] splitName = musicList[currentTrack].name.Split(char.Parse("/"));
            string[] splitName2 = splitName[4].Split(char.Parse("."));
            title.text = splitName2[0];
            audioSource.UnPause();
            //audioSource.UnPause();
            playPauseControl.sprite = pause;
            gear.isPlaying = true;
            icon.isPlaying = true;
        }
        else
        {
            audioSource.Pause();
            playPauseControl.sprite = play;
            gear.isPlaying = false;
            icon.isPlaying = false;
        }
    }
    public void NextMusic()
    {
        currentTrack++;
        audioSource.clip = musicList[currentTrack];
        string[] splitName = musicList[currentTrack].name.Split(char.Parse("/"));
        string[] splitName2 = splitName[4].Split(char.Parse("."));
        title.text = splitName2[0];

        ResetMusic();
        playPause = true;
        playPauseControl.sprite = pause;
        audioSource.Play();
        CheckMaxMusic();
    }
    void ResetMusic()
    {
        
        slider.value = 0;
        audioSource.time = 0;
    }
    public void PreviousMusic()
    {
        if (currentTrack != 0)
        {
            currentTrack--;
            audioSource.clip = musicList[currentTrack];
            string[] splitName = musicList[currentTrack].name.Split(char.Parse("/"));
            string[] splitName2 = splitName[4].Split(char.Parse("."));
            title.text = splitName2[0];
            ResetMusic();
            playPause = true;
            playPauseControl.sprite = pause;
            audioSource.Play();

            
        }
        
    }
    void CheckMaxMusic()
    {
        int diff = musicList.Count - currentTrack;
        Debug.Log(diff);
        if (diff <= 2)
        {
            ListBlobs();
            Debug.Log("ListBlob again");    
        }
    }
    public void GazeSlider()
    {
        Vector3 hitpos = CoreServices.InputSystem.EyeGazeProvider.HitPosition;
        PointerEventData eve = new PointerEventData(EventSystem.current);
        eve.position = hitpos;

        slider.OnPointerDown(eve);
        audioSource.time = audioSource.clip.length * slider.value;
    }

    IEnumerator WaitForMusicEnd()
    {
        while (audioSource.isPlaying && !playPause)
        {
            yield return null;
        }

        NextMusic();
        
    }
}
