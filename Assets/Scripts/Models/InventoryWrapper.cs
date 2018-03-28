using System.Collections.Generic;
using System;

[Serializable]
public class InventoryWrapper
{
    public List<Item> inventory;

    public InventoryWrapper(List<Item> _inventory)
    {
        inventory = _inventory;
    }
}

