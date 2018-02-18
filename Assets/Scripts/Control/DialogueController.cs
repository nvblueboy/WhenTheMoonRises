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
    private static float lastChoiceTime, oldSkip;
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
        
        lastChoiceTime = -999f;  // Used to prevent skipping of dialogue following choice 

        Choice choice1 = new Choice(0, "Shop", Constants.Action.OPEN_STORE);
        Choice choice2 = new Choice(4, "Talk");
        Choice choice3 = new Choice(5, "Train", Constants.Action.ADD_STRENGTH);
        Choice choice4 = new Choice(0, "Exit");

        List<Choice> choices = new List<Choice>() { choice1, choice2, choice3, choice4 };

        DialogueComponent d1 = new DialogueComponent(1, 3, "Shopkeeper", "Hello, and welcome to my shop!");        
        DialogueComponent d3 = new DialogueComponent(3, -1, "Shopkeeper", "How can I help you today?", choices);
        DialogueComponent d4 = new DialogueComponent(4, 0, "Sunny", "So what can you tell me about Raven's Ridge?");
        DialogueComponent d5 = new DialogueComponent(5, 0, "", "One point added to your Strength!");
        dialogue = new Dictionary<int, DialogueComponent>() { { 1, d1 }, { 3, d3 }, { 4, d4 }, { 5, d5 } };

        Show(1);             
    }
	
	// Update
	void Update () {
        float newSkip = Input.GetAxis("Jump");        
        if(oldSkip < 1 && newSkip > 0 && Time.time - lastChoiceTime > .2f)
        {
            Skip();            
        }
        oldSkip = newSkip;	
	}

    // Skip
    private void Skip()
    {
        int next = currentDialogue.Next();
        Debug.Log("Next: " + next);
        if(next == 0)
        {
            Debug.Log("Next is 0, return");
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
        // Show appropriate dialogue and perform correct action for choice
        Debug.Log("Choice: " + choice);        
        Choice selectedChoice = currentDialogue.choices[choice];
        Debug.Log("Selected choice next: " + selectedChoice.next);
        Show(selectedChoice.next);
        ActionController.performAction(selectedChoice.actionCode);
        lastChoiceTime = Time.time;
    }

    // Show
    public static void Show(int key)
    {
        if(key < 1)
        {           
            hideAll();
            return;
        }

        showDialogue();   // activate dialogue UI components
        currentDialogue = dialogue[key];
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
