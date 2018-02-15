using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DialogueUtils : MonoBehaviour {

    private static Dictionary<int, string[]> nightOneDialogue = new Dictionary<int, string[]>() {
        {1, new string[] { "Sunny", "It's kind of eerie out here..." } },
        {2, new string[] { "Sunny", "Guess I better start looking for those shards." } },
        {3, new string[] { "", "You find a ladder!\nLadder added to inventory." } },
        {4, new string[] { "Sunny", "Hope no one minds if I borrow this." } },
        {5, new string[] { "", "You pick up the rock.\nRock added to inventory." } },
        {6, new string[] { "", "You search the well. " } },
        {7, new string[] { "Sunny", "Looks like there's something in here." } },
        {8, new string[] { "Sunny", "I can see a shard up there in the branches, but I can't reach it." } },
        {9, new string[] { "Sunny", "The ladder should help me shake it down from the branch." } },
        {10, new string[] { "Sunny", "Oh. I've got to be careful not to spook it." } },
        {11, new string[] { "Sunny", "I can see something glowing down there, but it's too far down." } },
        {12, new string[] { "", "You place the rock on top of the stump." } },
        {13, new string[] { "Sunny", "No... I don't know how'd that help." } },
        {14, new string[] { "Sunny", "Gah! There's one of those... things... " } },
        {15, new string[] { "Sunny", "It's getting late. I should head back home." } },
    };


    public static Dictionary<int, string[]> getDialogueForScene()
    {
        string scene = SceneManager.GetActiveScene().name;
        Dictionary<int, string[]> retval = null;

        switch(scene)
        {
            case "Night1":
                retval = nightOneDialogue;
                break;
            /*case "Day1":
                break;
                .
                .
                .*/
        }
        return retval;
    }	

    public static void storeDialogue(DialogueComponent dialogue)
    {
        string json = JsonUtility.ToJson(dialogue);
        StreamWriter writer = File.AppendText("test.json");
        writer.WriteLine(json);       
        writer.Close();
    }

    public static void initDialogueForCurrentScene()
    {
        string scene = SceneManager.GetActiveScene().name;
        StreamReader reader = new StreamReader(File.OpenRead("test.json"));
        
        while(!reader.EndOfStream)
        {
            //DialogueTest dialogue = JsonUtility.FromJson<DialogueTest>(reader.ReadLine());
            //Debug.Log("Dialogue text: " + dialogue.text);
        }
    }
}
