﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleWarp : Move {

    public BlackHoleWarp(string _name) : base(_name) {
        this.description = "1 STA | Become invulnerable for 1 turn.";
        this.animateAttacker = true;
        this.animateDefender = false;
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        attacker.spendStamina(1);
        return new Dictionary<string, int> {
            { Constants.Invulnerability, 1 }
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return attacker.currStamina >= 1;
    }
}
