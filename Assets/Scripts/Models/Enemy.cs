﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This is a script representing an Enemy and its current state
*/

// Enemy
public class Enemy : Fighter {

    public EnemyType type;
    private int experience;

    // Start
    void Start ()
    {
        switch(type)
        {
            case EnemyType.Cosmid:
                experience = 4;
                break;
            case EnemyType.Cosmult:
                experience = 6;
                break;
            case EnemyType.Bear:
                experience = 8;
                break;            
        }                
    }

    public string getMove()
    {
        string selectedMove = getSelectedMove(false);
        if (selectedMove == null)
        {

            Dictionary<string, double> distribution = new Dictionary<string, double>();

            if(type == EnemyType.Cosmid) {
                if(currStamina <= 0) {
                    distribution.Add("Scratch", 1);
                } else {
                    distribution.Add("Scratch", .8);
                    distribution.Add("Black Hole Warp", .2);
                }
            } else if(type == EnemyType.Bear) {
                if(currStamina <= 3) {
                    distribution.Add("Slash", 1);
                } else {
                    distribution.Add("Slash", .75);
                    distribution.Add("Mighty Tackle", .25);
                }
            } else if(type == EnemyType.Wolf) {
                if(currStamina < 1) {
                    distribution.Add("Scratch", 1);
                } else if(currStamina < 4) {
                    distribution.Add("Bite", .5);
                    distribution.Add("Scratch", .5);
                } else {
                    distribution.Add("Bite", .5);
                    distribution.Add("Scratch", .2);
                    distribution.Add("Mighty Tackle", .3);
                }
            } else if(type == EnemyType.Cosmult) {
                if (currStamina > 3) {
                    distribution.Add("Meteor Shower", .2);
                    distribution.Add("Psychic Blast", .5);
                    distribution.Add("Black Hole Warp", .3);
                } else if (currStamina > 1) {
                    distribution.Add("Psychic Blast", .75);
                    distribution.Add("Black Hole Warp", .25);
                } else {
                    distribution.Add("Psychic Blast", 1);
                }
            } else {
                Debug.LogError("This enemy is not defined.");
            }

            double low = 0f;
            double num = Random.value;

            foreach(KeyValuePair<string, double> kvp in distribution) {
                double high = low + kvp.Value;
                if (num < high && num > low) {
                    selectedMove = kvp.Key;
                    break;
                }
                low = high;
            }

            addSelectedMove(selectedMove);
        }
        return getSelectedMove(true);
    }

    public int getExperience()
    {
        return experience;
    }
}


public enum EnemyType { Cosmid, Wolf, Bush_Spirit, Bear, Cosmult, Possessed_Wolf, Possessed_Bear, Cosmaster };