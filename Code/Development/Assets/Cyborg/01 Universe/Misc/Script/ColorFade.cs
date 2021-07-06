using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorFade : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    Renderer renderer;
    Image image;

    [SerializeField]
    [Range(0f, 1f)] float lerptime;

    [SerializeField]
    Color[] myColors;

    int colorIndex = 0;

    float t = 0f;
    int len;
    public bool isDone = true;
    void Start()
    {
        
        image = this.gameObject.GetComponent<Image>();
        len = myColors.Length;
    }

    // Update is called once per frame
    void Update()
    {
        image.color = Color.Lerp(image.color, myColors[colorIndex], lerptime * Time.deltaTime);
        
        t = Mathf.Lerp(t, 1f, lerptime * Time.deltaTime);
        Debug.Log(colorIndex);
        if (t > .5f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;
           
        }
    }
}
