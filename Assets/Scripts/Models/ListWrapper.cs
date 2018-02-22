using System.Collections.Generic;
using System;

[Serializable]
public class ListWrapper {
    public List<object> list;

    public ListWrapper(List<object> _list)
    {
        list = _list;
    }	
}
