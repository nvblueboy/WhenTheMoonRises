using System;

[Serializable]
public class Choice {
    public string text;
    public int next; 
    public Constants.Action actionCode;

    public Choice(int _next, string _text, Constants.Action _actionCode)
    {
        next = _next;
        text = _text;
        actionCode = _actionCode;
    }
    
    public Choice (int _next, string _text)
    {
        next = _next;
        text = _text;
        actionCode = Constants.Action.NONE;
    }	
}
