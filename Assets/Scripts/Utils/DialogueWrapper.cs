using System;
using System.Collections.Generic;

[Serializable]
public class DialogueWrapper {
    public List<DialogueComponent> dialogue;

    public DialogueWrapper(List<DialogueComponent> _dialogue)
    {
        dialogue = _dialogue;
    }	
}