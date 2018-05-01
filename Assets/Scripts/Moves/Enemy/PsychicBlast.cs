using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsychicBlast : Move {

    public PsychicBlast(string _name_) : base(_name_) {
        this.description = "0 STA | Does 3 damage.";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        return new Dictionary<string, int>
        {
            {Constants.HP,  3}
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return true;
    }
}
