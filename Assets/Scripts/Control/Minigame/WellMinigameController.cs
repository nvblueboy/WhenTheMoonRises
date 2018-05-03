using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WellMinigameController : MinigameController {

    float current;

    char[] possibilities = { 'w', 'a', 's', 'd' };

    char target;

    Queue<char> upcomingTargets = new Queue<char>();

    char[] targetSet;

    public float threshold = .2f;

    public Color upcoming;
    public Color currentColor;
    public Color finished;


    public Text display1;
    public Text display2;
    public Text display3;
    public Text display4;

    public GameObject bar;

    [SerializeField]int currentLetter = 0;

    [SerializeField] int level = 0;

    [SerializeField] float startTime = 0;

    Text[] displays;

	// Use this for initialization
	override protected void Start () {
        base.Start();
        current = 0;
        currentLetter = 0;
        newQueue();
        target = upcomingTargets.Peek();
        updateDisplay();

        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        base.getInput();

        char hit = '0';
        if (vert > threshold && vertPress) {
            hit = 'w';
        }
        if (vert < -threshold && vertPress) {
            hit = 's';
        }
        if(horiz > threshold && horizPress) {
            hit = 'd';
        }
        if(horiz < -threshold && horizPress) {
            hit = 'a';
        }

        if (hit == target) {
            upcomingTargets.Dequeue();


            if(upcomingTargets.Count == 0) {

                //SUCCESS
                if(level == 2) {
                    DontDestroyOnLoad(this.gameObject);
                    state = "post-game";
                    exitState = "win";
                    return;
                }

                newQueue();
                target = upcomingTargets.Peek();
                level++;
                startTime = Time.time;
                currentLetter = 0;
                updateDisplay();

            } else {
                target = upcomingTargets.Peek();
                currentLetter++;
                updateDisplay();
            }
        } else if (hit != '0') {
            level--;
            if (level < 0) {
                level = 0;
            }

            upcomingTargets.Clear();
            newQueue();
            currentLetter = 0;
            updateDisplay();
            target = upcomingTargets.Peek();
            startTime = Time.time;
        }

        float t = Time.time - startTime;
        float left = (5 - level);

        Vector3 temp = bar.transform.localScale;
        temp.x = 1 - (t / left);
        bar.transform.localScale = temp;

        if(t > left) {
            level--;
            if(level < 0) {
                level = 0;
            }

            upcomingTargets.Clear();
            newQueue();
            currentLetter = 0;
            updateDisplay();
            target = upcomingTargets.Peek();
            startTime = Time.time;
        }

	}

    string createString(char[] arr) {
        List<string> strings = new List<string>();

        foreach(char i in arr) {
            strings.Add(i.ToString());
        }

        return string.Join(" - ", strings.ToArray());
    }

    void updateDisplay() {
        display1.text = targetSet[0].ToString();
        if (currentLetter >= 1) {
            display1.color = finished;
        } else {
            display1.color = currentColor;
        }

        display2.text = targetSet[1].ToString();
        if (currentLetter >= 2) {
            display2.color = finished;
        } else if (currentLetter == 1) {
            display2.color = currentColor;
        } else {
            display2.color = upcoming;
        }

        display3.text = targetSet[2].ToString();
        if(currentLetter >= 3) {
            display3.color = finished;
        } else if(currentLetter == 2) {
            display3.color = currentColor;
        } else {
            display3.color = upcoming;
        }

        display4.text = targetSet[3].ToString();
        if(currentLetter == 3) {
            display4.color = currentColor;
        } else {
            display4.color = upcoming;
        }
    }

    void newQueue() {
        for(int i = 0; i < 4; ++i) {
            upcomingTargets.Enqueue(possibilities[Random.Range(0, 4)]);
        }
        targetSet = upcomingTargets.ToArray();
    }
}
