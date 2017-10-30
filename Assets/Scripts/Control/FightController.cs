using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Name: Dylan Bowman
 * ID: 2250585
 * Email: bowma128@mail.chapman.edu
 * Course: CPSC-340-01
 * Assignment: Semester Project
 * 
 * Description: This is the overall controller that manages the fight mechanic.
 *     It primarily uses a state machine to determine who needs to decide on a move.
 */ 
public class FightController : MonoBehaviour {

    public string state = "player";

    public GameObject moveSelector;
    public GameObject statusText;

    public Fighter player; //This should be replaced with the player class eventually.
    public Enemy enemy;

    public float waitTime = 5f; //This is how long display text should wait before setting the next state.
    private float waitStart;
    private string nextState;

    private bool gameOver = false;
    private string finalStatus;

	// Use this for initialization
	void Start () {
        //Set the "state" string to "player" if the player should go first, "enemy" if not.
        state = "player";
        //Link this controller to both fighters.
        enemy.fightController = this;
        player.fightController = this;
        //Get all moves initialized.
        MoveUtils.InitMoves();


        InitializeFighters();
	}
	
	/**
     * Update
     *    Runs once every frame. Handles the fight logic and state machine.
     * Parameters: None
     * Returns: None
     */
	void Update () {
        if (state == "player")
        {
            //See if the player has selected a move by getting the string and then seeing if it's null.
            string selectedMove = player.getSelectedMove(true);
            if (selectedMove != null)
            {
                //SelectedMove is a string, so turn it into a move object.
                Move m = MoveUtils.GetMove(selectedMove);
                //If the player can perform that move (decided by the move object), process it.
                if (m.moveEligible(player))
                {
                    //Process the move to find out what to tell the player.
                    string status = processMove(player, enemy, selectedMove);
                    setStatus("You used " + selectedMove + "! " + status);

                    //Set the state to display_wait to allow the player time to read what's happened.
                    state = "display_wait";
                    waitStart = Time.time;

                    //Set the next state to "enemy" (unless the game is over).
                    setNextState("enemy");

                } else
                {
                    //If the move is not eligible, inform the player.
                    setStatus("You don't have enough stamina!");

                    //Set the state to display_wait.
                    state = "display_wait";
                    waitStart = Time.time;
                    nextState = "player";
                }
            }
        }
        if(state=="enemy")
        {
            //Have the enemy player run it's logic.
            string selectedMove = enemy.getMove();

            //Process the move and alert the player.
            string status = processMove(enemy, player, selectedMove);
            setStatus("The enemy used " + selectedMove + "! " + status);

            //Set the state to display_wait to allow the player time to read what's happened.
            state = "display_wait";
            waitStart = Time.time;

            setNextState("player");
        }
        if (state == "display_wait")
        {
            //If the game is in a display wait, check if it's time to move on.
            if (Time.time >= waitStart + waitTime)
            {
                state = nextState;
            }
        }

        if (state == "end")
        {
            //If the fight is over, set the final status.
            setStatus(finalStatus);
        }

        updateUI();
	}

    /*
     * Name: setStatus
     * Parameters: string status
     * Description: Sets the status text to the parameter status for display to the player.
     */ 
    private void setStatus(string status)
    {
        statusText.GetComponent<Text>().text = status;
    }


    /*
     * Name: setNextState
     * Parameters: string next
     * Description: If the game is over (a fighter is dead), sets the next state to end.
     *     if not, sets the next state to parameter next.
     */ 
    private void setNextState(string next)
    {
        if (gameOver)
        {
            nextState = "end";
        } else
        {
            nextState = next;
        }
    }

    /*
     * Name: onFighterDead
     * Parameters: Fighter f
     * Description: Called by fighter f (either player or enemy) when they are dead,
     *     sets up the final status and marks the gameOver flag.
     */ 
    public void onFighterDead(Fighter f)
    {
        if (f == player)
        {
            Debug.Log("Player died");
            finalStatus = "You died!";
        } else if (f == enemy)
        {
            Debug.Log("Enemy died");
            finalStatus = "The enemy died!";
        }

        gameOver = true;
    }

    /*
     * Name: processMove
     * Parameters: fighter attack, fighter defend, string move
     * Description: Processes a move by using the move's process function.
     */ 
    string processMove(Fighter attack, Fighter defend, string move)
    {
        //Debug.Log("Entering processMove");
        //Debug.Log("Before Turn, Attacker: " + attack);
        //Debug.Log("Before Turn, Defender: " + defend);

        //Get the move from the move name.
        Move moveObj = MoveUtils.GetMove(move);
        Debug.Log(moveObj);

        Dictionary<string, int> moveData = moveObj.processMove(attack, defend);

        Debug.Log(moveData);

        //Debug.Log("Before Turn, Attacker: " + attack);
        //Debug.Log("Before Turn, Defender: " + defend);

        //Apply effects to attacker/defender.

        if (moveData.ContainsKey(Constants.HP))
        {
            Debug.Log("It contains the key yay");
            defend.takeDamage(moveData[Constants.HP]);
        }

        //Calculate damage string here.


        return "It sucked.";
    }

    /*
     * Name: initializeFighters
     * Parameters: None
     * Description: Initializes fighters' HP and Stamina.
     */ 
    void InitializeFighters()
    {
        player.currHP = player.hp;
        player.currStamina = player.stamina;

        enemy.currHP = enemy.hp;
        enemy.currStamina = enemy.stamina;
    }

    /*
     * Name: updateUI 
     * Parameters: None
     * Description: Sets up the UI to display properly based on the state of the fight.
     */
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
