using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Choice {
    public string text, actionCode;
    public int next;

    public Choice(int _next, string _text, string _actionCode)
    {
        next = _next;
        text = _text;
        actionCode = _actionCode;
    }	
}
