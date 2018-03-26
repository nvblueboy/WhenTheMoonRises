using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchThrow : Move {
    public WrenchThrow(string _name) : base(_name) {
        this.description = "An attack that does 4 damage (2 STA)";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        attacker.spendStamina(2);
        return new Dictionary<string, int> {
            { Constants.HP, 4 }
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return attacker.currStamina >= 2;
    }
}
