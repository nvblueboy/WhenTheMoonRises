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
    PhaseController State;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (col.CompareTag("Player"))
            {
                if (State.getActions() < State.NeededActions())
                {
                    State.addAction(); //increment actions counter

                    if (State.getActions() == State.NeededActions()) //check if actions target is reached - transition to day
                    {
                        State.resetActions(); //reset actions counter
                        State.setDayMode(false);      //set phase to night

                        level = "Night" + State.getCycle().ToString();
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
