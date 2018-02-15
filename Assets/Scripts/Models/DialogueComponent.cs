using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueComponent
{
    public string speaker, text;
    public int id, nextId;
    public List<Choice> choices;

    public DialogueComponent(int _id, int _nextId, string _speaker, string _text, List<Choice> _choices)
    {
        speaker = _speaker;
        text = _text;
        id = _id;
        nextId = _nextId;
        choices = _choices;
    }

    // Next
    public int Next()
    {
        if (choices != null && choices.Count > 0)
        {
            // Don't display new dialogue. Display choices instead
            return -1;
        }

        return nextId;
    }
}

