using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This is a script for handling loading game data
*/

// LoadHandler
public class LoadHandler : MonoBehaviour {

    // Load
	public void Load()
    {
        Debug.Log("Attempt to load");
        if(canLoad())
        {
            LoadPlayerPosition();
        }
    }

    // LoadPlayerPosition
    void LoadPlayerPosition()
    {
        Vector2 playerPosition = new Vector2(PlayerPrefs.GetFloat(
            Constants.PlayerX), PlayerPrefs.GetFloat(Constants.PlayerY));
        transform.position = playerPosition;

        Debug.Log("Loaded player position: " + playerPosition.ToString());
    }

    // canLoad
    bool canLoad()
    {
        return !(PlayerPrefs.GetString(Constants.SaveExists) == "");        
    }
}
