using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingAnim : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 originalScale;
    Vector3 currentScale;
    private void Awake()
    {
        originalScale = this.gameObject.transform.localScale;
    }
    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0f, 0f, gameObject.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        ScaleUp();
    }
    private void ScaleUp()
    {
        if(this.gameObject.transform.localScale.x <= originalScale.x)
        {
            this.gameObject.transform.localScale += new Vector3(0.05f, 0f, 0);
        }
        if(this.gameObject.transform.localScale.y <= originalScale.y)
        {
            this.gameObject.transform.localScale += new Vector3(0.0f, 0.05f, 0);
        }
    }
}
