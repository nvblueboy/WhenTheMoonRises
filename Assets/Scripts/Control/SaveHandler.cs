using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This is a script for handling saving game data
*/

// SaveHandler
public class SaveHandler : MonoBehaviour {
       
    // Save
    public void Save()
    {
        Debug.Log("Game saved");
        SavePlayerPosition();        
    }
    
    // SavePlayerPosition
    void SavePlayerPosition()
    {
        PlayerPrefs.SetString(Constants.SaveExists, "true");
        PlayerPrefs.SetFloat(Constants.PlayerX, transform.position.x);
        PlayerPrefs.SetFloat(Constants.PlayerY, transform.position.y);
    }	
}
