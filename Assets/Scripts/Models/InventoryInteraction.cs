using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: Script for interactions that add an item to the player's inventory
*/

// InventoryInteraction
public class InventoryInteraction : Interaction {

	// interact
    public override void interact()
    {
        if(hasPreReq() && !hasInteracted)
        {
            player.addItem(resultItem);
            Debug.Log(resultItem + " was added to you inventory.");            
        }
    }
}
