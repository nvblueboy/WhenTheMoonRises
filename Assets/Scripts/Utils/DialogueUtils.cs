using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueUtils : MonoBehaviour {    

    public static void storeDialogue(DialogueWrapper dialogue, string scene)
    {
        StreamWriter writer = new StreamWriter(File.OpenWrite(
            string.Format(Constants.BuildDialoguePath, scene)));
        string json = JsonUtility.ToJson(dialogue);
        writer.Write(json);        
        writer.Close();
    }

    public static Dictionary<int, DialogueComponent> initDialogueForScene(string scene)
    {
        Dictionary<int, DialogueComponent> retval = new Dictionary<int, DialogueComponent>();

        TextAsset text = Resources.Load<TextAsset>(scene);

        // Read in a list of DialogueComponents from JSON
        List<DialogueComponent> dialogue =  JsonUtility.
            FromJson<DialogueWrapper>(text.text).dialogue;

        foreach(DialogueComponent component in dialogue)
        {            
            retval.Add(component.id, component);
        }

        return retval;
    }    
}
