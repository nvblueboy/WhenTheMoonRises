using System;
using UnityEngine;

[Serializable]
public class StaminaPotion : Item {

    [SerializeField] int strength;

    public StaminaPotion(string _name, string _displayName, int _strength) : base(_name, _displayName) {
        strength = _strength;
    }

    public override void affectPlayer(PlayerCharacter p) {
        p.currStamina += strength;
        if (p.currStamina > p.stamina) {
            p.currStamina = p.stamina;
        }
    }
}
