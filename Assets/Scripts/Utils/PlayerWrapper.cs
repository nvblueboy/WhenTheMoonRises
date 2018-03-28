using UnityEngine;

// PlayerWrapper
public class PlayerWrapper {
    public int hp, stamina, strength, defense, coins,
        level, currHP, currStamina, intuition, experience;    
    public string weapon, name;
    public InventoryWrapper inventoryWrapper;

    public PlayerWrapper() { }

    public PlayerWrapper(PlayerCharacter player)
    {
        hp = player.hp;
        stamina = player.stamina;
        strength = player.strength;
        defense = player.defense;
        level = player.level;
        currHP = player.currHP;
        currStamina = player.currStamina;
        weapon = player.weapon;
        name = player.name;
        intuition = player.intuition;
        experience = player.experience;
        coins = player.coins;

        inventoryWrapper = new InventoryWrapper(player.inventory);        
    }

    public PlayerCharacter updatePlayer(PlayerCharacter player)
    {
        player.hp = hp;
        player.stamina = stamina;
        player.currStamina = currStamina;
        player.defense = defense;
        player.level = level;
        player.currHP = currHP;
        player.currStamina = currStamina;
        player.weapon = weapon;
        player.name = name;
        player.inventory = inventoryWrapper.inventory;
        player.intuition = intuition;
        player.experience = experience;
        player.coins = coins;

        return player;
    }
}
