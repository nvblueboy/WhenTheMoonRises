using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlourFlick : Move {
    public FlourFlick(string _name) : base(_name) { }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target)
    {
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
