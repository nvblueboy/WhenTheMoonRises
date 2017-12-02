using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: Script for interactions that spawn items in the world
*/

// WorldInteraction
public class WorldInteraction : Interaction {
    public GameObject resultItem;
    public Vector2 spawnOffset;

    // interaction
    public override void interact()
    { 
        if (hasPreReq() && !hasInteracted)
        {
            // spawns the resulting interaction item at the specified location
            Vector3 spawnLocation = new Vector3(transform.position.x + spawnOffset.x,
                1.5f, transform.position.z + spawnOffset.y);
            GameObject resultItemClone = Instantiate(resultItem,
                spawnLocation, transform.rotation);
            hasInteracted = true;

            if(removePreReq)
            {
                player.removeItem(preReq);
            }
        }
    }	
}
