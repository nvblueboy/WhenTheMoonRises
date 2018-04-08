using System.Collections;
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

    // Awake
    void Awake()
    {

    }

    public string getMove()
    {
        string selectedMove = getSelectedMove(false);
        if (selectedMove == null)
        {

            Dictionary<string, double> distribution = new Dictionary<string, double>();

            if (type == EnemyType.Cosmid) {
                if (currStamina <= 0) {
                    distribution.Add("Scratch", 1);
                } else {
                    distribution.Add("Scratch", .8);
                    distribution.Add("Black Hole Warp", .2);
                }
            } else if (type == EnemyType.Bear) {
                if (currStamina <= 3) {
                    distribution.Add("Slash", 1);
                } else {
                    distribution.Add("Slash", .75);
                    distribution.Add("Mighty Tackle", .25);
                }
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
}


public enum EnemyType { Cosmid, Wolf, Bush_Spirit, Bear, Cosmult, Possessed_Wolf, Possessed_Bear, Cosmaster };