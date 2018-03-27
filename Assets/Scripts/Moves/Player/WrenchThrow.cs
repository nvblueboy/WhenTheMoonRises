using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchThrow : Move {
    public WrenchThrow(string _name) : base(_name) {
        this.description = "Hits the player in the front and the back.";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        attacker.spendStamina(2);
        return new Dictionary<string, int> {
            { Constants.HP, 2 },
            { Constants.GuaranteedHP, 2 }
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return attacker.currStamina >= 2;
    }
}
