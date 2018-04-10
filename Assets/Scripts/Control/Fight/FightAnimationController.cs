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

    private float[] full = new float[] { 1f, 1f, 1f, 1f };

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

            float runtime = Time.time - animStartTime;

            if(currentAnimation == AnimationType.Flash) {
                if(Time.time - lastFlip > flipTime) {
                    GetComponent<Image>().enabled = !GetComponent<Image>().enabled;
                    lastFlip = Time.time;
                }
            } else if (currentAnimation == AnimationType.Shake) {
                float amount = Mathf.Sin(shakes * Mathf.PI * runtime / animationTime) * amplitude;
                float x = startLocation.x + amount;
                float y = startLocation.y;
                float z = startLocation.z;

                transform.position = new Vector3(x, y, z);

            } else if (currentAnimation == AnimationType.Fade) {
                float amount = (-Mathf.Sin(Mathf.PI * runtime / animationTime) + 1);
                Color tmp = GetComponent<Image>().color;
                tmp.a = amount;
                GetComponent<Image>().color = tmp;
            } else if(currentAnimation == AnimationType.Fade_To_Black) {
                float amount = (-Mathf.Sin(Mathf.PI * runtime / animationTime) + 1);
                SetColor(new float[] { amount, amount, amount, 1 });

            } else if(currentAnimation == AnimationType.Fade_To_Purple) {
                float amount = (-Mathf.Sin(Mathf.PI * runtime / animationTime) + 1);
                SetColor(new float[] { .5f * amount + .5f, amount, .5f * amount + .5f, 1 });
            } else if(currentAnimation == AnimationType.Fade_To_Red) {
                float amount = (-Mathf.Sin(Mathf.PI * runtime / animationTime) + 1);
                SetColor(new float[] { .5f * amount + .5f, amount, amount, 1 });
            } else if(currentAnimation == AnimationType.Tri_Color) {
                float alternate = -Mathf.Sin(5 * Mathf.PI * runtime / animationTime) + 1;
                float r = .3f;
                float g = .3f;
                float b = .3f;
                if (runtime < animationTime / 3) {
                    r = alternate;
                } else if (runtime > 2 * animationTime / 3) {
                    b = alternate;
                } else {
                    g = alternate;
                }
                SetColor(new float[] { r, g, b, 1 });
            }

            if(runtime > animationTime) {
                runAnimation = false;
                transform.position = startLocation;
                GetComponent<Image>().enabled = true;
                SetColor(new float[] { 1f, 1f, 1f, 1f });
            }
        }

    }

    void SetColor(float[] rgba) {
        Color tmp = GetComponent<Image>().color;
        tmp.r = rgba[0];
        tmp.g = rgba[1];
        tmp.b = rgba[2];
        tmp.a = rgba[3];
        GetComponent<Image>().color = tmp;
    }
}

public enum BaseAnimationType { Cosmid, None }
