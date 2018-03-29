using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHandedSwing : Move {

    public TwoHandedSwing(string _name) : base(_name) {
        this.description = "1 STA | Does 4 damage.";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        attacker.spendStamina(1);
        return new Dictionary<string, int> {
            { Constants.HP, 4 }
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return attacker.currStamina >= 1;
    }
}
