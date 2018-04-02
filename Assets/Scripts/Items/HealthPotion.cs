using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealthPotion : Item {
    [SerializeField] int strength;

    public HealthPotion(string _name, string _displayName, int _strength) : base(_name, _displayName) {
        strength = _strength;
        description = "This item heals " + _strength.ToString() + " HP.";
    }

    public override void affectPlayer(PlayerCharacter p) {
        p.currHP += strength;
        if (p.currHP > p.hp) {
            p.currHP = p.hp;
        }
    }
}
