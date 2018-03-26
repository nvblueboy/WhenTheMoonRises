using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHandedSwing : Move {

    public TwoHandedSwing(string _name) : base(_name) {
        this.description = "An attack that does 3 damage (1 STA)";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        attacker.spendStamina(1);
        return new Dictionary<string, int> {
            { Constants.HP, 3 }
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return attacker.currStamina >= 1;
    }
}
