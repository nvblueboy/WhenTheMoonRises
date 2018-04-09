using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealthPotion : Item {
    [SerializeField] int strength;

    public HealthPotion(string _name, string _displayName, int _strength) : base(_name, _displayName) {
        strength = _strength;
        description = "This item heals " + _strength.ToString() + " HP.";
        if(_name == "fruit_parfait") {
            description = "A small snack that replenishes 5 HP.";
        } else if(_name == "black_bean_soup") {
            description = "A meal on-the-go that replenishes 10 HP.";
        } else if(_name == "artisanal_sandwich") {
            description = "A meal on-the-go that replenishes 20 HP.";
        } else if (_name == "gourmet_pizza") {
            description = "A large meal that replenishes 50 HP.";
        }
    }

    public override void affectPlayer(PlayerCharacter p) {
        p.currHP += strength;
        if (p.currHP > p.hp) {
            p.currHP = p.hp;
        }
    }
}
