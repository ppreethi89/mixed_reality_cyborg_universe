using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class StaticViewEmailBody : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject staticViewEmailPrefab;
    public static TextMeshProUGUI staticBodyTxt;
    public static GameObject staticEyeGazeRegion;
    public static Animator staticAnimator;


    public static StaticViewEmailBody instance;
    public GameObject viewEmailPrefab;
    public TextMeshProUGUI bodyTxt;
    public GameObject eyeGazeRegion;
    public Animator animator;
    public GameObject quickReplyPanel;

    public static string recipientField;
    public static string replySubject;
    public static string emailContent;

    private void Awake()
    {
        //helper to convert non static to static var

        staticViewEmailPrefab = viewEmailPrefab;
        staticBodyTxt = bodyTxt;
        staticEyeGazeRegion = eyeGazeRegion;
        staticAnimator = animator;
        instance = this;
    }
    void Start()
    {
        //animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void OpenStaticViewEmailPrefab(string body)
    {
        //instance.animator.SetBool("IsDwell", true);
        //staticViewEmailPrefab.SetActive(true);
        staticAnimator.SetBool("IsDwell", true);
        staticBodyTxt.text = body;
        //instance.StartCoroutine(ActivateEyeGazeRegion());
    }
    public void CloseStaticViewEmailPrefab()
    {
        //instance.animator.SetBool("IsDwell", false);
        staticAnimator.SetBool("IsDwell", false);
        //staticViewEmailPrefab.SetActive(false);
        //staticEyeGazeRegion.SetActive(false);
    }
    static IEnumerator ActivateEyeGazeRegion()
    {
        yield return new WaitForSeconds(1f);
        staticEyeGazeRegion.SetActive(true);


    }
    public void ShowQuickReply()
    {
        quickReplyPanel.SetActive(true);
    }
}
