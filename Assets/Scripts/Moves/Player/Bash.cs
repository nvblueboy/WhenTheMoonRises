using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : Move {

    public Bash(string _name) : base(_name) {
        this.description = "0 STA | Does 2 damage.";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        return new Dictionary<string, int> {
            { Constants.HP, 2 }
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return true;
    }

}