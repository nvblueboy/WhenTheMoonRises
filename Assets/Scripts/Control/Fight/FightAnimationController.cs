using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightAnimationController : MonoBehaviour {

    public FightController fc;

    public float animationTime = .5f;

    public List<string> matchStrings;

    private bool runAnimation;
    private float animStartTime;

    //Flash variables
    private float lastFlip;
    public float flipTime = .1f;

    //Shake variables
    private Vector3 startLocation;
    public float shakes;
    public float amplitude;


    public FightAnimationDefinitions def;

    public BaseAnimationType baseAnimation;
    public Vector3 baseLocation;

    [SerializeField]
    private AnimationType currentAnimation;

	// Use this for initialization
	void Start () {
        baseLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate() {
        if(!runAnimation) {
            if (baseAnimation == BaseAnimationType.Cosmid) {

                float amount = Mathf.Sin(1 * Mathf.PI * Time.time / 2) * 2f;

                float x = baseLocation.x;
                float y = baseLocation.y + amount;
                float z = baseLocation.z;

                transform.position = new Vector3(x, y, z);
            }
        }


        if (fc.animationNeeded && matchStrings.Contains(fc.animationData) ){
            AnimationType animType = def.getType(fc.moveName);
            if(animType != AnimationType.None) {
                runAnimation = true;
                currentAnimation = animType;
                animStartTime = Time.time;
                lastFlip = 0f;
                startLocation = transform.position;
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
            } else if (currentAnimation == AnimationType.Shake) {
                float runtime = Time.time - animStartTime;

                float amount = Mathf.Sin(shakes * Mathf.PI * runtime / animationTime) * amplitude;

                float x = startLocation.x + amount;
                float y = startLocation.y;
                float z = startLocation.z;

                transform.position = new Vector3(x, y, z);


                if (runtime > animationTime) {
                    runAnimation = false;

                    transform.position = startLocation;
                }
            } else if (currentAnimation == AnimationType.Fade) {
                float runtime = Time.time - animStartTime;

                float amount = (-Mathf.Sin(Mathf.PI * runtime / animationTime) + 1);
                Color tmp = GetComponent<Image>().color;
                tmp.a = amount;
                GetComponent<Image>().color = tmp;

                Debug.Log(amount);

                if(runtime > animationTime) {
                    runAnimation = false;

                    tmp = GetComponent<Image>().color;
                    tmp.a = 1f;
                    GetComponent<Image>().color = tmp;
                }
            }
        }
    }
}

public enum BaseAnimationType { Cosmid, None }
