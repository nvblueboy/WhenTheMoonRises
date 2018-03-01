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
        List<Move> moves = new List<Move>
        {
            // Player moves
            new Move("Stunned"),
            new StandardAttack("Standard Attack"),
            new StrongSwing("Strong Swing"),
            new FlourFlick("Flour Flick"),
            new SpinAttack("Spin Attack"),
            new Guard("Guard"),
            new ItemUse("Item Use")
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
