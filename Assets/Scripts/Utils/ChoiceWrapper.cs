using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChoiceWrapper {
    public List<Choice> choices;
    
    public ChoiceWrapper()
    {
        choices = new List<Choice>();
    }	

    public ChoiceWrapper(List<Choice> _choices)
    {
        choices = _choices;
    }
}