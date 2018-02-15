using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueController : MonoBehaviour {
    private static GameObject uiDialogue, canDialogue, canTwoChoice, canFourChoice;
    private static Text txtSpeaker, txtDialogue, txtTwoChoice1, txtTwoChoice2,
        txtFourChoice1, txtFourChoice2, txtFourChoice3, txtFourChoice4;
    private static Text[] txtTwoChoices, txtFourChoices;
    private static DialogueComponent currentDialogue;
    private static float lastSkipTime;
    private static Dictionary<int, DialogueComponent> dialogue;

    // Start
    void Start () {
        // Initialize all parent GameObjects for hiding and showing dialogue UI
        uiDialogue = GameObject.FindGameObjectWithTag("DialogueUI");
        canDialogue = uiDialogue.transform.GetChild(1).gameObject;
        canTwoChoice = uiDialogue.transform.GetChild(2).gameObject;
        canFourChoice = uiDialogue.transform.GetChild(3).gameObject;

        // Initialize dialogue text
        txtSpeaker = canDialogue.transform.GetChild(1).GetComponent<Text>();
        txtDialogue = canDialogue.transform.GetChild(0).GetComponent<Text>();

        // Initialize two choice button text
        txtTwoChoice1 = canTwoChoice.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        txtTwoChoice2 = canTwoChoice.transform.GetChild(1).GetChild(0).GetComponent<Text>();

        // Initialize four choice button text
        txtFourChoice1 = canFourChoice.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        txtFourChoice2 = canFourChoice.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        txtFourChoice3 = canFourChoice.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        txtFourChoice4 = canFourChoice.transform.GetChild(3).GetChild(0).GetComponent<Text>();

        txtTwoChoices = new Text[] { txtTwoChoice1, txtTwoChoice2 };
        txtFourChoices = new Text[] { txtFourChoice1, txtFourChoice2, txtFourChoice3, txtFourChoice4 };

        lastSkipTime = -999f;

        Choice choice1 = new Choice(4, "Off myself", "");
        Choice choice2 = new Choice(4, "Die", "");
        Choice choice3 = new Choice(4, "Stop living", "");
        Choice choice4 = new Choice(4, "Perish", "");

        List<Choice> choices = new List<Choice>() { choice1, choice2, choice3, choice4 };

        DialogueComponent d1 = new DialogueComponent(1, 2, "Sunny", "First page", null);
        DialogueComponent d2 = new DialogueComponent(2, 3, "Sunny", "Second page", null);
        DialogueComponent d3 = new DialogueComponent(3, -1, "Sunny", "What are you going to do?", choices);
        DialogueComponent d4 = new DialogueComponent(4, 0, "Sunny", "Well alright then. Here I go.", null);
        dialogue = new Dictionary<int, DialogueComponent>() { { 1, d1 }, { 2, d2 }, { 3, d3 }, { 4, d4 } };

        Show(1);             
    }
	
	// Update
	void Update () {
        float skip = Input.GetAxis("Jump");	
        if(skip > 0 && Time.time - lastSkipTime > .2f)
        {
            Skip();
            lastSkipTime = Time.time;
        }	
	}

    // Skip
    private void Skip()
    {
        int next = currentDialogue.Next();
        Debug.Log("Next: " + next);
        if(next == 0)
        {
            hideAll();
            return;
        }
        else if(next > 0)
        {
            Show(next);
        }
        else
        {
            switch(currentDialogue.choices.Count)
            {
                case 2:
                    showTwoChoice();
                    for (int i = 0; i < 2; ++i)
                    {
                        txtTwoChoices[i].text = currentDialogue.choices[i].text;
                    }
                    break;
                case 4:
                    showFourChoice();
                    for(int i = 0; i < 4; ++i)
                    {
                        txtFourChoices[i].text = currentDialogue.choices[i].text;
                    }
                    break;
            }
        }
    }

    // Select
    public void Select(int choice)
    {
        Debug.Log("Choice: " + choice);
        Show(currentDialogue.choices[choice].next);
    }

    // Show
    public static void Show(int key)
    {
        currentDialogue = dialogue[key];
        showDialogue();   // activate dialogue UI components
        txtDialogue.text = currentDialogue.text;
        txtSpeaker.text = currentDialogue.speaker;     
    }

    // showDialogue
    private static void showDialogue()
    {
        canDialogue.SetActive(true);
        canTwoChoice.SetActive(false);
        canFourChoice.SetActive(false);
    }
    
    // showTwoChoice
    private static void showTwoChoice()
    {
        canDialogue.SetActive(false);
        canTwoChoice.SetActive(true);
        canFourChoice.SetActive(true);
    } 

    // showFourChoice
    private static void showFourChoice()
    {
        canDialogue.SetActive(false);
        canTwoChoice.SetActive(false);
        canFourChoice.SetActive(true);
    }

    // hideAll
    private static void hideAll()
    {
        uiDialogue.SetActive(false);
    }
}
