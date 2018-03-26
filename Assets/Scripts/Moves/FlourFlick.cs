using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlourFlick : Move {
    public FlourFlick(string _name) : base(_name) {
        this.description = "Stun the enemy for 2 turns.";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target)
    {
        attacker.spendStamina(2);
        return new Dictionary<string, int>
        {
            {Constants.Stunned,  2}
        };
    }

    public override bool moveEligible(Fighter attacker)
    {
        return attacker.stamina >= 2;
    }
}
