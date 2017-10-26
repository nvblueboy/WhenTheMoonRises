using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This is a script representing a generic Fighter. Will be the parent class of both Player and Enemies 
and contain common stats between them
*/

// Fighter
public class Fighter : MonoBehaviour {
    public int hp, stamina, strength, defense, 
        level, currHP, currStamina;
    public string weapon;

    public FightController fightController;
    
    public override string ToString()
    {
        return "HP: " + hp + 
            " Stamina: " + stamina + 
            " Strength: " + strength +
            " Defense: " + defense +
            " Level: " + level +
            " CurrHP: " + currHP +
            " CurrStamina: " + currStamina;
    }
    private string selectedMove;

    private Queue<string> moveQueue = new Queue<string>();
    /*
    Name: increaseStrength
    Parameters: int increase
    */
    public void increaseStrength(int increase)
    {
        strength += increase;
    }

    /*
    Name: decreaseStrength
    Parameters: int decrease
    */
    public void decreaseStrength(int decrease)
    {
        if(strength - decrease >= 0)
        {
            strength -= decrease;
            return;
        }
        strength = 0;      
    }

    /*
    Name: increaseDefense
    Parameters: int increase
    */
    public void increaseDefense(int increase)
    {
        defense += increase;
    }

    /*
    Name: decreaseDefense
    Parameters: int decrease
    */
    public void decreaseDefense(int decrease)
    {
        if(defense - decrease >= 0)
        {
            defense -= decrease;
            return;
        }
        defense = 0;       
    }

    /*
    Name: takeDamage
    Parameters: int damage
    */
    public void takeDamage(int damage)
    {
        if (currHP - damage > 0)
        {
            currHP -= damage;
            return;
        }
        currHP = 0;
        // Player dead at this point so do stuff
        fightController.onFighterDead(this);
    }

    /*
    Name: restoreHealth
    Parameters: int gained
    */
    public void gainHealth(int gained)
    {
        if (currHP + gained <= hp)
        {
            currHP += gained;
            return;
        }
        currHP = hp;
    }

    /*
    Name: spendStamina
    Parameters: int spent
    Returns: bool
    */
    public bool spendStamina(int spent)
    {
        if (stamina - spent >= 0)
        {
            currStamina -= spent;
            return true;            
        }
        return false;        
    }

    /*
    Name: spendStamina
    Parameters: int spent
    */
    public void gainStamina(int gained)
    {
        if (stamina + gained <= stamina)
        {
            currStamina += gained;
            return;
        }
        currStamina = stamina;
    }

    /*
     * Name: addSelectedMove
     * Parameters: string move
     * Description: sets selectedMove variable to be accessed by the fight controller. 
     */
     public void addSelectedMove(string move)
    {
        moveQueue.Enqueue(move);
    }

    /*
     * Name: getSelectedMove
     * Parameters: bool clear
     * Description: returns the selected move string. If clear is true, it clears the string.
     */
    public string getSelectedMove(bool clear)
    {
        string ret = null;
        if (moveQueue.Count > 0)
        {
            ret = moveQueue.Peek();
            if (clear)
            {
                moveQueue.Dequeue();
            }
        }
        return ret;
    }
}
