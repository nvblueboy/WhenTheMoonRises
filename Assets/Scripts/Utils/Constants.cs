﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {
    // Dictionary mapping level to necessary xp
    public static Dictionary<int, int> LevelMap = new Dictionary<int, int>
    {
        {2, 2}, {3, 4}, {4, 8}, {5, 16}, {6, 32},
        {7, 64}, {8, 128}, {9, 256}, {10, 512} 
    };

    // Keys for storing player info
    public static string PlayerX = "playerX";
    public static string PlayerY = "playerY";
    public static string CollectedShards = "collectedShards";
    public static string MaxHP = "maxHP";
    public static string MaxStamina = "maxHP";
    public static string CurrHP = "currHP";
    public static string CurrStamina = "currStamina";
    public static string Strength = "strength";
    public static string Intuition = "intuition";
    public static string Defense = "defense";
    public static string Experience = "experience";
    public static string Level = "level";

    // Keys for storing general game state
    public static string SaveExists = "saveExists";
    public static string TimeOfDay = "timeOfDay";
    public static string DaysComplete = "daysComplete";
   
    // Any other saved items here...	
}
