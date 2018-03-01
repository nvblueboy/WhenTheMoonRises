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
    public PlayerCharacter player;    
    
    private static PlayerMovementController playerController;
    private static string currentScene, previousScene, activeScene;

    // Awake
    void Awake () {
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
}
