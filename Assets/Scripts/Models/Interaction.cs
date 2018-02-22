using System.Collections;
using System.Collections.Generic;
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
    public bool removePreReq;
    public float pulseSpeed, pulseStrength;

    protected bool hasInteracted;
    [SerializeField] protected PlayerCharacter player;

    private SpriteRenderer renderer;
    private float alpha = 255;
    private bool dim;
    
    // Awake
    void Awake()
    {
        hasInteracted = false;
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

        foreach(Item item in player.inventory)
        {
            Debug.Log(item.getName());
            if(item.getName() == preReq)
            {
                return true;
            }
        }
        return false;
    }

    // interact
    public abstract void interact();	
}
