using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
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
    private static PlayerCharacter player;
    public static GameController instance;    
    
    private static Vector3 playerPosition;
    private static PlayerMovementController playerController;
    private static string currentScene, previousScene, activeScene;    

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
        playerPosition = Vector3.zero;
        previousScene = "";
        currentScene = SceneManager.GetActiveScene().name;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }

    // Update
    void Update() {
        currentScene = SceneManager.GetActiveScene().name;         
    }   
    
    // getActiveSceneName
    public static string getActiveSceneName()
    {
        return activeScene;
    }

    // LoadScene
    public static void LoadScene(string sceneName)
    {
        LoadScene(sceneName, Vector3.zero);
    }

    public static void LoadScene(string sceneName, Vector3 position)
    {
        playerPosition = position;
        Debug.Log("Saved position: " + playerPosition.x + ", " + playerPosition.y + ", " + playerPosition.z);
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    // LoadPreviousScene
    public static void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousScene);
    }

    // LoadNextScene
    public static void LoadNextNight()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if(currentScene.Contains("Day"))
        {
            previousScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else  // called from inside a store/house scene
        {
            int cycle = (int) Char.GetNumericValue(previousScene[3]);            
            LoadScene("Night" + cycle);
        }        
    }

    // GetPreviousScene
    public static string GetPreviousScene()
    {
        return previousScene;
    }

    // getLastPlayerPosition
    public static Vector3 getLastPlayerPosition()
    {
        return playerPosition;
    } 
}
