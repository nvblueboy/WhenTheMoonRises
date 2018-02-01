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

Description: This is the parent interaction script with base functionality
*/

// Interaction
public abstract class Interaction : MonoBehaviour {
    public string preReq;
    public Color32 pulseColor;
    public bool removePreReq, displayDialogue, useDefaultText;
    public float pulseSpeed, pulseStrength;
    public string[] successText, failText;

    protected bool hasInteracted;
    protected PlayerCharacter player;    

    private SpriteRenderer renderer;
    private GameObject dialogueUI;
    private Text dialogueText;
    private string[] activeText;
    private float alpha, oldSkip; // oldSkip for storing skip input from last frame
    protected int idx = 0;
    private bool dim, dialogueActive;
    
    // Awake
    void Awake()
    {        
        hasInteracted = false;
        dialogueActive = false;
        oldSkip = 1;
        alpha = 255;        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        dialogueUI = GameObject.FindGameObjectWithTag("TempUI");
        dialogueText = dialogueUI.GetComponentInChildren<Text>();
        renderer = GetComponent<SpriteRenderer>();
        pulseColor.a = 255;        
    } 
    
    // Update
    void Update()
    {
        // Use this input to skip/dismiss dialogue        
        if (dialogueActive)
        {
            float skip = Input.GetAxis("Jump");
            if (skip > 0 && oldSkip == 0)
            {                
                idx++;
                Debug.Log("Skip dialogue: " + idx);
                try
                {
                    dialogueText.text = activeText[idx];
                }
                catch (IndexOutOfRangeException e)
                {
                    Debug.Log("IndexOutOfRangeException");
                    dialogueActive = false;
                    dialogueUI.SetActive(false);
                    idx = 0;
                }
            }
            oldSkip = skip;
        }
    }   

    // FixedUpdate
    void FixedUpdate()
    {
        if(!hasInteracted)
        {
            if (alpha >= 254)
            {
                dim = true;

            }
            else if (alpha <= (255 - pulseStrength * 10))
            {
                dim = false;

            }

            if (dim)
            {
                alpha = alpha - Time.deltaTime * (10f * pulseSpeed);
            }
            else
            {
                alpha = alpha + Time.deltaTime * (10f * pulseSpeed);
            }

            pulseColor.a = (byte)alpha;
            renderer.color = pulseColor;
        }
        else
        {
            renderer.color = Color.white;
        }                
    }

    // hasPreReq
    public bool hasPreReq()
    {
        if(preReq == "")
        {            
            return true;
        }

        foreach(string item in player.inventory)
        {
            if(item == preReq)
            {
                return true;
            }
        }
        return false;
    }

   /*
   Name: showDialogue
   Parameters: bool success
   */
    public void showDialogue(bool success)
    {
        if(displayDialogue)
        {
            dialogueUI.SetActive(true);
            dialogueActive = true;

            if(success)
            {
                activeText = successText;
                dialogueText.text = successText[0];
                return;
            }
            activeText = failText;
            dialogueText.text = failText[0];
        }               
    }

    // interact
    public abstract void interact();	
}
