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
            if (successText.Length == 0)
            {                
                successText = new string[] { resultItem + " was added to your inventory." };
            }

            if (!hasInteracted)
            {
                player.addItem(resultItem);
                hasInteracted = true;                
                
                GameController.showDialogue(successText, displayDialogue, gameObject.name);

                if (gameObject.name.Contains("pickup"))
                {
                    //Destroy(gameObject);
                    gameObject.SetActive(false);
                }

                if (removePreReq)
                {
                    player.removeItem(preReq);
                }
            }
            return;                        
        }
        GameController.showDialogue(failText, displayDialogue, gameObject.name); 
    }
}
