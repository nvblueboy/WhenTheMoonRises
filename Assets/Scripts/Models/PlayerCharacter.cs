using System;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This is a script representing the player character and their current state
*/

// PlayerCharacter
public class PlayerCharacter : Fighter {
    public int intuition, experience;
    public int coins = 0;
    public List<Item> inventory;
    public int max_size = 10;
   
    public PlayerCharacter() {
        inventory = new List<Item>();
    }

    // levelUp
    private void levelUp()
    {
        level++;
    }
    //returns amount of coins
    public int getCoins()
    {
        return coins;
    }
    //increases amount of coins
    public void increaseCoins(int bigger)
    {
        coins += bigger;
    }
    /*
   Name: increaseIntuition
   Parameters: int increase
   */
    public void inreaseIntuition(int increase)
    {
        intuition += increase;
    }

    /*
    Name: gainExperience
    Parameters: int experience
    */
    public void gainExperience(int experience)
    {
        experience += experience;
        if (Constants.LevelMap[level + 1] <= experience)
        {
            levelUp();
        }
    }   

    // restoreHPAndStamina
    public void restoreHPAndStamina()
    {
        currHP = hp;
        currStamina = stamina;
    }

    /*
    Name: addItem
    Parameters: Item item
    */
    public void addItem(Item item)
    {
        Debug.Log(inventory.Count);
        if (inventory.Count < max_size) {
            inventory.Add(item);
        } else {
            Debug.Log("Not enough room in inventory");
        }
    }

    /*
    Name: removeItem
    Parameters: string item
    */
    public void removeItem(Item item)
    {
       if (inventory.Contains(item)) {
            inventory.Remove(item);
        }
    }

    /*
     * Name: removeItemByName
     * parameters: string name
     */
    public void removeItemByName(string name) {
        foreach(Item i in inventory) {
            if (i.getName() == name) {
                inventory.Remove(i);
                return;
            }
        }
    }

    /*
    Name: getMoves
    Description: Returns a list of Move objects that this PlayerCharacter is authorized to use.
    */
    public Move[] getMoves() {
        //TODO: Actually calculate what moves are possible.
        MoveUtils.InitMoves();
        List<string> names = new List<string>();
        List<Move> moves = new List<Move>();

        if (weapon == "Wrench") {
            names.Add("Bash");
            if (level >= 2) {
                names.Add("Wrench Throw");
            }
            if (level >= 3) {
                names.Add("Two-Handed Swing");
            }
        } else if(weapon == "Fan Sword") {
            if (level >= 4) {
                names.Add("Slash");
                names.Add("Two-Handed Swing");
            }
            if (defense >= 5) {
                names.Add("Pierce The Heart");
            }
        } else {
            Debug.LogWarning("The weapon held by the PlayerCharacter script doesn't have logic!");
        }

        List<string> addedMoves = new List<string>();

        foreach (string name in names) {
            if (!addedMoves.Contains(name)) {
                addedMoves.Add(name);
                moves.Add(MoveUtils.GetMove(name));
            }
        }
        return moves.ToArray();
    }

    /*
    Name: testInventory
    Description: Creates a fake inventory for testing with other components.
    */
    public void testInventory() {
        inventory = new List<Item>();
        inventory.Add(new HealthPotion("fruit_parfait", "Fruit Parfait", 5));
        inventory.Add(new HealthPotion("black_bean_soup", "Black Bean Soup", 10));
        inventory.Add(new HealthPotion("artisanal_sandwich", "Artisanal Sandwich", 20));
        inventory.Add(new HealthPotion("gourmet_pizza", "Gourmet Pizza", 50));
        inventory.Add(new StaminaPotion("water_bottle", "Water Bottle", 5));
        inventory.Add(new StaminaPotion("citrus_cola_can", "Citrus Cola Can", 10));
        inventory.Add(new StaminaPotion("lemonade_jug", "Lemonade Jug", 20));
        inventory.Add(new StaminaPotion("coffee_pot", "Coffee Pot", 50));
    }
}
