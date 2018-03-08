using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item {
    int strength;

    public HealthPotion(string _name, string _displayName, int _strength) : base(_name, _displayName) {
        strength = _strength;
    }

    public override void affectPlayer(PlayerCharacter p) {
        p.currHP += strength;
        if (p.currHP > p.hp) {
            p.currHP = p.hp;
        }
    }
}
