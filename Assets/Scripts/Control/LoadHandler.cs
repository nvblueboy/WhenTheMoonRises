using System.Collections;
using System.Collections.Generic;
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
public class LoadHandler : MonoBehaviour { 

    /*
    Name: Load
     Parameters: PlayerCharacter player
    */
    public static void Load(PlayerCharacter player)
    {
        Debug.Log("Attempt to load");
        if (canLoad())
        {
            LoadPlayerPosition(player);
            LoadPlayerStats(player);
            LoadInventory(player);
        }
    }  
     
    /*
    Name: LoadPlayerPosition
    Parameters: PlayerCharacter player
    */
    private static void LoadPlayerPosition(PlayerCharacter player)
    {
        Vector2 playerPosition = new Vector2(PlayerPrefs.GetFloat(
            Constants.PlayerX), PlayerPrefs.GetFloat(Constants.PlayerY));        
        player.transform.position = playerPosition;
        Debug.Log("Loaded player position: " + playerPosition.ToString());
    }  
         
     /*
     Name: LoadPlayerStats
     Parameters: PlayerCharacter player
     */
     private static void LoadPlayerStats(PlayerCharacter player)
     {
        player.hp = PlayerPrefs.GetInt(Constants.MaxHP);
        player.stamina = PlayerPrefs.GetInt(Constants.MaxStamina);
        player.currHP = PlayerPrefs.GetInt(Constants.CurrHP);
        player.currStamina = PlayerPrefs.GetInt(Constants.CurrStamina);
        player.strength = PlayerPrefs.GetInt(Constants.Strength);
        player.intuition = PlayerPrefs.GetInt(Constants.Intuition);
        player.defense = PlayerPrefs.GetInt(Constants.Defense);
        player.experience = PlayerPrefs.GetInt(Constants.Experience);
        player.level = PlayerPrefs.GetInt(Constants.Level);
     }

    /*
   Name: LoadInventory
   Parameters: PlayerCharacter player
   */
    private static void LoadInventory(PlayerCharacter player)
    {        
        string item;
        for (int i = 0; i < player.inventory.Length; ++i)
        {
            item = PlayerPrefs.GetString(Constants.Inventory + i.ToString(), "");
            player.inventory[i] = item;           
        }
    }

    // canLoad    
    private static bool canLoad()
    {
        return !(PlayerPrefs.GetString(Constants.SaveExists) == "");
    } 
}
