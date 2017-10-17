using System.Collections;
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

    // Keys for storing general game state
    public static string SaveExists = "saveExists";
    public static string TimeOfDay = "timeOfDay";
    public static string DaysComplete = "daysComplete";
   
    // Any other saved items here...	
}
