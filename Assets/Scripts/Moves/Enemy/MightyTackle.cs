using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MightyTackle : Move {

    public MightyTackle(string _name) : base(_name) {
        this.description = "4 STA | Stun the enemy for 1 turn.";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        attacker.spendStamina(4);
        return new Dictionary<string, int>
        {
            {Constants.Stunned,  1}
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return attacker.stamina >= 4;
    }
}
