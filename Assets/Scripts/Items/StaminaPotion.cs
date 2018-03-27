using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPotion : Item {

    int strength;

    public StaminaPotion(string _name, string _displayName, int _strength) : base(_name, _displayName) {
        strength = _strength;
        description = "This item adds " + _strength.ToString() + " stamina.";
    }

    public override void affectPlayer(PlayerCharacter p) {
        p.currStamina += strength;
        if (p.currStamina > p.stamina) {
            p.currStamina = p.stamina;
        }
    }
}
