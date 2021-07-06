using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgShowPanel_General : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> panelList;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowPanel(GameObject panel)
    {
        HidePanels();
        panel.SetActive(true);
    }
    void HidePanels()
    {
        foreach(GameObject panel in panelList)
        {
            panel.SetActive(false);
        }
    }
}
