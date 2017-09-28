using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This is a script for controlling changes to the game state
*/

// GameController
public class GameController : MonoBehaviour {
    private static GameController instance;
    
    // Instance
    public static GameController Instance
    {
        get { return instance; }
    }

    // Awake
    void Awake () {

        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update
	void Update () {
		
	}    
}
