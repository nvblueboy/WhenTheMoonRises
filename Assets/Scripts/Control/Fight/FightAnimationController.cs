using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightAnimationController : MonoBehaviour {

    public FightController fc;

    public float animationTime = .5f;

    public string matchString; //this needs to be better eventually.

    private bool runAnimation;
    private float animStartTime;

    private float lastFlip;
    public float flipTime = .1f;

    public FightAnimationDefinitions def;

    [SerializeField]
    private AnimationType currentAnimation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate() {
        if (fc.animationNeeded && fc.animationData == matchString) {
            AnimationType animType = def.getType(fc.moveName);
            if(animType != AnimationType.None) {
                runAnimation = true;
                currentAnimation = animType;
                animStartTime = Time.time;
                lastFlip = 0f;
            }
        }

        if (runAnimation) {
            if(currentAnimation == AnimationType.Flash) {
                if(Time.time - lastFlip > flipTime) {
                    GetComponent<Image>().enabled = !GetComponent<Image>().enabled;
                    lastFlip = Time.time;
                }

                if(Time.time - animStartTime > animationTime) {
                    runAnimation = false;
                    GetComponent<Image>().enabled = true;
                }
            }
        }
    }
}
