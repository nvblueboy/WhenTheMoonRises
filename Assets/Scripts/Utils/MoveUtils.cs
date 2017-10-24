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
        List<Move> moves = new List<Move>
        {
            new Move("Punch", 10, 10, 1),
            new Move("Fireball", 50, 40, 1),
            new Move("Kick", 20, 20, 1)
        };


        moveDict = new Dictionary<string, Move>();
        foreach (Move m in moves)
        {
            moveDict.Add(m.name, m);
        }
    }

    /*
   Name: Moves
   Parameters: string name
   Returns: Dictionary<string, Move> 
   */
    public static Move GetMove(string name)
    {
        return moveDict[name];        
    }   	
}
