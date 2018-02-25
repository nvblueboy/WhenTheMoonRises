using System.Collections;
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
    public string[] inventory;
    public int coins = 0;
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
    Parameters: string item
    */
    public void addItem(string item)
    {
        for (int i = 0; i < inventory.Length; ++i)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                return;
            }
        }
        if (inventory.Length == 0)
        {
            inventory = new string[20];
            inventory[0] = item;
            return;
        } 
        Debug.Log("Not enough room in inventory");
    }

    /*
    Name: removeItem
    Parameters: string item
    */
    public void removeItem(string item)
    {
        for (int i = 0; i < inventory.Length; ++i)
        {
            if (inventory[i] == item)
            {
                inventory[i] = "";
                return;
            }
        }
    }
}
