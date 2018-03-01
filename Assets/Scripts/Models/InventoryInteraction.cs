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
            if (!hasInteracted)
            {                
                feedbackController.showFeedback(successText, gameObject.name, this);
                if(!delayAction)
                {
                    triggerAction();
                }                
            }
            return;                        
        }
        feedbackController.showFeedback(failText, gameObject.name, this); 
    }

    // triggerAction
    public override void triggerAction()
    {
        player.addItem(resultItem);
        hasInteracted = true;

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
}
