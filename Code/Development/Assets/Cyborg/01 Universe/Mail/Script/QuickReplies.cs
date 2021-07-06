using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickReplies{

    private static List<string> quickReplies;    

    private static void initReplies() {
        quickReplies = new List<string>();
        quickReplies.Add("Thanks for the Mail");
        quickReplies.Add("Thanks!, best wishes");
        quickReplies.Add("Thanks!,Good Luck!");
        quickReplies.Add("Great Work!");
        quickReplies.Add("Good Work!");
        quickReplies.Add("Sounds Good!");
        quickReplies.Add("Apologies");
        quickReplies.Add("Will get back to you shortly");
        quickReplies.Add("Will do!");
        quickReplies.Add("Sounds Good");
        quickReplies.Add("Thanks Team!");
    }

    public static List<string> GetRandomResponses() {

        if (quickReplies == null){
            initReplies();
        }

        int maxResponses = 3;
        List<string> responses = new List<string>();               

        int randomValue = Random.Range(1, quickReplies.Count);
        while (responses.Count <= maxResponses) {
            Debug.Log($"Responses Count is : {responses.Count}");
            responses.Add(quickReplies[randomValue]);
            randomValue = Random.Range(1, quickReplies.Count);
        }
        
        return responses;
    }
   
}
