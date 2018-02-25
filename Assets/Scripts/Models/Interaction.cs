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

Description: This is the parent interaction script with base functionality
*/

// Interaction
public abstract class Interaction : MonoBehaviour {
    public string preReq;
    public Color32 pulseColor;
    [HideInInspector] public bool displayDialogue;
    public bool removePreReq, delayAction;
    public float pulseSpeed, pulseStrength;
    //public int successStart, successEnd, failStart, failEnd;
    public Dialogue[] successText, failText;

    protected bool hasInteracted, actionComplete;
    protected PlayerCharacter player;    

    private SpriteRenderer renderer;    
    private float alpha;    
    private bool dim;
    
    // Awake
    void Awake()
    {        
        hasInteracted = false;
        actionComplete = false;
        displayDialogue = successText.Length > 0 || failText.Length > 0;              
        alpha = 255;  
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();        
        renderer = GetComponent<SpriteRenderer>();
        pulseColor.a = 255;        
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
    
    // isSuccess
    public bool isSuccess()
    {
        return hasInteracted;
    }

    // interact
    public abstract void interact();

    // triggerAction
    public abstract void triggerAction();
}
