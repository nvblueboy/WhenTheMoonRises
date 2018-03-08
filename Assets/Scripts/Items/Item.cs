using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Item
 * Dylan Bowman
 * 2250585
 * bowma128@mail.chapman.edu
 * Description: A base class for all items to be put in the player's inventory.
 */
[System.Serializable]
public class Item {
    private string name;
    private string displayName;

    public Item(string _name, string _displayName) {
        name = _name;
        displayName = _displayName;
    }

    public virtual void affectPlayer(PlayerCharacter p) {
        return; //This doesn't have any impact on a player.
    }

    public string getName() {
        return name;
    }

    public string getDisplayName() {
        return displayName;
    }
}
