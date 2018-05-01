using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceTheHeart : Move {

	public PierceTheHeart(string _name) : base(_name) {
        this.description = "6 STA | A startling attack that can potentially stun the enemy as well as cause damage.";
    }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        attacker.spendStamina(6);
        Dictionary<string, int> outDict = new Dictionary<string, int> { { Constants.HP, 8 } };

        if (Random.value < .2) {
            outDict.Add(Constants.Stunned, 1);
        }

        return outDict;
    }

    public override bool moveEligible(Fighter attacker) {
        return attacker.currStamina >= 6;
    }
}
