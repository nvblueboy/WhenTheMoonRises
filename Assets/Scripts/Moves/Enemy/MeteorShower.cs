using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShower : Move {

    public MeteorShower(string _name_) : base(_name_) {
        this.description = "3 STA | Does 6 damage.";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        attacker.spendStamina(3);
        return new Dictionary<string, int>
        {
            {Constants.HP,  6}
        };
    }

    public override bool moveEligible(Fighter attacker) {
        return attacker.stamina >= 3;
    }
}
