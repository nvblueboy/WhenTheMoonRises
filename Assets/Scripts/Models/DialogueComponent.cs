using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueComponent
{
    public string speaker, text;
    public int id, nextId;
    public ChoiceWrapper choiceWrapper;

    public DialogueComponent(int _id, int _nextId, string _speaker, string _text, ChoiceWrapper _choiceWrapper)
    {
        speaker = _speaker;
        text = _text;
        id = _id;
        nextId = _nextId;
        choiceWrapper = _choiceWrapper;
    }

    public DialogueComponent(int _id, int _nextId, string _speaker, string _text)
    {
        speaker = _speaker;
        text = _text;
        id = _id;
        nextId = _nextId;
        choiceWrapper = new ChoiceWrapper();
    }

    // Next
    public int Next()
    {
        if (choiceWrapper.choices.Count > 0)
        {
            // Don't display new dialogue. Display choices instead
            return -1;
        }

        return nextId;
    }
}

