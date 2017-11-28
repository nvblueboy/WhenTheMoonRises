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
    public Vector3 spawnPosition;

    // interaction
    public override void interact()
    { 
        if (hasPreReq() && !hasInteracted)
        {
            // spawns the resulting interaction item at the specified location
            Instantiate(Resources.Load("Items/" + resultItem), spawnPosition, new Quaternion());
        }
    }	
}
