using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtistSpriteRendererChange : MonoBehaviour
{
    // Start is called before the first frame update
    private Sprite spriteStart;
    public Sprite spriteActive = null;
    public Sprite spriteWhileLooking = null;
    public Sprite spriteSelected = null;
    public Sprite spriteInactive = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        spriteStart = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    public void WhileLookingSprite() {

        gameObject.GetComponent<SpriteRenderer>().sprite = spriteWhileLooking;

    }

    public void ActiveSprite() {

        gameObject.GetComponent<SpriteRenderer>().sprite = spriteActive;
    }

    public void SelectedSprite() {

        gameObject.GetComponent<SpriteRenderer>().sprite = spriteSelected;

    }

    public void OriginalSprite() {

        gameObject.GetComponent<SpriteRenderer>().sprite = spriteStart;

    }

    //Greyed out sprite we disabled the box collider as well
    public void InactiveSprite() {

        gameObject.GetComponent<SpriteRenderer>().sprite = spriteInactive; 
        gameObject.GetComponent<BoxCollider>().enabled = false;

    }


}
