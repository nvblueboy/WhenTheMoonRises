using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This class initializes moves and holds a Dictionary of them
*/

// MoveUtils
public class MoveUtils {
    private static Dictionary<string, Move> moveDict;
   
   // InitMoves
    public static void InitMoves()
    {
        // Initialize moves here
        Move sampleMove = new Move("name", 1, 1, 1);

        // Add moves to dictionary here
        moveDict.Add(sampleMove.name, sampleMove);
    }

    /*
   Name: Moves
   Returns: Dictionary<string, Move> 
   */
    public static Dictionary<string, Move> Moves()
    {
        return moveDict;
    }   	
}
