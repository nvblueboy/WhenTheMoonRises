using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Name: Rabah Habiss
ID: 2268381
Email: habis102@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project
*/

public class LevelManager : MonoBehaviour
{
    public int shardsTotal;
    public int shardsCount = 0;
    public Text totalText;
    public Text countText;

    // Use this for initialization
    void Start()
    {
        CountTotalShardsInLevel();
        countText.text = "- /";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CountTotalShardsInLevel()
    {
        shardsTotal = GameObject.FindGameObjectsWithTag("StarShard").Length;
        totalText.text = shardsTotal.ToString();
    }

    public void CollectShard()
    {
        shardsCount++;
        countText.text = shardsCount.ToString() + " /";
    }


}