using System;
using UnityEngine;

[Serializable]
public class StaminaPotion : Item {

    [SerializeField] int strength;

    public StaminaPotion(string _name, string _displayName) : base(_name, _displayName) {
        
        switch(_name)
        {
            case "water_bottle":
                description = "A small drink that replenishes 5 STA.";
                strength = 5;
                break;
            case "citrus_cola_can":
                description = "A small drink that replenishes 10 STA.";
                strength = 10;
                break;
            case "lemonade_jug":
                description = "A medium drink that replenishes 20 STA.";
                strength = 20;
                break;
            case "coffee_pot":
                description = "A large drink that replenishes 50 STA.";
                strength = 50;
                break;
        }
    }

    public override void affectPlayer(PlayerCharacter p) {
        p.currStamina += strength;
        if (p.currStamina > p.stamina) {
            p.currStamina = p.stamina;
        }
    }
}
