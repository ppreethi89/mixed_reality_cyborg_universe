using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class DeleteSpeech : MonoBehaviour
{
    /*public string[] words = null;*/
    public GameObject parentobj;
    public TMP_InputField input;
    public string filepath = "savedSpeeches.json";
    public bool startnow = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Delete()
    {
        string word = gameObject.GetComponentInChildren<TextMeshPro>().text;
        StartCoroutine(Save(word));
        Debug.Log("Speech Saved");
        //Destroy(transform.parent.gameObject);
    }
    public IEnumerator Save(string word)
    {
        string fileName = Path.Combine(Application.streamingAssetsPath, filepath);
        Debug.Log(fileName);


        string jsonString = File.ReadAllText(fileName);
        JSONNode jsonNode = JSON.Parse(jsonString);

        List<string> myList = new List<string>();
        foreach (JSONNode saved in jsonNode["Speeches"])
        {
            if (saved == word)
            {
                continue;
            }
            else
            {;
                myList.Add(saved);
            }
        }


        

        for (int i = 0; i < myList.Count; i++)
        {
            jsonNode["Speeches"][i] = myList[i]; //Append each word from list
        }

        for (int i = jsonNode["Speeches"].Count-1; i > myList.Count-1; i--)
        {
            jsonNode["Speeches"].Remove(i); //delete excess from list
        }
        Debug.Log(jsonNode["Speeches"]);


        File.WriteAllText(fileName, jsonNode.ToString());
        yield return true;
    }
}
