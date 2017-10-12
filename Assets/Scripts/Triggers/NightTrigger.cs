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

Description: Night Trigger Script. Attach to action objects
*/

public class NightTrigger : MonoBehaviour {

    string level = "";

    Collider2D col;
    PhaseController Status;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (col.CompareTag("Player"))
            {
                if (Status.getActions() < Status.NeededActions())
                {
                    Status.addAction(); //increment actions counter

                    if (Status.getActions() == Status.NeededActions()) //check if actions target is reached - transition to day
                    {
                        Status.resetActions(); //reset actions counter
                        Status.setDayMode(false);      //set phase to night

                        level = "Night" + Status.getCycle().ToString();
                        SceneManager.LoadScene(level);
                    }
                }
            }
        }
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
