using System.IO;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This is a script for saving game and player data
*/

// SaveHandler
public class SaveHandler {      

    /*
    Name: SavePlayer
    Parameters: PlayerCharacter player
    */
    public static void SavePlayer(PlayerCharacter player)
    {
        StreamWriter writer = new StreamWriter(
            File.OpenWrite(Constants.StorePlayerPath));
        string json = JsonUtility.ToJson(new PlayerWrapper(player));
        writer.Write(json);
        writer.Close();
    }   
}
