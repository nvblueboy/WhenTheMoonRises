using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : Move {

    public Bite(string _name_) : base(_name_) {
        this.description = "1 STA | Does 3 damage.";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        attacker.spendStamina(1);
        return new Dictionary<string, int>
        {
            {Constants.HP,  3}
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return attacker.stamina >= 1;
    }
}
