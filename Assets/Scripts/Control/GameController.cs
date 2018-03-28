using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public static PlayerCharacter player;
    private static Text actionText;
    public static GameController instance;    
    
    private static Vector3 playerPosition;
    private static PlayerMovementController playerController;
    private static List<string> loadedScenes;
    private static string currentScene, previousScene, activeScene, oldScene;
    private static int prevSceneIndex;   

    // Awake
    void Awake () {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {            
            DestroyImmediate(gameObject);
        }
       
        DontDestroyOnLoad(this);       
    }

    // Start
    void Start()
    {
        Debug.Log("Start");    
        playerPosition = Vector3.zero;
        previousScene = "";
        prevSceneIndex = 0;
        currentScene = SceneManager.GetActiveScene().name;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();        
        SaveHandler.SavePlayer(player);
        oldScene = SceneManager.GetActiveScene().name;                
    }

    // Update
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;

        if(currentScene != oldScene)
        {
            // Carry over player stats when switching scenes
            player = LoadHandler.LoadPlayer().updatePlayer(player);
            Debug.Log("Changed scene");
            Debug.Log("New player strength: " + player.strength);

            Debug.Log("Inventory: ");
            foreach(Item item in player.inventory)
            {
                Debug.Log("Item: " + item.getDisplayName());                
            }
        }

        // TODO: Replace this with a complete menu
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

        oldScene = currentScene;
    }         
    
    // getActiveSceneName
    public static string getActiveSceneName()
    {
        return activeScene;
    }

    // LoadScene
    public static void LoadScene(string sceneName)
    {
        SaveHandler.SavePlayer(player);
        LoadScene(sceneName, Vector3.zero);
    }

    public static void LoadScene(string sceneName, Vector3 position)
    {
        playerPosition = position;
        string currentScene = SceneManager.GetActiveScene().name;
        if(currentScene != "Fight")
        {
            previousScene = SceneManager.GetActiveScene().name;
            prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }               
        
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadScene(int index)
    {
        SaveHandler.SavePlayer(player);
        previousScene = SceneManager.GetActiveScene().name;
        prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    // LoadPreviousScene
    public static void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousScene);
    }

    // LoadNextScene
    public static void LoadNextScene()
    {        
        if (GetNextScene() == "Night2")
        {
            // Don't load Night 2; not ready yet
            LoadScene("TitleScreen");
        }        

        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene: " + currentScene);
        if (currentScene.Contains("Day") || currentScene.Contains("Night"))
        {
            Debug.Log("Contains Night");            
            LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (currentScene.Contains("Fight"))
        {            
            LoadScene(prevSceneIndex + 1);
        }
        else  // called from inside a store/house scene
        {
            Debug.Log("Does not contain Night");
            int cycle = (int) Char.GetNumericValue(previousScene[3]);
            
            // temp code to prevent unfinished Night 2 from being loaded
            if("Night" + cycle != "Night2")
            {
                LoadScene("Night" + cycle);
            } 
            else
            {
                LoadScene("TitleScreen");
            }            
        }    
    }

    // GetPreviousScene
    public static string GetPreviousScene()
    {
        return previousScene;
    }

    // GetNextScene
    public static string GetNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        return SceneManager.GetSceneByBuildIndex(currentIndex + 1).name;
    }

    // getLastPlayerPosition
    public static Vector3 getLastPlayerPosition()
    {
        return playerPosition;
    }
    
    // addLoadedScene
    public static void addLoadedScene(string sceneName)
    {        
        loadedScenes.Add(sceneName);
    }
    
    // getLoadedScenes
    public static List<string> getLoadedScenes()
    {        
        if (loadedScenes == null)
        {
            loadedScenes = new List<string>();
        }
        return loadedScenes;
    }
    
    // getPlayer 
    public PlayerCharacter getPlayer()
    {
        return player;
    }
}
