﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightController : MonoBehaviour {


    public string state = "player";

    public GameObject moveSelector;
    public GameObject statusText;

    public Fighter player; //This should be replaced with the player class eventually.
    public Enemy enemy;

    public float waitTime = 5f; //This is how long display text should wait before setting the next state.
    private float waitStart;
    private string nextState;

	// Use this for initialization
	void Start () {
        //Set the "state" string to "player" if the player should go first, "enemy" if not.
        state = "player";
        //Link this controller to the enemy.
        enemy.fightController = this;

        //Get all moves initialized.
        MoveUtils.InitMoves();


        InitializeFighters();
	}
	
	// Update is called once per frame
	void Update () {

        if (state == "player")
        {
            //See if the player has selected a move by getting the string and then seeing if it's null.
            string selectedMove = player.getSelectedMove(true);
            if (selectedMove != null)
            {
                Debug.Log("Player uses " + selectedMove);
                //This block runs when the player has selected a move. Run any logic needed to process the move.

                string status = processMove(player, enemy, selectedMove);

                statusText.GetComponent<Text>().text = "You used " + selectedMove + "! "+status;

                //Set the state to display_wait to allow the player time to read what's happened.
                state = "display_wait";
                waitStart = Time.time;
                nextState = "enemy";
            }
        }
        if(state=="enemy")
        {
            //Have the enemy player run it's logic.
            string selectedMove = enemy.getMove();
            Debug.Log("Enemy uses " + selectedMove);

            string status = processMove(enemy, player, selectedMove);
            statusText.GetComponent<Text>().text = "The enemy used " + selectedMove + "! " + status;

            //Set the state to display_wait to allow the player time to read what's happened.
            state = "display_wait";
            waitStart = Time.time;
            nextState = "player";
        }

        if (state == "display_wait")
        {
            if (Time.time >= waitStart + waitTime)
            {
                state = nextState;
            }
        }
        updateUI();
	}

    string processMove(Fighter attack, Fighter defend, string move)
    {
        Debug.Log("Entering processMove");
        Debug.Log("Attacker: " + attack);
        Debug.Log("Defender: " + defend);

        Move moveObj = MoveUtils.GetMove(move);

        Debug.Log(moveObj);

        int defendDamage = moveObj.damage;

        int attackStaminaChange = moveObj.staminaCost;


        defend.takeDamage(defendDamage);
        attack.spendStamina(attackStaminaChange);

        return "It's not very effective...";
    }

    void InitializeFighters()
    {
        player.currHP = player.hp;
        player.currStamina = player.stamina;

        enemy.currHP = enemy.hp;
        enemy.currStamina = enemy.stamina;
    }

    void updateUI() {
        //This function should be called every time the UI needs to be updated.
        if(state == "player") {
            moveSelector.SetActive(true);
            statusText.SetActive(false);
        } else {
            moveSelector.SetActive(false);
            statusText.SetActive(true);
        }
    }
}
