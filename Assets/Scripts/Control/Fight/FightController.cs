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
    
    public Enemy enemy;

    public Enemy passedEnemy;
    
    public float waitTime = 5f; //This is how long display text should wait before setting the next state.
    private float waitStart;
    private int defenseEffect, baseDefense;
    private string nextState;

    private bool gameOver = false;
    private string finalStatus;
    private string nextScene;

    public string exitState;

    private float oldJump; //What the axis "jump" was in the past frame.
    private bool hasFallen;
    private bool isRising;

    private bool jumpFrame; //This will be true for one frame when the player has pressed the "jump" button.

    //Variables used for animation systems
    public bool animationNeeded;
    public string animationData;
    public string moveName;
    private bool runAnimation;

	// Use this for initialization
	void Start () {
        //Set the "state" string to "player" if the player should go first, "enemy" if not.
        state = "player";        
        defenseEffect = 0;
        baseDefense = GameController.player.defense;
        //Link this controller to both fighters.
        enemy.fightController = this;
        GameController.player.fightController = this;
        //Get all moves initialized.
        MoveUtils.InitMoves();

        hasFallen = true;
        isRising = false;

        //Get the scene active scene switcher and get the enemy from that.
        //First, get the game object then get it's controller.
        GameObject sc = GameObject.Find("Scene Switcher");
        SceneSwitchController ssc = null;

        animationNeeded = false;

        if(sc != null) {
            ssc = sc.GetComponent<SceneSwitchController>();
        }

        if (ssc != null) {
            ssc.fc = this;
            ssc.fc_go = this.gameObject;
            passedEnemy = (Enemy)ssc.passingObject;
        }

        InitializeFighters();
	}
	
	// Update is called once per frame
	void Update () {

        animationNeeded = false;
        animationData = "";
        moveName = "";

        float jump = Input.GetAxis("Jump");
        jumpFrame = false;

        

        if (jump > 0 && oldJump == 0 && moveSelector != null && !moveSelector.GetComponent<MoveSelector>().takeControl) {
            jumpFrame = true;
        }

        oldJump = jump;

        //  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // |                                                                      |
        // |   If you're looking for the state machine process, it's right here.  |
        // |                                                                      |
        //  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if(state == "player") {
            //See if the player has selected a move by getting the string and then seeing if it's null.            
            string selectedMove = GameController.player.getSelectedMove(true);
            if (selectedMove != null) {
                moveSelector.GetComponent<MoveSelector>().takeControl = false;
                //Check if the player has enough stamina for this move.
                Move m = MoveUtils.GetMove(selectedMove);

                if (m.moveEligible(GameController.player)) {
                    // Check if player stats need to be changed this turn      
                    if (defenseEffect - 1 > 0) {
                        defenseEffect -= 1;
                    }
                    else {
                        // Reset defense
                        GameController.player.defense = baseDefense;
                    }

                    //This block runs when the player has selected a move. Run any logic needed to process the move.

                    string status = processMove(GameController.player, enemy, selectedMove);
                    string prefix = "";
                    if (selectedMove != Constants.ItemUse && selectedMove != "Run") {
                        prefix = "You used " + selectedMove + "! ";
                    }

                    setStatus(prefix + status);
                    //Set the state to display_wait to allow the player time to read what's happened.
                    state = "display_wait";
                    if (runAnimation) {
                        animationNeeded = true;
                        if(m.animateAttacker) {
                            animationData = "player_buff";
                        } else if (m.animateDefender) {
                            animationData = "enemy_damage";
                        }
                        moveName = selectedMove;
                    }

                    waitStart = Time.time;
                    if (selectedMove == "Run") {
                        gameOver = true;
                        exitState = "run";
                    }
                    if (gameOver) {
                        nextState = "end";
                    }
                    else {
                        nextState = "enemy";
                    }
                } else {
                    setStatus("You don't have enough stamina!");

                    //Set the state to display_wait.
                    state = "display_wait";
                    waitStart = Time.time;
                    nextState = "player";
                    
                }
            }
        }

        if(state=="enemy") {
            //Have the enemy player run it's logic.
            string selectedMove = enemy.getMove();
            string status = processMove(enemy, GameController.player, selectedMove);

            Move m = MoveUtils.GetMove(selectedMove);

            if(selectedMove == Constants.Stunned || selectedMove == "NoStamina") {
                setStatus(status);
            }
            else {
                setStatus("The enemy used " + selectedMove + "! " + status);
            }            

            //Set the state to display_wait to allow the player time to read what's happened.
            state = "display_wait";

            if(runAnimation) {
                animationNeeded = true;
                if(m.animateAttacker) {
                    animationData = "enemy_buff";
                } else if(m.animateDefender) {
                    animationData = "player_damage";
                }
                moveName = selectedMove;
            }


            waitStart = Time.time;
            if (gameOver) {
                nextState = "end";
            }
            else {
                nextState = "player";
                
            }
        }

        if (state == "display_wait") {
            if (Time.time >= waitStart + waitTime) {
                state = nextState;

                if (nextState == "player") {
                    moveSelector.GetComponent<MoveSelector>().takeControl = true;
                }
            }

            if (jumpFrame) {
                state = nextState;

                if(nextState == "player") {
                    moveSelector.GetComponent<MoveSelector>().takeControl = true;
                }
            }
        }

        if (state == "end") {
            setStatus(finalStatus);

            if (jumpFrame) {
                //The player is ready to leave the fight.
                DontDestroyOnLoad(this.gameObject);
                state = "post-fight";

                if (nextScene == "TitleScreen") {
                    //Destroy ALL gameobjects. 
                    foreach(GameObject o in Object.FindObjectsOfType<GameObject>()) {
                        if(o != this.gameObject) {
                            Destroy(o);
                        }
                    }
                    SceneManager.LoadScene(nextScene);
                }
            }
        }

        if (state != "post-fight") {
            updateUI();
        }
	}

    private void setStatus(string status)
    {
        statusText.GetComponent<Text>().text = status;
    }

    public void onFighterDead(Fighter f) {
        if (f == GameController.player) {
            exitState = "loss";
            finalStatus = "You died! Press space to go to the main menu."; //TODO: Fix this based on controller type.
            //Do we want to save anything when the player dies? if so, this is the place to do it.

            nextScene = "TitleScreen";
        } else if (f == enemy) {
            exitState = "win";
            finalStatus = "The enemy died!";
            nextScene = "Night1"; 
        }

        gameOver = true;
    }

    string processMove(Fighter attack, Fighter defend, string move)
    {
        runAnimation = false;

        // Check if attacker is stunned 
        if(move == Constants.Stunned) {
            return attack.name + " is stunned!";
        }

        if (move == Constants.ItemUse) {
            return "You used an item!";
        }

        if(move == "NoStamina") {
            return attack.name + " has no stamina!";
        }

        if (move == "Run") {
            return "You ran from the fight!";
        }

        //Get the move from the move name.
        Move moveObj = MoveUtils.GetMove(move);
        Dictionary<string, int> moveData = moveObj.processMove(attack, defend);

        //Apply effects to attacker/defender.



        if(moveData.ContainsKey(Constants.HP)) {
            int damage = moveData[Constants.HP];

            if(defend.invulnerable == 0) {
                if(defend.defense <= 4) {
                    defend.takeDamage(damage);
                } else if(5 <= enemy.defense && enemy.defense <= 9) {
                    defend.takeDamage(damage - 1);
                } else if(10 <= enemy.defense && enemy.defense <= 19) {
                    defend.takeDamage(damage - 2);
                } else {
                    defend.takeDamage(damage - 3);
                }

                runAnimation = true;
            }
        }
        if (moveData.ContainsKey(Constants.GuaranteedHP)) {
            int damage = moveData[Constants.GuaranteedHP];
            defend.takeDamage(damage);

            runAnimation = true;
        }

        if (moveData.ContainsKey(Constants.Heal)) {
            int amt = moveData[Constants.Heal];
            attack.heal(amt);

            runAnimation = true;
        }
       

        if (moveData.ContainsKey(Constants.Stunned)) {
            int turns = moveData[Constants.Stunned];
            for(int i = 0; i < turns; ++i)
            {
                // Will add "Stunned" move to defender move queue for i turns
                defend.addSelectedMove(Constants.Stunned);
            }
        }



        if(moveData.ContainsKey(Constants.DefenseEffect)) {
            defenseEffect += moveData[Constants.DefenseEffect];
        }

        if(moveData.ContainsKey(Constants.Invulnerability)) {
            attack.invulnerable += moveData[Constants.Invulnerability];

            runAnimation = true;
        }
        


        //Calculate damage string here.

        if (defend.invulnerable > 0) {
            defend.invulnerable--;
        }


        return "";
    }

    void InitializeFighters()
    {
        if(passedEnemy != null) {
            enemy.hp = passedEnemy.hp;
            enemy.stamina = passedEnemy.stamina;
            enemy.currHP = passedEnemy.hp;
            enemy.currStamina = passedEnemy.stamina;
            enemy.strength = passedEnemy.strength;
            enemy.defense = passedEnemy.defense;
            enemy.level = passedEnemy.level;
            enemy.name = passedEnemy.name;
            enemy.weapon = passedEnemy.weapon;
        } else {
            Debug.LogWarning("There was no enemy passed into the scene!");
            enemy.currHP = enemy.hp;
            enemy.currStamina = enemy.stamina;
        }

        GameController.player.invulnerable = 0;
        enemy.invulnerable = 0;
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
