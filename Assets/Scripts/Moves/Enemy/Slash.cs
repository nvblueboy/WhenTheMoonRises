using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Move {

    public Slash(string _name_) : base(_name_) {
        this.description = "0 STA | Does 5 damage.";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        return new Dictionary<string, int> {
            { Constants.HP, 5 }
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return true;
    }
}
