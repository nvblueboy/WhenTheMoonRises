using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealthPotion : Item {
    [SerializeField] int strength;

    public HealthPotion(string _name, string _displayName) : base(_name, _displayName) {        
        
        if(_name.Contains("fruit")) {
            description = "A small snack that replenishes 5 HP.";
            strength = 5;
        } else if(_name.Contains("black")) {
            description = "A meal on-the-go that replenishes 10 HP.";
            strength = 10;
        } else if(_name.Contains("artisanal")) {
            description = "A meal on-the-go that replenishes 20 HP.";
            strength = 20;
        } else if (_name.Contains("gourmet")) {
            description = "A large meal that replenishes 50 HP.";
            strength = 50;
        }
    }

    public override void affectPlayer(PlayerCharacter p) {
        p.currHP += strength;
        if (p.currHP > p.hp) {
            p.currHP = p.hp;
        }
    }
}
