using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    // Start is called before the first frame update
    Renderer renderer;

    [SerializeField]
    [Range(0f, 1f)] float lerptime;

    [SerializeField]
    Color[] myColors;

    public bool isPlaying;

    int colorIndex = 0;

    float t = 0f;
    int len;
    void Start()
    {
        renderer = this.gameObject.GetComponent<Renderer>();
        len = myColors.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            renderer.material.color = Color.Lerp(renderer.material.color, myColors[colorIndex], lerptime * Time.deltaTime);
            t = Mathf.Lerp(t, 1f, lerptime * Time.deltaTime);

            if (t > .9f)
            {
                t = 0f;
                colorIndex++;
                colorIndex = (colorIndex >= len) ? 0 : colorIndex;
            }
        }
       
    }
}
