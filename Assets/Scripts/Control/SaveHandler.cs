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

   /*
   Name: Save
   Parameters: PlayerCharacter player
   */
    public static void Save(PlayerCharacter player)
    {
        Debug.Log("Game saved");
        SavePlayerPosition(player);
        SavePlayerStats(player);     
    }

   /*
   Name: SavePlayerPosition
   Parameters: PlayerCharacter player
   */
    private static void SavePlayerPosition(PlayerCharacter player)
    {
        PlayerPrefs.SetString(Constants.SaveExists, "true");
        PlayerPrefs.SetFloat(Constants.PlayerX, player.transform.position.x);
        PlayerPrefs.SetFloat(Constants.PlayerY, player.transform.position.y);
    }

   /*
   Name: SavePlayerPosition
   Parameters: PlayerCharacter player
   */
   private static void SavePlayerStats(PlayerCharacter player)
   {
        PlayerPrefs.SetInt(Constants.MaxHP, player.hp);
        PlayerPrefs.SetInt(Constants.MaxStamina, player.stamina);
        PlayerPrefs.SetInt(Constants.CurrHP, player.currHP);
        PlayerPrefs.SetInt(Constants.CurrStamina, player.currStamina);
        PlayerPrefs.SetInt(Constants.Strength, player.strength);
        PlayerPrefs.SetInt(Constants.Defense, player.defense);
        PlayerPrefs.SetInt(Constants.Intuition, player.intuition);
        PlayerPrefs.SetInt(Constants.Experience, player.experience);
        PlayerPrefs.SetInt(Constants.Level, player.level);
   }
}
