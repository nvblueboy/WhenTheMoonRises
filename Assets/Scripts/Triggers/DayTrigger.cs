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

Description: Day Trigger Script. Attach to collectibles
*/

public class DayTrigger : MonoBehaviour {

    public int collectedItems=0;
    public string level;
    Collider2D col;
    PhaseController State;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (col.CompareTag("Player"))
            {
                if (collectedItems < State.NeededCollectibles())
                {
                    collectedItems++;
                    if (collectedItems == State.NeededCollectibles())
                    {
                        State.setInventory(++collectedItems);
                        State.resetCollectibles();   //reset collected items counter
                        State.setDayMode(true);      //set phase to day
                        State.setCycle();            //increment cycle counter (new day)
                        level = "Day" + State.getCycle().ToString();
                        SceneManager.LoadScene(level);
                    }
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
