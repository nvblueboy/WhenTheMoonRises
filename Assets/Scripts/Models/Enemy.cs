using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This is a script representing an Enemy and its current state
*/

// Enemy
public class Enemy : Fighter {


    // Awake
    void Awake()
    {

    }

    public string getMove()
    {
        string selectedMove = getSelectedMove(false);
        if (selectedMove == null)
        {
            if(currStamina <= 0) {
                addSelectedMove("NoStamina");
            } else {
                //This is where all fancy logic will go to process what the enemy will do.
                string[] moves = { "Standard Attack", "Strong Swing", "Spin Attack" };
                selectedMove = moves[Random.Range(0, 2)];
                addSelectedMove(selectedMove);
            }
        }
        //For now...
        return getSelectedMove(true);
    }
}


