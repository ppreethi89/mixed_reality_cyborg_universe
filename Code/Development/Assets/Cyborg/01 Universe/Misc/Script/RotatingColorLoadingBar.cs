using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatingColorLoadingBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<Color32> listColors;

    [SerializeField]
    List<SpriteRenderer> listSprite;
    void Start()
    {
        for (int x =0; x < listSprite.Count; x++)
        {
            listSprite[x].color = listColors[x];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
