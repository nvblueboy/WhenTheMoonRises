using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
Name: Rabah Habiss
ID: 2268381
Email: habis102@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project
*/

public class LevelManager : MonoBehaviour
{
    private string round;
    private int starsTotal;
    private int starsCount = 0;
    private int actionCount = 0;
    private int cycleCount;
    private int starsInventory = 0;
    private bool isDay;    //Day mode

    public Text totalText;
    public Text countText;

    // Use this for initialization
    void Start()
    {
        CountTotalStarsInLevel();
        countText.text = "- /";
    }


    // Update is called once per frame
    void Update()
    {

    }

    void CountTotalStarsInLevel()
    {
        starsTotal = GameObject.FindGameObjectsWithTag("StarShard").Length;
        totalText.text = starsTotal.ToString();
    }

    public void CollectStar()
    {
        
        starsCount++;
        countText.text = starsCount.ToString() + " /";
        //win condition
        //Description: ..
        if (starsCount == starsTotal)
        {

            SetInventory(starsTotal);
            ResetStarsCount();   //reset collected items counter
            setDayMode(true);      //set phase to day
            addCycle();            //increment cycle counter (new day)
            round = "Day" + getCycle().ToString(); //set new level name and load it
            SceneManager.LoadScene(round);
        }
    }

    public int GetStarsCollected()
    {
        return starsCount;
    }


    public void ResetStarsCount()
    {
        starsCount = 0;
        countText.text = starsCount.ToString() + " /";
    }

    public int GetTotalStars()
    {
        return starsTotal;
    }

    public void SetInventory(int starsCollected)
    {
        starsInventory += starsCollected;
    }

    
    /*   Day Mode   */

    public void setDayMode(bool day)
    {
        isDay = day;
    }

    //Round Count (Day+Night)
    public int getCycle()
    {
        return cycleCount;
    }

    public void addCycle()
    {
        cycleCount++;
    }


    /*   Night Mode   */
    //implement actions trigger here
    public int getActions()
    {
        return actionCount;
    }

    public void addAction()
    {
        actionCount++;
    }

}