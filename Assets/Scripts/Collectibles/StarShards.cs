using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Name: Rabah Habiss
ID: 2268381
Email: habis102@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: Collectible Script. Attach to star objects
*/

public class StarShards : MonoBehaviour
{
    private string round;
    private bool isCollected = false;
    private Renderer render;
    private LevelManager level;

    // Use this for initialization
    void Start()
    {
        level = GameObject.FindGameObjectWithTag(
            "LevelManager").GetComponent<LevelManager>();
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        level = GameObject.FindGameObjectWithTag(
            "LevelManager").GetComponent<LevelManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isCollected)
        {
            //collect star
            Debug.Log("Collect star");
            isCollected = true;
            render.enabled = false;
            level.CollectStar();


            
        }
    }

}
