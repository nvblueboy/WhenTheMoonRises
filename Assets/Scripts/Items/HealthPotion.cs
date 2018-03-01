using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item {
    int strength;

    public HealthPotion(string _name, string _displayName, int _strength) : base(_name, _displayName) {
        strength = _strength;
    }

    public void affectPlayer(PlayerCharacter p) {
        Debug.Log("Affecting");
        p.currHP += strength;
    }
}
