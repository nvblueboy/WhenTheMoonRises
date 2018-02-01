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
    public string resultItem;

	// interact
    public override void interact()
    {              
        if(hasPreReq())
        {
            if (useDefaultText)
            {
                successText = new string[] { resultItem + " was added to your inventory." };
            }

            if (!hasInteracted)
            {
                player.addItem(resultItem);
                hasInteracted = true;
                Debug.Log(resultItem + " was added to your inventory.");
                
                showDialogue(true);

                if (gameObject.name.Contains("pickup"))
                {
                    Destroy(gameObject);
                }

                if (removePreReq)
                {
                    player.removeItem(preReq);
                }
            }
            return;                        
        }
        showDialogue(false); 
    }
}
