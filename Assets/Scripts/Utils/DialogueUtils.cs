using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueUtils : MonoBehaviour {    

    // storeDialogue
    private static void storeDialogue(DialogueWrapper dialogue, string scene)
    {
        StreamWriter writer = new StreamWriter(File.OpenWrite(
            string.Format(Constants.StoreDialoguePath, scene)));
        string json = JsonUtility.ToJson(dialogue);
        writer.Write(json);        
        writer.Close();
    }

    // storeDialogueFromFile
    public static void storeDialogueFromFile(string source, string destination)
    {
        string line;
        string[] data;
        List<DialogueComponent> dList = new List<DialogueComponent>();
        List<Choice> choices;        
        int lineCount = 0;
        StreamReader reader = new StreamReader(File.OpenRead(source + ".csv"));

        // read to the end of the file line by line
        while(!reader.EndOfStream)
        {
            line = reader.ReadLine();
            choices = new List<Choice>();
            if(lineCount != 0)
            {
                data = line.Split(',');
                data[3] = data[3].Replace('%', ',');

                // init dialogue component
                DialogueComponent component = new DialogueComponent(int.Parse(data[0]),
                    int.Parse(data[1]), data[2], data[3], getActionFromString(data[4]));

                // get choices, if any
                int next = 0;
                string name = "";
                Constants.Action action = Constants.Action.NONE; 
                for(int i = 5; i < data.Length; ++i)
                {                    
                    if(i % 3 == 2) {
                        name = data[i];
                    } else if(i % 3 == 0) {
                        int.TryParse(data[i], out next);
                    } else if(i % 3 == 1) {
                        action = getActionFromString(data[i]);
                        if(name != "")
                        {
                            choices.Add(new Choice(next, name, action));
                        }                        
                    }
                }

                component.choiceWrapper = new ChoiceWrapper(choices);
                dList.Add(component);

                //Debug
                
            }
            ++lineCount;                        
        }
        reader.Close();
        storeDialogue(new DialogueWrapper(dList), destination);        
    }

    // initDialogueForScene
    public static Dictionary<int, DialogueComponent> initDialogueForScene(string scene)
    {
        Dictionary<int, DialogueComponent> retval = 
            new Dictionary<int, DialogueComponent>(); 
             
        TextAsset dialogueText = Resources.Load<TextAsset>(
            string.Format(Constants.DialoguePath, scene));
        
        List<DialogueComponent> dialogueComponents = 
            JsonUtility.FromJson<DialogueWrapper>(dialogueText.text).dialogue;

        foreach(DialogueComponent component in dialogueComponents)
        {
            retval.Add(component.id, component);
        }

        return retval;
    }

    // getActionFromString
    private static Constants.Action getActionFromString(string action)
    {
        Constants.Action retval = Constants.Action.NONE;
        action = action.ToLower();
        if(action.Contains("bookstore")) {
            retval = Constants.Action.OPEN_BOOKSTORE;
        } else if(action.Contains("store")) {
            retval = Constants.Action.OPEN_STORE;
        } else if(action.Contains("prev")) {
            retval = Constants.Action.LOAD_PREV_SCENE;
        } else if(action.Contains("next")) {
            retval = Constants.Action.LOAD_NEXT_SCENE;
        } else if (action.Contains("stren")) {
            retval = Constants.Action.ADD_STRENGTH;
        } else if (action.Contains("stam")) {
            retval = Constants.Action.ADD_STAMINA;
        } else if (action.Contains("intuit")) {
            retval = Constants.Action.ADD_INTUITION;
        } else if (action.Contains("defense")) {
            retval = Constants.Action.ADD_DEFENSE;
        } else if (action.Contains("health")) {
            retval = Constants.Action.ADD_HEALTH;
        } else if (action.Contains("magic")) {
            retval = Constants.Action.ADD_MAGIC;
        }

        return retval;
    }
}
