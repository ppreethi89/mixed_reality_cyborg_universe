using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using SimpleJSON;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;
public class UWPFileWrite : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField input;
    public TMP_InputField loadInput;
    [Serializable]
    public class TestUWP
    {
        public string test;
    }
    void Start()
    {
        /* TestUWP x = new TestUWP();
         x.test = "Anyeong";
         BinaryFormatter formatter = new BinaryFormatter();
         string path = Application.persistentDataPath + "/TestUWP.txt";
         FileStream stream = new FileStream(path, FileMode.Create);
         formatter.Serialize(stream, x);
         stream.Close();*/
        CreateFile();

        Debug.Log(Application.persistentDataPath);
        
    }
    void CreateFile()
    {
        string path = Application.persistentDataPath + "/uwpTestFile3.json";
        if (File.Exists(path))
        {

            
            Debug.Log("There is a file existing already");
            

            //stream.Close();

        }
        else
        {
            string filepath = "savedSpeeches.json";
            string data = CyborgFileRead_General.ReadFileStreamingAsset(filepath);
            File.WriteAllText(path, data);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveData()
    {
        string x = input.text;
        string path = Application.persistentDataPath + "/uwpTestFile3.json";
        if (File.Exists(path))
        {
            
            string data = File.ReadAllText(path);
            JSONNode jsonNode = JSON.Parse(data);

            jsonNode["test"] = x;
            Debug.Log("The data replaced is " + x);
            //File.WriteAllText(path, jsonNode.ToString());
            File.AppendAllText(path, x);
            //stream.Close();

        }
        else
        {
            Debug.LogError("File not found in " + path);
        }
    }
    public void LoadData()
    {
       
        string path = Application.persistentDataPath + "/uwpTestFile3.json";
        if (File.Exists(path))
        {
/*            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            TestUWP data = (TestUWP)formatter.Deserialize(stream);
            Debug.Log(data.test);*/
            string data = File.ReadAllText(path);
            //JSONNode jsonNode = JSON.Parse(data);
            //string JSONText = jsonNode["test"];
            Debug.Log("The string data is " + data);

            loadInput.text = data;
            //stream.Close();
           
        }
        else
        {
            Debug.LogError("File not found in " + path);
        }

    }

}
