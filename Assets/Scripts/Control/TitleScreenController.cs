using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour {

    private float oldJump;
    private bool jumpFrame;

    public Text title;
    public Text pressSpace;
    public Text copyright;

    private Color space_baseColor;
    private Color title_baseColor;
    private Color copyright_baseColor;

    float startTime;

	// Use this for initialization
	void Start () {
        //The fight carries us back here sometimes, and the Fight Controller 
        //   sticks around to make sure everything gets cleared.
        //   Find it and destroy it.
        GameObject go = GameObject.Find("Fight Controller");
        if (go != null) {
            Destroy(go);
        }

        space_baseColor = pressSpace.color;
        title_baseColor = title.color;
        copyright_baseColor = copyright.color;
        startTime = Time.time;
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

        float currTime = Time.time - startTime;

        float runTime = 2;

        float mult = 1f;
        if (currTime < runTime) {
            mult = 0;
        }else if (currTime < runTime * 2) {
            mult = (1 / runTime) * (currTime - runTime) ;
        }

        Color c = space_baseColor;
        c.a = (Mathf.Sin(Time.time * 2)/5 + .8f) * mult;
        pressSpace.color = c;

        c = title_baseColor;
        c.a = title_baseColor.a * mult;
        title.color = c;

        c = copyright_baseColor;
        c.a = copyright_baseColor.a * mult;
        copyright.color = c;

    }
}
