using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This is the parent interaction script with base functionality
*/

// Interaction
public abstract class Interaction : MonoBehaviour {
    public string preReq, resultItem;
    protected bool hasInteracted;
    protected PlayerCharacter player;
    
    // Awake
    void Awake()
    {
        hasInteracted = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }    

    // hasPreReq
    public bool hasPreReq()
    {
        foreach(string item in player.inventory)
        {
            if(item == preReq)
            {
                return true;
            }
        }
        return false;
    }

    // interact
    public abstract void interact();	
}
