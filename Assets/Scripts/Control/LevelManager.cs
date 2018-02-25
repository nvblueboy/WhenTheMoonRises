using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
Name: Rabah Habiss
ID: 2268381
Email: habis102@mail.chapman.edu
Course: CPSC-340-01, CPSC-440
Assignment: Capstone Project
*/

public class LevelManager : MonoBehaviour
{
    public Dialogue[] startDialogue;

    private GameObject tempUI;    
    private string round;    
    private int starsCount = 0;
    private int actionCount = 0;
    private int cycleCount = 1;
    private int starsInventory = 0;
    private bool isDay;
    private string sceneName;

    public Text totalText;
    public Text countText;
    public int starsTotal;

    void Awake()
    {

    }

    void Start()
    {
        //Check day mode
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        if (sceneName.Substring(0, 3).Equals("Day"))
        {
            isDay = true;
        }
        else isDay = false;

        tempUI = GameObject.FindGameObjectWithTag("TempUI");
        tempUI.SetActive(false);

        CountTotalStarsInLevel();
        countText.text = "0 /";
        GameController.showDialogue(startDialogue);
    }

    void Update() {

        if (!isDay)
        {
            if (starsCount == starsTotal)
            {
                if (Input.GetKeyDown("space"))
                {
                    AddToStarStats(starsTotal);
                    setDayMode(true);      //set phase to day
                    addCycle();            //increment cycle counter (new day)
                    round = "Day" + getCycle().ToString(); //set new level name and load it
                    SceneManager.LoadScene(round);
                }

                tempUI.SetActive(true);
            }
        }

        if (isDay)
        {
            if (getActions() >= 6)
            {
                setDayMode(false);      //set phase to night
                round = "Night" + getCycle().ToString(); //set new level name and load it
                SceneManager.LoadScene(round);

                tempUI.SetActive(true);
            }
        }

    }

    /*-------------Night Mode------------*/
    void CountTotalStarsInLevel() {        
        totalText.text = starsTotal.ToString();
    }

    public void CollectStar() {
        starsCount++;
        countText.text = starsCount.ToString() + " /";
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

    public void AddToStarStats(int starsCollected)
    {
        starsInventory += starsCollected;
    }


    /*-------------Day Mode------------*/

    public void setDayMode(bool day)
    {
        isDay = day;
    }

    //Cycle Count (Day&Night)
    public int getCycle()
    {
        return cycleCount;
    }

    public void addCycle()
    {
        cycleCount++;
    }

    public int getActions()
    {
        return actionCount;
    }

    //Use this within dialogues to increment actions
    public void addAction()
    {
        actionCount++;
    }

}