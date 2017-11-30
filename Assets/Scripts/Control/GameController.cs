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
    public PlayerCharacter player;    

    // Awake
    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update
	void Update () {
		
	}    
}
