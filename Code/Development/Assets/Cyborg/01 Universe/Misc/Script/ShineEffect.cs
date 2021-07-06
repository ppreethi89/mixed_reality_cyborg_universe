using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShineEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage lines;
    [SerializeField]
    float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rect uvRect = lines.uvRect;
        uvRect.x += speed * Time.deltaTime;
        lines.uvRect = uvRect;
    }
}
