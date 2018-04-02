using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : Move {

    public Scratch(string _name) : base(_name) {
        this.description = "An attack that does 4 damage (2 STA)";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        return new Dictionary<string, int> {
            { Constants.HP, 1 }
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return true;
    }
}
