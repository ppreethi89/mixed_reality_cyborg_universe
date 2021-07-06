using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssitTone : MonoBehaviour
{
    public AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAssitSound() 
    {
       
            source.Play();
        
    }
}
