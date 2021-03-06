﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This is a class representing a move to be used in the Fight scene by a Fighter
*/

// Move
public class Move {    
    public string name;
    public string description;
    public bool animateDefender;
    public bool animateAttacker;

    /*
    Name: Move
    Parameters: string _name
   */
   public Move(string _name)
   {
        this.name = _name;
        this.description = "This move does not have a description.";
        this.animateDefender = true;
        this.animateAttacker = false;
   }

   /*
  Name: processMove
  Parameters: Fighter attacker, Fighter target
  Returns: Dictionary<string, int>
  */
    public virtual Dictionary<string, int> processMove(Fighter attacker, Fighter target)
    {

        return null;
    }

    /*
  Name: moveEligible
  Parameters: Fighter attacker
  Returns: bool
  */
    public virtual bool moveEligible(Fighter attacker)
    {
        return true;
    }
}
