using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using SimpleJSON;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;

public class CyborgFileRead_General
{
    //This function read a file from the Streaming Asset and return a string
    public static string ReadFileStreamingAsset(string fileName)
    {
        
        string file = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(file))
        {
            string data = File.ReadAllText(file);
            return data;
        }
        else
        {
            Debug.Log("No such file exists");
            return null;
        }
       
    }

    //This function reads a file from the Persistent Data Path (App Data) and return a string
    public static string ReadFileApplicationData(string fileName)
    {
        string file = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(file))
        {
            string data = File.ReadAllText(file);
            return data;
        }
        else
        {
            Debug.Log("No such file exists");
            return null;
        }
    }

    //This function writes a string to a file in the Persistent Data Path (App Data).
    public static void WriteFileApplicationData(string fileName, string dataToWrite)
    {
        string file = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(file, dataToWrite);
        Debug.Log("Data has been successfully written");
        

    }

}
