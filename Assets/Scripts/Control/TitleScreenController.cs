using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScreenController : MonoBehaviour {

    private float oldJump;
    private bool jumpFrame;

	// Use this for initialization
	void Start () {
        //The fight carries us back here sometimes, and the Fight Controller 
        //   sticks around to make sure everything gets cleared.
        //   Find it and destroy it.
        GameObject go = GameObject.Find("Fight Controller");
        if (go != null) {
            Destroy(go);
        }
	}
	
	// Update is called once per frame
	void Update () {
        float jump = Input.GetAxis("Jump");
        jumpFrame = false;

        if(jump > 0 && oldJump == 0) {
            jumpFrame = true;
        }

        oldJump = jump;


        if (jumpFrame) {
            SceneManager.LoadScene("Day1");
        }
    }
}
