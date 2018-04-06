using System.IO;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This is a script for loading game and player data
*/

// LoadHandler
public class LoadHandler {    

    // LoadPlayer
    public static PlayerWrapper LoadPlayer()
    {        
        TextAsset playerJson = Resources.Load<TextAsset>(Constants.PlayerPath);
        Debug.Log("Player json: " + playerJson);
        PlayerWrapper playerWrapper = JsonUtility.FromJson<PlayerWrapper>(playerJson.text);
        return playerWrapper;
    }     
}
