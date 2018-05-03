using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : MonoBehaviour {


    protected float horiz;
    protected float vert;

    protected bool horizPress;
    protected bool vertPress;

    private float oldHoriz;
    private float oldVert;

    private bool horizHasFallen;
    private bool vertHasFallen;

    public string state;
    public string exitState;

	// Use this for initialization
	virtual protected void Start () {
        horizHasFallen = true;
        vertHasFallen = true;

        GameObject sc = GameObject.Find("Minigame Scene Switcher");
        MiniGameSceneSwitchController mgssc = null;

        if (sc != null) {
            mgssc = sc.GetComponent<MiniGameSceneSwitchController>();
        }
        
        if (mgssc != null) {
            mgssc.mc = this;
            mgssc.mc_go = this.gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    protected void getInput() {

        //Once we have a prettier Input Manager, make this suck less.
        horiz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        horizPress = false;
        vertPress = false;

        float absHoriz = Mathf.Abs(horiz);
        float absVert = Mathf.Abs(vert);

        if (absHoriz < oldHoriz) {
            horizHasFallen = true;
        }

        if (absVert < oldVert) {
            vertHasFallen = true;
        }

        if (absHoriz > .5 && horizHasFallen) {
            horizPress = true;
            horizHasFallen = false;
        }

        if (absVert > .5 && vertHasFallen) {
            vertPress = true;
            vertHasFallen = false;
        }

        oldVert = absVert;
        oldHoriz = absHoriz;
    }
}
