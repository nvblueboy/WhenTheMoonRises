﻿using System.Collections;
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
   
    // levelUp
    private void levelUp()
    {
        level++;
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
        for(int i = 0; i < inventory.Length; ++i)
        {
            if(inventory[i] == "")
            {
                inventory[i] = item;
                return;
            }
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

    /*
    Name: getMoves
    Description: Returns a list of Move objects that this PlayerCharacter is authorized to use.
    */
    public Move[] getMoves() {
        //TODO: Actually calculate what moves are possible.
        MoveUtils.InitMoves();
        return new Move[] { MoveUtils.GetMove("Standard Attack"), MoveUtils.GetMove("Strong Swing"), MoveUtils.GetMove("Flour Flick") };
    }

    /*
    Name: testInventory
    Description: Creates a fake inventory for testing with other components.
    */
    public void testInventory() {
        inventory = new string[]{ "Mushroom", "Fire Flower", "Star", "1-Up"};
    }
}
