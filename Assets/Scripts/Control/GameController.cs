using System.Collections;
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

    private PlayerMovementController playerController;
    private float oldSkip;    

    private static GameObject dialogueUI;
    private static Text dialogueText, speakerText;
    private static string activeDialogue;
    private static bool dialogueActive;
    private static int dialogueIdx, dialogueEnd;
    private static float newDialogueTime;
    private static Dictionary<int, string[]> sceneDialogue;

    // Awake
    void Awake () {
        oldSkip = 1;
        activeDialogue = "";
        newDialogueTime = -999f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        playerController = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerMovementController>();
        dialogueUI = GameObject.FindGameObjectWithTag("TempUI");
        dialogueText = dialogueUI.GetComponentInChildren<Text>();
        speakerText = dialogueUI.transform.GetChild(2).GetComponent<Text>();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update
    void Update() {   
        if(dialogueActive)
        {
            // player can't move while dialogue active
            playerController.setPlayerCanMove(false);
        }
        else
        {
            playerController.setPlayerCanMove(true);
        }
                 
        if (activeDialogue != "" && Time.time - newDialogueTime > .2f) 
        {
            float skip = Input.GetAxis("Jump");
            if (skip > 0 && oldSkip == 0)
            {
                Debug.Log("Skip dialogue: " + dialogueIdx);
                dialogueIdx++;
                if(dialogueIdx > dialogueEnd)
                {
                    activeDialogue = "";
                    dialogueUI.SetActive(false);
                    dialogueActive = false;
                    dialogueIdx = 0;
                    return;
                }

                dialogueText.text = sceneDialogue[dialogueIdx][1];
                speakerText.text = sceneDialogue[dialogueIdx][0];            
            }
            oldSkip = skip;                        
        }        
    }

    /*
    Name: showDialogue
    Parameters: int start, int end, string interaction
    */
    public static void showDialogue(int start, int end, string interaction)
    {
         sceneDialogue = DialogueUtils.getDialogueForScene();

        // if the dialogue should be displayed and dialogue isn't
        // associated with currently active interaction
        if(start > 0 && activeDialogue != interaction)
        {           
            dialogueIdx = start;
            dialogueEnd = end;
            Debug.Log("GameController: showDialogue dialougeIdx: " + dialogueIdx);
            dialogueText.text = sceneDialogue[start][1];
            speakerText.text = sceneDialogue[start][0];            
            activeDialogue = interaction;
            dialogueUI.SetActive(true);
            dialogueActive = true;        
            newDialogueTime = Time.time;                                  
        }
    }    
}
