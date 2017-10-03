using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour {

    //Parameters
    private int neededActions;
    private int neededCollectibles;

    
    private int actionCount = 0;
    private int cycleCount = 0;
    private int inventory;
    private bool isDay;    //Day status


    //Day or Night
    public bool dayMode()
    {
        return isDay;
    }

    public void setDayMode(bool day)
    {
       isDay = day;
    }


    //Collectibles
    public int NeededCollectibles()
    {
        return neededCollectibles;
    }

    public void resetCollectibles()
    {
        neededCollectibles = 0;
    }


    //Player Actions
    public int NeededActions()
    {
        return neededActions;
    }

    public void resetActions()
    {
        actionCount = 0;

    }


    public int getActions() 
    {
        return actionCount;
    }

    //Day Cycle
    public int getCycle() 
    {
        return cycleCount;
    }

    public void setCycle()
    {
        cycleCount++;
    }


    //Inventory
    public int getInventory()
    {
        return inventory;
    }

    public void setInventory(int newInv)
    {
        inventory = newInv;
    }




	// Use this for initialization
	void Start () {
		
	}

    public void addAction()
    {
        actionCount++;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
