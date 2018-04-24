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
        List<Move> moves = new List<Move>();

        Debug.Log(MoveUtils.getMoveSet());

        //if (weapon == "wrench") {
            moves.Add(MoveUtils.GetMove("Bash"));
            moves.Add(MoveUtils.GetMove("Two Handed Swing"));
            moves.Add(MoveUtils.GetMove("Wrench Throw"));
            moves.Add(MoveUtils.GetMove("Pierce The Heart"));
       // }

        moves.Add(MoveUtils.GetMove("Black Hole Warp"));
        return moves.ToArray();
    }

    /*
    Name: testInventory
    Description: Creates a fake inventory for testing with other components.
    */
    public void testInventory() {
        inventory = new List<Item>();
        inventory.Add(new HealthPotion("fruit_parfait", "Fruit Parfait"));
        inventory.Add(new HealthPotion("black_bean_soup", "Black Bean Soup"));
        inventory.Add(new HealthPotion("artisanal_sandwich", "Artisanal Sandwich"));
        inventory.Add(new HealthPotion("gourmet_pizza", "Gourmet Pizza"));
        inventory.Add(new StaminaPotion("water_bottle", "Water Bottle"));
        inventory.Add(new StaminaPotion("citrus_cola_can", "Citrus Cola Can"));
        inventory.Add(new StaminaPotion("lemonade_jug", "Lemonade Jug"));
        inventory.Add(new StaminaPotion("coffee_pot", "Coffee Pot"));
    }
}
