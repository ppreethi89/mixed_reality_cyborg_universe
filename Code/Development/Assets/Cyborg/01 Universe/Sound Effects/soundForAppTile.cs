using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class soundForAppTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
          
    }

    public void Play(string name)
    {
        FindObjectOfType<audiomana>().Play(name);
    }
}
