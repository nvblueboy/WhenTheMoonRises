using System.Collections;
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
	
	// Update is called once per frame
	void Update () {

        if (state == "player")
        {
            //See if the player has selected a move by getting the string and then seeing if it's null.
            string selectedMove = player.getSelectedMove(true);
            if (selectedMove != null)
            {
                //Check if the player has enough stamina for this move.
                Move m = MoveUtils.GetMove(selectedMove);
                if (m.moveEligible(player))
                {
                    Debug.Log("Player uses " + selectedMove);
                    //This block runs when the player has selected a move. Run any logic needed to process the move.

                    string status = processMove(player, enemy, selectedMove);

                    setStatus("You used " + selectedMove + "! " + status);

                    //Set the state to display_wait to allow the player time to read what's happened.
                    state = "display_wait";
                    waitStart = Time.time;
                    if (gameOver)
                    {
                        nextState = "end";
                    }
                    else
                    {
                        nextState = "enemy";
                    }
                } else
                {
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
            Debug.Log("Enemy uses " + selectedMove);

            string status = processMove(enemy, player, selectedMove);
            setStatus("The enemy used " + selectedMove + "! " + status);

            //Set the state to display_wait to allow the player time to read what's happened.
            state = "display_wait";
            waitStart = Time.time;
            if (gameOver)
            {
                nextState = "end";
            }
            else
            {
                nextState = "player";
            }
        }

        if (state == "display_wait")
        {
            if (Time.time >= waitStart + waitTime)
            {
                state = nextState;
            }
        }

        if (state == "end")
        {
            setStatus(finalStatus);
        }

        updateUI();

        Debug.Log(nextState + " " + state);
	}

    private void setStatus(string status)
    {
        statusText.GetComponent<Text>().text = status;
    }

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
