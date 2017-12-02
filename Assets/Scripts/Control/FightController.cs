using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FightController : MonoBehaviour {


    public string state = "player";

    public GameObject moveSelector;
    public GameObject statusText;

    public GameObject moveSelectorDisplay;
    public GameObject statusTextDisplay;

    public Fighter player; //This should be replaced with the player class eventually.
    public Enemy enemy;
    
    public float waitTime = 5f; //This is how long display text should wait before setting the next state.
    private float waitStart;
    private int defenseEffect, baseDefense;
    private string nextState;

    private bool gameOver = false;
    private string finalStatus;

    private float oldJump; //What the axis "jump" was in the past frame.
    private bool hasFallen;
    private bool isRising;

    private bool jumpFrame; //This will be true for one frame when the player has pressed the "jump" button.

	// Use this for initialization
	void Start () {
        //Set the "state" string to "player" if the player should go first, "enemy" if not.
        state = "player";        
        defenseEffect = 0;
        baseDefense = player.defense;
        //Link this controller to both fighters.
        enemy.fightController = this;
        player.fightController = this;
        //Get all moves initialized.
        MoveUtils.InitMoves();

        hasFallen = true;
        isRising = false;

        SceneSwitchController ssc = GameObject.Find("Scene Switcher").GetComponent<SceneSwitchController>();
        if (ssc != null)
        {
            ssc.fc = this;
            ssc.fc_go = this.gameObject;
        }

        InitializeFighters();
	}
	
	// Update is called once per frame
	void Update () {

        float jump = Input.GetAxis("Jump");
        jumpFrame = false;
        if (jump > 0 && oldJump == 0)
        {
            jumpFrame = true;
        }
        oldJump = jump;

        //Process the state machine.
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
                    // Check if player stats need to be changed this turn      
                    if (defenseEffect - 1 > 0)
                    {
                        defenseEffect -= 1;
                    }
                    else
                    {
                        // Reset defense
                        player.defense = baseDefense;
                    }

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

            string status = processMove(enemy, player, selectedMove);

            if(selectedMove == Constants.Stunned)
            {
                setStatus(status);
            }
            else
            {
                setStatus("The enemy used " + selectedMove + "! " + status);
            }            

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
            //This code has the following issue:
            //  When you press space, both the MoveSelector AND the FightController see the press.
            //  The MoveSelector sees the press and chooses the move.
            //  The FightController sees the press and skips the text.
            if (jumpFrame)
            {
                state = nextState;
            }
        }

        if (state == "end")
        {
            setStatus(finalStatus + " Press space to play again.");

            if (jumpFrame)
            {
                //The player is ready to leave the fight.
                DontDestroyOnLoad(this.gameObject);
                state = "post-fight";
                SceneManager.LoadScene("Night1");
            }
        }

        if (state != "post-fight")
        {
            updateUI();
        }
	}

    private void setStatus(string status)
    {
        statusText.GetComponent<Text>().text = status;
    }

    public void onFighterDead(Fighter f)
    {
        if (f == player)
        {
            finalStatus = "You died!";
        } else if (f == enemy)
        {
            finalStatus = "The enemy died!";
        }

        gameOver = true;
    }

    string processMove(Fighter attack, Fighter defend, string move)
    {
        //Debug.Log("Entering processMove");
        //Debug.Log("Before Turn, Attacker: " + attack);
        //Debug.Log("Before Turn, Defender: " + defend);

        // Check if attacker is stunned 
        if(move == Constants.Stunned)
        {
            return attack.name + " is stunned!";
        }

        //Get the move from the move name.
        Move moveObj = MoveUtils.GetMove(move);

        Dictionary<string, int> moveData = moveObj.processMove(attack, defend);


        //Debug.Log("Before Turn, Attacker: " + attack);
        //Debug.Log("Before Turn, Defender: " + defend);

        //Apply effects to attacker/defender.

        if (moveData.ContainsKey(Constants.HP))
        {        
            int damage = moveData[Constants.HP];            
            if (defend.defense <= 4)
            {
                defend.takeDamage(damage);
            }
            else if(5 <= enemy.defense && enemy.defense <= 9)
            {
                defend.takeDamage(damage - 1);
            }
            else if (10 <= enemy.defense && enemy.defense <= 19)
            {
                defend.takeDamage(damage - 2);
            }
            else
            {
                defend.takeDamage(damage - 3);
            }
        }

        if (moveData.ContainsKey(Constants.Stunned))
        {
            int turns = moveData[Constants.Stunned];
            for(int i = 0; i < turns; ++i)
            {
                // Will add "Stunned" move to defender move queue for i turns
                defend.addSelectedMove(Constants.Stunned);
            }
        }

        if(moveData.ContainsKey(Constants.DefenseEffect))
        {
            defenseEffect += moveData[Constants.DefenseEffect];
        }

        //Calculate damage string here.


        return "";
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
            moveSelectorDisplay.SetActive(true);
            statusTextDisplay.SetActive(false);
        } else {
            moveSelectorDisplay.SetActive(false);
            statusTextDisplay.SetActive(true);
        }
    }
}
