using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealWounds : Move {

    public HealWounds(string _name) : base(_name) {
        this.description = "2 STA | Heal 5 HP.";
        this.animateAttacker = true;
        this.animateDefender = false;
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        attacker.spendStamina(2);
        return new Dictionary<string, int> {
            { Constants.Heal, 5 }
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return attacker.currStamina >= 2;
    }
}
