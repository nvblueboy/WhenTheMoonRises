﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

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

    private float oldSkip;

    private static GameObject dialogueUI;
    private static Text dialogueText;
    private static string activeInteraction;
    private static int dialogueIdx = 0;
    private static string[] activeText;

    // Awake
    void Awake () {
        oldSkip = 1;
        activeInteraction = "";
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();        
        dialogueUI = GameObject.FindGameObjectWithTag("TempUI");
        dialogueText = dialogueUI.GetComponentInChildren<Text>();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update
    void Update() {
        if (activeInteraction != "") 
        {
            float skip = Input.GetAxis("Jump");
            if (skip > 0 && oldSkip == 0)
            {
                dialogueIdx++;
                Debug.Log("Skip dialogue: " + dialogueIdx);
                try
                {
                    dialogueText.text = activeText[dialogueIdx];
                }
                catch (IndexOutOfRangeException e)
                {
                    Debug.Log("IndexOutOfRangeException");
                    activeInteraction = "";                    
                    dialogueUI.SetActive(false);
                    dialogueIdx = 0;
                }
            }
            oldSkip = skip;
        }
    }

    /*
    Name: showDialogue
    Parameters: string[] dialogue, bool displayDialogue
    */
    public static void showDialogue(string[] dialogue, bool displayDialogue, 
        string interaction)
    {

        // if the dialogue should be displayed and dialogue isn't
        // associated with currently active interaction
        if(displayDialogue && activeInteraction != interaction)
        {
            Debug.Log("GameController: showDialogue");
            dialogueIdx = 0;
            activeText = dialogue;
            activeInteraction = interaction;
            dialogueUI.SetActive(true);
            dialogueText.text = dialogue[dialogueIdx];                        
        }
    }    
}
