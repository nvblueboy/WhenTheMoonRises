using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealthPotion : Item {
    [SerializeField] int strength;

    public HealthPotion(string _name, string _displayName) : base(_name, _displayName) {        
        
        if(_name.Contains("fruit")) {
            description = "A small snack that replenishes 3 HP.";
            strength = 3;
        } else if(_name.Contains("black")) {
            description = "A meal on-the-go that replenishes 5 HP.";
            strength = 5;
        } else if(_name.Contains("artisanal")) {
            description = "A meal on-the-go that replenishes 7 HP.";
            strength = 7;
        } else if (_name.Contains("gourmet")) {
            description = "A large meal that replenishes 10 HP.";
            strength = 10;
        }
    }

    public override void affectPlayer(PlayerCharacter p) {
        p.currHP += strength;
        if (p.currHP > p.hp) {
            p.currHP = p.hp;
        }
    }
}
