using System.Collections;
using System.Collections.Generic;
using System;

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
    public static string Inventory = "inventory";

    // Keys for returned move dictionaries
    public static string HP = "HP";
    public static string Stamina = "Stamina";
    public static string Stunned = "Stunned";
    public static string DefenseEffect = "DefenseEffect";
    public static string ItemUse = "Item Use";

    // Player Animations
    public static string NightPrefix = "AnimationControllers/Night/";
    public static string WalkFront = "sunny walk front night 3";
    public static string WalkBack = "sunny walk back night 1";
    public static string WalkRight = "sunny walk night right1";
    public static string WalkLeft = "sunny walk night left3";

    // File paths
    public static string DialoguePath = "Dialogue/{0}";
    public static string StoreDialoguePath = "Assets/Resources/Dialogue/{0}.json";

    // Action codes for performing actions based on dialogue option
    [Serializable]
    public enum Action
    {
        NONE,
        QUIT,
        OPEN_STORE,
        OPEN_BOOKSTORE,
        ADD_STRENGTH,
        ADD_STAMINA,
        ADD_INTUITION,
        ADD_MAGIC,
        ADD_DEFENSE,
        ADD_HEALTH,
        LOAD_PREV_SCENE,
        LOAD_NEXT_SCENE
    }
}
