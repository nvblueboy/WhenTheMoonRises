using System;
using UnityEngine;

[Serializable]
public class StaminaPotion : Item {

    [SerializeField] int strength;

    public StaminaPotion(string _name, string _displayName) : base(_name, _displayName) {
        
        if(_name.Contains("water"))
        {
            description = "A small drink that replenishes 3 STA.";
            strength = 3;
        }
        else if(_name.Contains("cola"))
        {
            description = "A small drink that replenishes 5 STA.";
            strength = 5;
        }
        else if(_name.Contains("jug"))
        {
            description = "A medium drink that replenishes 7 STA.";
            strength = 7;
        } 
        else if(_name.Contains("pot"))
        {
            description = "A large drink that replenishes 10 STA.";
            strength = 10;
        }        
    }

    public override void affectPlayer(PlayerCharacter p) {
        p.currStamina += strength;
        if (p.currStamina > p.stamina) {
            p.currStamina = p.stamina;
        }
    }
}
