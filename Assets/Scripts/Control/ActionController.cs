using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

	// Start
	void Start () {
		
	}    
	
	// Update is called once per frame
	void Update () {
		
	}    

    public static void performAction(Constants.Action action)
    {
        switch(action)
        {
            case Constants.Action.OPEN_STORE:
                Debug.Log("Open store");
                break;
            case Constants.Action.NONE:
                break;
            case Constants.Action.ADD_STRENGTH:
                Debug.Log("Strength increased");
                break;
            case Constants.Action.ADD_STAMINA:
                break;
            case Constants.Action.ADD_INTUITION:
                break;
            default:
                break;
        }
    }  
}
