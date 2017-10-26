using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongSwing : Move {

	public StrongSwing(string _name) : base(_name) { }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target)
    {
        int damage = 0;
        switch(attacker.weapon)
        {
            case "Rolling Pin":
                damage = 2 + (int)Mathf.Round(.25f * attacker.strength);
                break;
            case "Frying Pan":
                damage = 4 + (int)Mathf.Round(.25f * attacker.strength);
                break;
        }

        return new Dictionary<string, int>()
        {
            {Constants.HP, damage }
        };
    }

    public override bool moveEligible(Fighter attacker)
    {
        return attacker.currStamina >= 2;
    }
}
