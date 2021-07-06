using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playatpoint : MonoBehaviour
{
    public AudioClip clip;
    public float volume = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void click()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }
}