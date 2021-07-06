using UnityEngine.Audio;
using System;
using UnityEngine;

public class audiomana : MonoBehaviour
{
    public soundeffects[] soundeffect;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(soundeffects s in soundeffect)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

    }

    private void Start()
    {
        //Play("latch");
    }

    public void click()
    {
        Play("latch");
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Play(string name)
    {
        soundeffects s = Array.Find(soundeffect, sound => sound.name == name);
        s.source.Play();
    }

}
