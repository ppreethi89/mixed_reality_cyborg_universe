using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChange_General : MonoBehaviour
{
    private Sprite spriteStart;
    public Sprite spriteWhileLooking;
    public Sprite spriteOnDwell;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Awake()
    {
        spriteStart = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void WhileLookingSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteWhileLooking;
    }

    public void OnDwellSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteOnDwell;
    }

    public void OriginalSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteStart;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
